using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface MessageInterface
    {
        Task<IEnumerable<Message>> GetAllMessages();
        Task<Message> GetMessageById(int messageId);
        Task AddMessage(Message message);
        Task UpdateMessage(Message message);
        Task DeleteMessage(int messageId);
    }
}
