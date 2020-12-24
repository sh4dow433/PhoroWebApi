using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public interface IPhotoSaver
    {
        string SavePhoto(byte[] photoFile);
        Task<string> SavePhotoAsync(byte[] photoFile);// returns diskName
    }
}
