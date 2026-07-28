using Microsoft.AspNetCore.SignalR;
using SignalRWebpack.Data;

namespace SignalRWebpack.Hubs;

public class ChatHub : Hub
{
    private readonly AppDbContext _db;

    public ChatHub(AppDbContext db)
    {
        _db = db;
    }

    public async Task NewMessage(long username, string message)
    {
        // OTel trace activity
        using var activity = ChatDiagnostics.ChatActivitySource.StartActivity("SignalR NewMessage");
        activity?.SetTag("chat.username", username);
        activity?.SetTag("chat.message_length", message.Length);

        // save msg to sqllite
        _db.ChatLogs.Add(new ChatLog { Username = username.ToString(), Message = message });
        await _db.SaveChangesAsync();

        // increment metrics counter
        ChatDiagnostics.MessageCounter.Add(1, new KeyValuePair<string, object?>("username", username.ToString()));

        // broadcast msg to all connected browser windows
        await Clients.All.SendAsync("messageReceived", username, message);
    }
}