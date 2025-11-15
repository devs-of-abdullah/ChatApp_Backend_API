using Microsoft.AspNetCore.SignalR;

namespace API
{
    public class ChatHub : Hub
    {
        public async Task SendPrivate(int senderId, int receiverId, string message)
        {
            await Clients.User(receiverId.ToString())
                .SendAsync("ReceivePrivateMessage", senderId, message);
        }

        public async Task SendGroup(int groupId, string message)
        {
            await Clients.Group(groupId.ToString())
                .SendAsync("ReceiveGroupMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId!);
            }

            await base.OnConnectedAsync();
        }
    }
}
