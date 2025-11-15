
using Bussiness;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _hub;

        public ChatController(IChatService chatService, IHubContext<ChatHub> hub)
        {
            _chatService = chatService;
            _hub = hub;
        }

        [HttpPost("private")]
        public async Task<IActionResult> SendPrivate(MessageDto dto)
        {
            await _chatService.SendPrivateMessageAsync(dto.SenderId, dto.ReceiverId, dto.Message);

            await _hub.Clients.User(dto.ReceiverId.ToString())
                .SendAsync("ReceivePrivateMessage", dto.SenderId, dto.Message);

            return Ok("Message sent.");
        }

        [HttpPost("group")]
        public async Task<IActionResult> SendGroup(GroupMessageDto dto)
        {
            await _chatService.SendMessageToGroupAsync(dto.GroupId,dto.SenderId, dto.Message);

            await _hub.Clients.Group(dto.GroupId.ToString())
                .SendAsync("ReceiveGroupMessage", dto.Message);

            return Ok("Group message sent.");
        }

        [HttpPost("group/create")]
        public async Task<IActionResult> CreateGroup(CreateGroupDto dto)
        {
            await _chatService.CreateGroupAsync(dto.Name);

            return Ok("Group created.");
        }

        [HttpGet("messages/")]
        public async Task<IActionResult> GetMessages(int currentId, int otherUserId)
        {
           var msgs =  await _chatService.GetMessagesWithUserAsync(currentId, otherUserId);

            return Ok(msgs);
        }

        [HttpGet("group/messages/")]
        public async Task<IActionResult> GetGroupMessages(int groupId)
        {
           var msgs =  await _chatService.GetGroupMessagesAsync(groupId);

            return Ok(msgs);
        }
        [HttpGet("groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _chatService.GetAllGroupsAsync();
            return Ok(result);
        }

    }
}
