using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace WebAPI.Signal
{
    [HubName("MessengerHub")]
    public class MessengerHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.InvokeAsync("SendMessage", message);

        }
    }
}