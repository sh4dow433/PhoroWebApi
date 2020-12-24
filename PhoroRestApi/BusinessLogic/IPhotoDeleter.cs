using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public interface IPhotoDeleter
    {
        bool DeletePhoto(string diskName);
    }
}
