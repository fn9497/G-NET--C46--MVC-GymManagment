using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Interfaces
{
    public interface IAttachmentService
    {
        Task<string?> UploadAsync(Stream filestream, string filename, string foldername, CancellationToken ct = default);
        bool Delete( string filename, string foldername);
        (Stream stream , string contenttype)?GetFile(string filename, string foldername);
    }
}
