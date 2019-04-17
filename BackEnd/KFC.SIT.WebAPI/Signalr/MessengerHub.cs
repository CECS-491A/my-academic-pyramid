using DataAccessLayer;
using DataAccessLayer.Models.Messenger;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ServiceLayer.Messenger;
using System.Threading.Tasks;

namespace WebAPI.Signal
{
    [HubName("MessengerHub")]
    public class MessengerHub : Hub
    {
        private readonly static ChatConnectionMapping<string> _connections =
            new ChatConnectionMapping<string>();


        
        public async Task Send(string message)
        {
            await Clients.All.InvokeAsync("SendMessage", message);

        }

        public ChatConnectionMapping<string> GetConnectionMapping()
        {
            return _connections;
        }

        public override Task OnConnected()
        {
            string token = Context.QueryString["jwt"];
            //_connections.Add(token, Context.ConnectionId);
            
            using (var db = new DatabaseContext())
            {
                var connection = db.ChatConnectionMappings.Find(Context.ConnectionId);
                if(connection == null)
                {
                    ChatConnectionMapping newConnection = new ChatConnectionMapping
                    {
                        Username = token,
                        ConnectionId = Context.ConnectionId
                    };
                    db.ChatConnectionMappings.Add(newConnection);
                }

                db.SaveChanges();
            }
                return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string token = Context.QueryString["jwt"];

            //_connections.Remove(token, Context.ConnectionId);
            using (var db = new DatabaseContext())
            {
                var connection = db.ChatConnectionMappings.Find(Context.ConnectionId);
                if (connection != null)
                {
                    db.ChatConnectionMappings.Remove(connection);
                }
                db.SaveChanges();
            }

            return base.OnDisconnected(stopCalled);
        }

        //public override Task OnReconnected()
        //{
        //    string token = Context.QueryString["jwt"];

        //    using (var db = new DatabaseContext())
        //    {
        //        var connection = db.ChatConnectionMappings.Find(token);
        //        if (connection == null)
        //        {
        //            ChatConnectionMapping newConnection = new ChatConnectionMapping
        //            {
        //                Username = token,
        //                ConnectionId = Context.ConnectionId
        //            };
        //            db.ChatConnectionMappings.Add(newConnection);
        //            db.SaveChanges();
        //        }
                
        //    }

        //    return base.OnReconnected();
        //}

        public string GetConnectionString(string username)
        {
            using (var db = new DatabaseContext())
            {
                var connection = db.ChatConnectionMappings.Find(username);
                if (connection != null)
                {
                    return connection.ConnectionId;
                }

                else
                {
                    return null;
                }


            }

        }



    }
    

    
}