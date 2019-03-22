using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.Messenger
{
    public class MessengerServices
    {
        public List<User>GetAllChatContacts(String currentUsername)
        {
            using (var db = new DatabaseContext())
            {
                return db.Users.Where(u => u.UserName != currentUsername)
                                     .ToList();
            }
        }

        public List<Conversation>GetAllConservationBetweenContact(string senderUserName, string receiverUserName)
        {
            using (var db = new DatabaseContext())
            {
                return db.Conservations.
                              Where(c => (c.ReceiverUserName == receiverUserName
                                  && c.SenderUserName == senderUserName) ||
                                  (c.ReceiverUserName == senderUserName
                                  && c.SenderUserName == receiverUserName))
                              .OrderBy(c => c.CreatedDate)
                              .ToList();
            }
        }

        public void SendMessage(Conversation conversation)
        {

            using (var db = new DatabaseContext())
            {
                db.Conservations.Add(conversation);
                db.SaveChanges();
            }
        }
    }
}