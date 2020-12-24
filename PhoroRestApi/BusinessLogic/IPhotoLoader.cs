using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public interface IPhotoLoader
    {
        string LoadPhoto(string diskName);
        Task<byte[]> LoadPhotoAsync(string diskName);
    }
}
