using DataAccessLayer.Models.Messenger;
using ServiceLayer.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerLayer.Gateways.Messenger
{
    public class MessengerGateway
    {
        MessengerServices ms;
        public MessengerGateway()
        {
             ms = new MessengerServices();
        }
        public List<Conversation>GetConservationBetweenUser(String senderUserName, String receiverUserName)
        {
            
            return ms.GetAllConservationBetweenContact(senderUserName, receiverUserName);
        }

        public void SendMessage(Conversation conversation)
        {
            ms.SendMessage(conversation);
        }
    }
}