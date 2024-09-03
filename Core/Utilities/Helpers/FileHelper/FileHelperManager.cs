using Core.Utilities.Helpers.GuidHelperr;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            Delete(filePath);
            return Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length <= 0) return null;

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            string extension = Path.GetExtension(file.FileName);
            string guid =GuidHelper.CreateGuid();
            string path = guid + extension;
            using (FileStream fileStream = File.Create(root + path))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                return path;
            }
        }
    }
}
