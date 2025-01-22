using Microsoft.AspNetCore.SignalR;

namespace ConnectFour.Hubs;

public class ChatHub : Hub {
    public async Task SendMessage(string GameRoom, string Message){
        await Clients.All.SendAsync("ReceiveMessage", GameRoom, Message);
    }
}
