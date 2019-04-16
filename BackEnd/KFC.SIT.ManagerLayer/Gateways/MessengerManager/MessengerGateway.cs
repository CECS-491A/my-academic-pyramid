using DataAccessLayer;
using DataAccessLayer.Models.Messenger;
using ServiceLayer.Messenger;
using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI.Gateways.Messenger
{
    public class MessengerGateway
    {
        private MessengerServices msServices;
        private UserManagementServices umServices;
        protected DatabaseContext DbContext;
        public MessengerGateway()
        {
            DbContext = new DatabaseContext();
            msServices = new MessengerServices(DbContext);
            umServices = new UserManagementServices(DbContext);
        }
        public Task<List<Conversation>> GetConservationBetweenUser(String senderUserName, String receiverUserName)
        {
            
            return Task.FromResult(msServices.GetAllConservationBetweenContact(senderUserName, receiverUserName));
        }

        public void SendMessage(Conversation conversation)
        {
            msServices.SendMessage(conversation);
            msServices.AddContactHistory(conversation.SenderUserName, conversation.ReceiverUserName);
            DbContext.SaveChanges();

        }

        public Task<IQueryable<MessengerContactHist>> GetAllContactHistory(string senderUsername)
        {
            return msServices.GetAllContactHistory(senderUsername);
        }

        public User AddUserFriendList(int addingUserId, int addedUserId)
        {
            var addingUserObj = umServices.FindById(addingUserId);
            var addedUserObj = umServices.FindById(addedUserId);

            try
            {
                msServices.AddContactFriendList(addingUserObj, addedUserObj);
                DbContext.SaveChanges();
                return addedUserObj;
            }

            catch (Exception exception)
            {
                throw exception;
            }
           
        }
    }
}