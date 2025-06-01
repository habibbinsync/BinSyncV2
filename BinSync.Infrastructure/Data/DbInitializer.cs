using BinSync.Core.Models.Licensing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BinSyncDbContext context)
        {
            // Ensure database is created and migrated
            context.Database.Migrate();

            // Optimize SQLite settings
            ConfigureDatabasePerformance(context);

            // Seed initial license data if empty
            SeedLicenseData(context);

            // Create essential indexes if they don't exist
            CreateAdditionalIndexes(context);
        }

        private static void ConfigureDatabasePerformance(BinSyncDbContext context)
        {
            context.Database.ExecuteSqlRaw("PRAGMA synchronous = NORMAL");
            context.Database.ExecuteSqlRaw("PRAGMA temp_store = MEMORY");
            context.Database.ExecuteSqlRaw("PRAGMA cache_size = -10000"); // ~10MB cache
            context.Database.ExecuteSqlRaw("PRAGMA mmap_size = 30000000000"); // 30GB mmap
        }

        private static void SeedLicenseData(BinSyncDbContext context)
        {
            if (!context.LicenseStates.Any())
            {
                var freeLicense = new LicenseState
                {
                    LicenseId = "FREE_LICENSE",
                    Type = "free",
                    IssueDate = DateTime.UtcNow,
                    Features = new List<LicenseFeature>
                    {
                        new() { Name = "zstd_1_22" },
                        new() { Name = "nzb_support" },
                        new() { Name = "basic_p2p" }
                    }
                };

                context.LicenseStates.Add(freeLicense);
                context.SaveChanges();
            }
        }

        private static void CreateAdditionalIndexes(BinSyncDbContext context)
        {
            // Indexes not covered by EF configurations
            try
            {
                context.Database.ExecuteSqlRaw(
                    @"CREATE INDEX IF NOT EXISTS IX_UploadProgress_MessageId 
                    ON UploadProgress(MessageId)");

                context.Database.ExecuteSqlRaw(
                    @"CREATE INDEX IF NOT EXISTS IX_FailureReports_Timestamp 
                    ON FailureReports(Timestamp)");

                context.Database.ExecuteSqlRaw(
                    @"CREATE INDEX IF NOT EXISTS IX_Seeds_IsPrivate 
                    ON Seeds(IsPrivate)");
            }
            catch (Exception ex)
            {
                // Log index creation errors
                Console.WriteLine($"Index creation failed: {ex.Message}");
            }
        }

        public static void WarmUpCache(BinSyncDbContext context)
        {
            // Preload frequently accessed tables
            _ = context.LicenseStates.FirstOrDefault();
            _ = context.JournalCaches.Take(1).ToList();
            _ = context.UsageTrackings.Take(1).ToList();
        }
    }
}