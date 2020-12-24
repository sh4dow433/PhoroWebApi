using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.IRepositories
{
    public interface IMessagesRepository : IRepository<Message>
    {
        IEnumerable<Message> GetAllFromId(int id);
        IEnumerable<Message> GetAllToId(int id);
        IEnumerable<Message> GetAllFromIdToId(int fromId, int toId);

        Message GetLastFromId(int id);
        Message GetLastToId(int id);
        Message GetLastFromIdToId(int fromId, int toId);
    }
}
