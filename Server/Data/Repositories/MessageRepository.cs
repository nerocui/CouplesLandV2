using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;
        }
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<IEnumerable<Message>> GetMessages(AppUser sender, AppUser recipient)
        {
            return await _context.Messages
                .Where(message =>
                    message.SenderId == sender.Id && message.RecipientId == recipient.Id)
                .ToListAsync();
        }
    }
}