using DataAccessLayer;
using DataAccessLayer.Models.Messenger;
using ServiceLayer.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ManagerLayer.Gateways.Messenger
{
    public class MessengerGateway
    {
        private MessengerServices ms;
        protected DatabaseContext DbContext;
        public MessengerGateway()
        {
            DbContext = new DatabaseContext();
            ms = new MessengerServices(DbContext);
        }
        public Task<List<Conversation>> GetConservationBetweenUser(String senderUserName, String receiverUserName)
        {
            
            return Task.FromResult(ms.GetAllConservationBetweenContact(senderUserName, receiverUserName));
        }

        public void SendMessage(Conversation conversation)
        {
            ms.SendMessage(conversation);
            ms.AddContactHistory(conversation.SenderUserName, conversation.ReceiverUserName);
            DbContext.SaveChanges();

        }

        public Task<IQueryable<MessengerContactHist>> GetAllContactHistory(string senderUsername)
        {
            return ms.GetAllContactHistory(senderUsername);
        }
    }
}