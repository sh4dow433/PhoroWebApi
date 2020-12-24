using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public class PhotoLoader : IPhotoLoader
    {
        public string LoadPhoto(string diskName)
        {
            string path = $@".\Photos\{diskName}.txt";
            byte[] fileBytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(fileBytes);
        }

        public async Task<byte[]> LoadPhotoAsync(string diskName)
        {
            throw new NotImplementedException();
        }
    }
}
