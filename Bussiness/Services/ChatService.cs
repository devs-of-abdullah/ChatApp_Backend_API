

using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Bussiness.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repo;

        public ChatService(IChatRepository repo)
        {
            _repo = repo;
        }

       
        public async Task SendPrivateMessageAsync(int senderId, int receiverId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new Exception("Message cannot be empty.");

            if (senderId == receiverId)
                throw new Exception("Cannot send messages to yourself.");

            await _repo.SendPrivateMessageAsync(senderId, receiverId, message);
        }

        public async Task<List<Message>> GetMessagesWithUserAsync(int currentUserId, int otherUserId)
        {
            if (currentUserId <= 0 || otherUserId <= 0)
                throw new Exception("Invalid user IDs.");

            return await _repo.GetMessagesWithUserAsync(currentUserId, otherUserId);
        }

        public async Task SendMessageToGroupAsync(int groupId, int senderId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new Exception("Message cannot be empty.");

            await _repo.SendMessageToGroupAsync(groupId, senderId, message);
        }

        public async Task<List<GroupMessage>> GetGroupMessagesAsync(int groupId)
        {
            if (groupId <= 0)
                throw new Exception("Invalid group ID.");

            return await _repo.GetGroupMessagesAsync(groupId);
        }

        public async Task<Group> CreateGroupAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Group name cannot be empty.");

            return await _repo.CreateGroupAsync(name);
        }

        public async Task AddUserToGroupAsync(int groupId, int userId)
        {
            if (groupId <= 0 || userId <= 0)
                throw new Exception("Invalid IDs.");

            await _repo.AddUserToGroupAsync(groupId, userId);
        }
        public async Task<List<Group>> GetAllGroupsAsync() =>  await _repo.GetAllGroupsAsync();
        
      

    }
}
