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
        public List<Conversation> GetConservationBetweenUser(String senderUserName, String receiverUserName)
        {
            
            return msServices.GetAllConservationBetweenContact(senderUserName, receiverUserName);
        }

        public Task<Conversation> GetLatestMessageBetweenUser(String senderUserName, String receiverUserName)
        {

            return Task.FromResult(msServices.GetLatestMessageBetweenContact(senderUserName, receiverUserName));
        }

        public void SendMessage(Conversation conversation)
        {
            msServices.SendMessage(conversation);
            msServices.AddContactHistory(conversation.SenderUserName, conversation.ReceiverUserName);
            DbContext.SaveChanges();

        }

        public IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserName(string username)
        {
            return msServices.GetConnectionIdWithUserName(username);
        }

        public IQueryable<MessengerContactHist> GetAllContactHistory(string senderUsername)
        {
            return msServices.GetAllContactHistory(senderUsername);
        }

        public User AddUserFriendList(int addingUserId, string addedUsername)
        {
            var addingUserObj = umServices.FindById(addingUserId);
            var addedUserObj = umServices.FindByUsername(addedUsername);
            
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

        public IEnumerable<FriendRelationship>GetFriendRelationships(string username)
        {
            try
            {
                return msServices.GetAllFriendRelationship(username);
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}