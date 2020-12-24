using PhoroRestApi.Models;

namespace PhoroRestApi.DataAccess
{
    public interface IUserAuth
    {
        User Login(string username, string password);
        bool Register(User user, string password);
        bool UpdatePassword(User user, string password);
    }
}