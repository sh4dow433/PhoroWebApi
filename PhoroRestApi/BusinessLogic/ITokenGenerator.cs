using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.BusinessLogic
{
    public interface ITokenGenerator
    {
        string GenerateToken(int id, bool isSeller, bool isAdmin);
    }
}
