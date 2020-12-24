using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public class PhotoSaver : IPhotoSaver
    {
        public string SavePhoto(byte[] photoFile)
        {

            var uniqueFileName = $@"{Guid.NewGuid()}";
            string path = $@".\Photos\{uniqueFileName}.txt";
            //try
            //{
            File.WriteAllBytes(path, photoFile);
            //}
            //catch (Exception)
            //{
                    
            //}
            return uniqueFileName;
        }

        public async Task<string> SavePhotoAsync(byte[] photoFile)
        {
            throw new NotImplementedException();
        }
    }
}
