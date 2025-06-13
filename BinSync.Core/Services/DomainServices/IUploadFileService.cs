using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Services.DomainServices
{
	public interface IUploadFileService
	{
		Task<string> StoreFileAsync(Stream fileStream, string fileName, long maxAllowedSizeBytes, CancellationToken cancellationToken = default);
	}
}
