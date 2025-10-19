using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Files
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
