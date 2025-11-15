
namespace Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }     
        public DateTime SentAt { get; set; } = DateTime.UtcNow;    
        public string Content { get; set; } = string.Empty;
        public PrivateMessage? PrivateMessage { get; set; }
        public GroupMessage? GroupMessage { get; set; }
    }
    public class PrivateMessage
    {
    public int Id { get; set; }
    public int MessageId { get; set; }     
    public Message? Message { get; set; }      
    public int ReceiverId { get; set; }

    }
    public class GroupMessage
    {
         public int Id { get; set; }
         public int MessageId { get; set; }       
         public Message? Message { get; set; }
         public Group? Group { get; set; }

        public int GroupId {  get; set; }

    }
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GroupMessage>? Messages { get; set; }
        public List<User>? Users { get; set;}

    }
  
}
