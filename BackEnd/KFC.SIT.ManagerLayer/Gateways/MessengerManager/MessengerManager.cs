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
    public class MessengerManager
    {
        private MessengerServices msServices;
        private UserManagementServices umServices;
        protected DatabaseContext DbContext;
        public MessengerManager()
        {
            DbContext = new DatabaseContext();
            msServices = new MessengerServices(DbContext);
            umServices = new UserManagementServices(DbContext);
        }
        public List<Conversation> GetConversationBetweenUser(int senderId, int receiverId)
        {
            
            
            return msServices.GetAllMessagesBetweenUsers(senderId, receiverId);
        }


        public void DeleteConversation(int senderId, int receiverId)
        {
            msServices.DeleteChatHistoryRecord(senderId, receiverId);
            msServices.DeleteMessageFromDatabase(senderId, receiverId);
            
        }

        public Task<Conversation> GetLatestMessageBetweenUser(int senderId, int receiverId)
        {
            return Task.FromResult(msServices.GetMostRecentMessageBetweenUsers(senderId, receiverId));
        }



       

        public void SendMessage(Conversation conversation)
        {

            var sender = umServices.FindById(conversation.SenderId);
            var receiver = umServices.FindById(conversation.ReceiverId);
            if(receiver != null)
            {
                msServices.SaveMessageToDatabase(conversation);
                msServices.AddContactHistory(sender, receiver);
                DbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("User with username does not exist to receive message ");
            }
           

        }




        public void DeleteChatMessageBetweenUsers(int authUserId, int targetUserId)
        {
            var chatHistory = msServices.GetContactHistoryBetweenUsers(authUserId, targetUserId);

            if(chatHistory.DeleteByReceiver == true)
            {
                msServices.DeleteMessageFromDatabase(authUserId, targetUserId);
                msServices.DeleteChatHistoryRecord(authUserId, targetUserId);
            }

            else
            {
                msServices.DeleteChatHistoryRecord(authUserId, targetUserId);
            }

            DbContext.SaveChanges();

        }

        public IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserId(int userId)
        {
            return msServices.GetConnectionIdWithUserId(userId);
        }

        public IQueryable<ChatHistory> GetAllContactHistory(int senderId)
        {
           
            return msServices.GetAllContactHistory(senderId);
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

        public IEnumerable<FriendRelationship>GetFriendRelationships(int userId)
        {
     
            try
            {
                return msServices.GetAllFriendRelationship(userId);
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void RemoveUserFromFriendList(int authUserId, int friendUserId)
        {

            try
            {
                msServices.RemoveUserFromFriendList(authUserId, friendUserId);
                DbContext.SaveChanges();
            }

            catch(ArgumentException exception)
            {
                throw (exception);
            }
        
            


        }
    }
}