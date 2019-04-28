using DataAccessLayer;
using DataAccessLayer.Models.Messenger;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ServiceLayer.Messenger;
using System;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace WebAPI.Signalr
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
            string authUserId = Context.QueryString["authUserId"];
            //_connections.Add(token, Context.ConnectionId);
            
            using (var db = new DatabaseContext())
            {
                var connection = db.ChatConnectionMappings.Find(Context.ConnectionId);
                if(connection == null)
                {
                    if(authUserId != "undefined" || authUserId !="")
                    {
                        ChatConnectionMapping newConnection = new ChatConnectionMapping
                        {

                            UserId = Convert.ToInt32(authUserId),
                            ConnectionId = Context.ConnectionId
                        };
                        db.ChatConnectionMappings.Add(newConnection);
                    }
                    try
                    {
                        db.SaveChanges();
                    }

                    catch(DbUpdateException ex)
                    {
                        // Do something here
                    }

                }
                
               
            }
                return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string authUserId = Context.QueryString["authUserId"];

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