using Microsoft.AspNetCore.Http;
using ServiceLayer.ServiceDtos.Flowers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers
{
    internal static class ImageHelpers
    {
        internal static async Task<string?> TryStoreImage(IFormFile? file, string? physicalPath)
        {
            string? imageUri = null;
            if (file != null && physicalPath != null)
            {
                var filePath = physicalPath + "\\" + file.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    imageUri = file.FileName;
                }
            }

            return imageUri;
        }
    }
}
