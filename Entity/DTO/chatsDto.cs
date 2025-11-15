
namespace Entities
{

    public class MessageDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; } = string.Empty;


    }
    public class GroupMessageDto
    {   public int SenderId { get; set; }
        public int GroupId { get; set; }
        public string Message { get; set; } = string.Empty;
     

    }
    public class CreateGroupDto
    {
        public string Name { get; set; } = string.Empty;
    }
  
}
