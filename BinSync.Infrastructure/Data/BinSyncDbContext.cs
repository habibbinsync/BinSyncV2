using BinSync.Core.Models.Files;
using BinSync.Core.Models.Licensing;
using BinSync.Core.Models.P2P;
using BinSync.Core.Models.Seeds;
using BinSync.Core.Models.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.IO;

namespace BinSync.Infrastructure.Data
{
	public class BinSyncDbContext : DbContext
	{
		// Storage Tables
		public DbSet<UploadProgress> UploadProgresses { get; set; }
		public DbSet<DownloadProgress> DownloadProgresses { get; set; }
		public DbSet<NzbProgress> NzbProgresses { get; set; }
		public DbSet<FileHash> FileHashes { get; set; }

		// P2P Tables
		public DbSet<FailureReport> FailureReports { get; set; }
		public DbSet<FailureReportMessage> FailureReportMessages { get; set; }
		public DbSet<MissingArticle> MissingArticles { get; set; }
		public DbSet<JournalCache> JournalCaches { get; set; }

		// License Tables
		public DbSet<LicenseState> LicenseStates { get; set; }
		public DbSet<LicenseFeature> LicenseFeatures { get; set; }
		public DbSet<UsageTracking> UsageTrackings { get; set; }

		// Seed Tables
		public DbSet<Seed> Seeds { get; set; }
		public DbSet<SeedAccess> SeedAccesses { get; set; }

		/// <summary>
		/// Parameterless constructor needed for EF Core design-time tools 
		/// (Add-Migration, Update-Database) to call OnConfiguring().
		/// </summary>
		public BinSyncDbContext()
		{
		}

		/// <summary>
		/// This constructor is used when you want to register the context via DI
		/// (e.g. in ASP.NET Core Startup or a HostBuilder). EF will pass in
		/// DbContextOptions<BinSyncDbContext> when you call services.AddDbContext&lt;...&gt;(...).
		/// </summary>
		public BinSyncDbContext(DbContextOptions<BinSyncDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure composite keys and relationships
			ConfigureStorageModels(modelBuilder);
			ConfigureP2PModels(modelBuilder);
			ConfigureLicenseModels(modelBuilder);
			ConfigureSeedModels(modelBuilder);

			// Apply SQLite-specific optimizations
			ConfigureSqliteSpecifics(modelBuilder);
		}

		private void ConfigureStorageModels(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UploadProgress>()
				.HasIndex(u => new { u.DatasetId, u.Status });

			modelBuilder.Entity<DownloadProgress>()
				.HasIndex(d => new { d.DatasetId, d.Path, d.ChunkIndex })
				.IsUnique();

			modelBuilder.Entity<NzbProgress>()
				.HasIndex(n => n.MessageId);

			modelBuilder.Entity<FileHash>()
				.HasIndex(f => f.Path)
				.IsUnique();
		}

		private void ConfigureP2PModels(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FailureReport>()
				.HasMany(f => f.Messages)
				.WithOne(m => m.FailureReport)
				.HasForeignKey(m => m.FailureReportId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MissingArticle>()
				.HasIndex(m => new { m.DatasetId, m.Status });

			modelBuilder.Entity<JournalCache>()
				.HasIndex(j => j.DatasetId)
				.IsUnique();
		}

		private void ConfigureLicenseModels(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LicenseState>()
				.HasMany(l => l.Features)
				.WithOne(f => f.LicenseState)
				.HasForeignKey(f => f.LicenseStateId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<UsageTracking>()
				.HasIndex(u => u.LicenseId);
		}

		private void ConfigureSeedModels(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Seed>()
				.HasMany(s => s.AllowedPeers)
				.WithOne(a => a.Seed)
				.HasForeignKey(a => a.SeedId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Seed>()
				.HasIndex(s => s.DatasetId);
		}

		private void ConfigureSqliteSpecifics(ModelBuilder modelBuilder)
		{
			// SQLite doesn't support decimal directly, so map decimals/doubles as needed
			modelBuilder.Entity<UsageTracking>()
				.Property(u => u.UploadBytes)
				.HasConversion<double>();

			modelBuilder.Entity<UsageTracking>()
				.Property(u => u.DownloadBytes)
				.HasConversion<double>();

			// Enable WAL mode for improved concurrency (optional)
			// Note: Executing PRAGMAs in OnModelCreating can sometimes cause issues
			// if the database isn’t created yet. You might want to move this into
			// a separate initialization routine that runs once the DB exists.
			try
			{
				Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL");
			}
			catch
			{
				// If the database doesn’t exist yet, this will fail silently
				// and will be applied automatically once the DB file is created.
			}
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// If nobody configured through DI or passed in DbContextOptions, fall back to a file-based SQLite
			if (!optionsBuilder.IsConfigured)
			{
				// 1. Use the current working directory
				//var folder = "Data Source=C:\\Users\\RT\\";

				// 2. Compose a path like "<working-folder>/binsync.db"
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "binsync.db");

				// 3. Use a file-based SQLite database (no in-memory mode):
				optionsBuilder.UseSqlite($"Data Source={filePath}");
			}
		}
	}
}
