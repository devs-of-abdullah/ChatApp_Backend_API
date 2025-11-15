using Entities;

namespace DataAccess
{
    public interface IChatRepository
    {
        Task SendPrivateMessageAsync(int senderId, int receiverId, string message);
        Task<List<Message>> GetMessagesWithUserAsync(int currentUserId, int otherUserId);

        Task SendMessageToGroupAsync(int groupId, int senderId, string message);
        Task<List<GroupMessage>> GetGroupMessagesAsync(int groupId);

        Task<Group> CreateGroupAsync(string name);
        Task AddUserToGroupAsync(int groupId, int userId);
        Task<List<Group>> GetAllGroupsAsync();

    }
}
