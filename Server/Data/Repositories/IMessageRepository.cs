using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Data.Repositories
{
    public interface IMessageRepository
    {
        public void AddMessage(Message message);
        public Task<IEnumerable<Message>> GetMessages(AppUser sender, AppUser recipient);
    }
}