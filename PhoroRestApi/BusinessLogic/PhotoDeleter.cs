using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public class PhotoDeleter : IPhotoDeleter
    {
        //returns true if file was deleted
        public bool DeletePhoto(string diskName)
        {
            string path = $@".\Photos\{diskName}.txt";

            //check if the file exists
            if (File.Exists(path) == false)
            {
                return false;
            }

            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
