using BinSync.Core.Models.Storage;
using BinSync.Core.Services.DomainServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BinSync.Infrastructure.Services
{
	public class UploadFileService: IUploadFileService
	{
		private readonly ILogger<UploadFileService> _logger;
		private readonly HttpClient _httpClient;
		private readonly string _remoteUploadUrl;
		public UploadFileService(ILogger<UploadFileService> logger, HttpClient httpClient, IConfiguration configuration)
		{
			_logger = logger;
			_httpClient = httpClient;
			//_remoteUploadUrl = configuration["FileUpload:RemoteUrl"]
							   //?? throw new InvalidOperationException(
									 //"Missing configuration: FileUpload:RemoteUrl");
		}
		public async Task<string> StoreFileAsync(Stream fileStream, string fileName, long maxAllowedSizeBytes, CancellationToken cancellationToken = default)
		{
			if(fileStream is null)
				throw new ArgumentNullException(nameof(fileStream), "File stream cannot be null.");
			if(string.IsNullOrWhiteSpace(fileName))
				throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
			if(fileStream.CanSeek)
			{
				fileStream.Seek(0, SeekOrigin.Begin);
				if(fileStream.Length > maxAllowedSizeBytes)
					throw new ArgumentException($"File size exceeds the maximum allowed size of {maxAllowedSizeBytes} bytes.", nameof(fileStream));
				fileStream.Seek(0, SeekOrigin.Begin);
			}
			try
			{
				using var content = new StreamContent(fileStream);
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
				content.Headers.ContentLength = fileStream.Length;
				content.Headers.Add("FileName", fileName);
				var response = await _httpClient.PostAsync(_remoteUploadUrl, content, cancellationToken);
				response.EnsureSuccessStatusCode();
				var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
				return responseContent; 
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error uploading file: {FileName}", fileName);
				throw;
			}
		}

	}
}
