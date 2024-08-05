using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class MessageImplementation : MessageInterface
    {
        private readonly AppDbContext _context;

        public MessageImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> GetMessageById(int messageId)
        {
            return await _context.Messages.FindAsync(messageId);
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessage(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessage(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
