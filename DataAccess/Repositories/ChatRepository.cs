using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

    
      
        public async Task SendPrivateMessageAsync(int senderId, int receiverId, string message)
        {
            var msg = new Message
            {
                SenderId = senderId,
                Content = message
            };

            await _context.Messages.AddAsync(msg);
            await _context.SaveChangesAsync();

            var privateMsg = new PrivateMessage
            {
                MessageId = msg.Id,
                ReceiverId = receiverId,
            };

            await _context.PrivateMessages.AddAsync(privateMsg);
            await _context.SaveChangesAsync();
        }

     
        public async Task<List<Message>> GetMessagesWithUserAsync(int currentUserId, int otherUserId)
        {
            return await _context.Messages
                .Include(m => m.PrivateMessage)
                .Where(m =>
                    m.PrivateMessage != null &&
                    (
                        (m.SenderId == currentUserId && m.PrivateMessage.ReceiverId == otherUserId) ||
                        (m.SenderId == otherUserId && m.PrivateMessage.ReceiverId == currentUserId)
                    )
                )
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

  
        public async Task SendMessageToGroupAsync(int groupId, int senderId, string message)
        {
            var msg = new Message
            {
                SenderId = senderId,
                Content = message
            };

            await _context.Messages.AddAsync(msg);
            await _context.SaveChangesAsync();

            var gm = new GroupMessage
            {
                MessageId = msg.Id,
                GroupId = groupId,
            };

            await _context.GroupMessages.AddAsync(gm);
            await _context.SaveChangesAsync();
        }

     
        public async Task<List<GroupMessage>> GetGroupMessagesAsync(int groupId)
        {
            return await _context.GroupMessages
                .Include(gm => gm.Message)
                .Where(gm => gm.GroupId == groupId)
                .OrderBy(gm => gm.Message!.SentAt)
                .ToListAsync();
        }

        public async Task<Group> CreateGroupAsync(string name)
        {
            var group = new Group { Name = name };

            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return group;
        }

      
        public async Task AddUserToGroupAsync(int groupId, int userId)
        {
            var group = await _context.Groups
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
                throw new Exception("Group not found");

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            if (group.Users == null)
                group.Users = new List<User>();

            if (!group.Users.Any(u => u.Id == userId))
                group.Users.Add(user);

            await _context.SaveChangesAsync();
        }

   

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            var groups = await _context.Groups.ToListAsync();
            return groups;
        }
    }
}
