using PhoroRestApi.DataAccess.IRepositories;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.DataAccess.Repositories
{
    public class MessagesRepository : Repository<Message>, IMessagesRepository
    {
        public MessagesRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Message> GetAllFromId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Messages
                .Where(m => m.FromUserId == id)
                .OrderByDescending(m => m.CreationDate)
                .ToList();
        }

        public IEnumerable<Message> GetAllFromIdToId(int fromId, int toId)
        {
            if (fromId < 0 || toId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Messages
                .Where(m => m.FromUserId == fromId && m.ToUserId == toId)
                .OrderByDescending(m => m.CreationDate)
                .ToList();
        }

        public IEnumerable<Message> GetAllToId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Messages
                .Where(m => m.ToUserId == id)
                .OrderByDescending(m => m.CreationDate)
                .ToList();
        }

        public Message GetLastFromId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Messages
                .Where(m => m.FromUserId == id)
                .OrderByDescending(m => m.CreationDate)
                .FirstOrDefault();
        }
        public Message GetLastFromIdToId(int fromId, int toId)
        {
            if (fromId < 0 || toId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Messages
                .Where(m => m.FromUserId == fromId && m.ToUserId == toId)
                .OrderByDescending(m => m.CreationDate)
                .FirstOrDefault();
        }

        public Message GetLastToId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _dbContext.Messages
                .Where(m => m.ToUserId == id)
                .OrderByDescending(m => m.CreationDate)
                .FirstOrDefault();
        }
    }
}
