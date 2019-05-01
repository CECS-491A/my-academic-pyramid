using DataAccessLayer;
using DataAccessLayer.Models;
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
        private MessengerServices _msServices;
        private UserManagementServices _umServices;
        protected DatabaseContext _DbContext;
        public MessengerManager()
        {
            _DbContext = new DatabaseContext();
            _msServices = new MessengerServices(_DbContext);
            _umServices = new UserManagementServices(_DbContext);
        }


        /// <summary>
        /// Method to get messages between current authenticated user and other user
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="targetUserId"></param>
        /// <returns></returns>
        public List<Conversation> GetConversationBetweenUser(int authUserId, int targetUserId)
        {
            
            return _msServices.GetAllMessagesBetweenUsers(authUserId, targetUserId);
        }


        /// <summary>
        /// Method to get most recent message between current authenticated user and other user
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        public Conversation GetRecentMessageBetweenUser(int senderId, int receiverId)
        {
            return _msServices.GetMostRecentMessageBetweenUsers(senderId, receiverId);
        }


        /// <summary>
        /// Method to send a message to another user
        /// First check if the sender and receive exists
        /// Then messenger services will try to create a chat history first
        /// Then save the message to database. Dbcontext will call SaveChanges after  those 2 operations finish.
        /// </summary>
        /// <param name="conversation"></param>
        public void SendMessageToUser(Conversation conversation)
        {

            var sender = _umServices.FindById(conversation.SenderId);
            var receiver = _umServices.FindById(conversation.ReceiverId);
            if(receiver != null)
            {
                _msServices.CreateChatHistory(sender, receiver);
                _msServices.SaveMessageToDatabase(conversation);
               
                _DbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("User with username does not exist to receive message ");
            }
           

        }


        /// <summary>
        /// Method to delete messages between current authenticated user and other user
        /// First, delete the chat history record in authenticated user,Then, try to look for the chat history in target user
        /// If no chat history record exist in target user's side, all messages between users will be deleted
        /// If a chat history record exist in target user, we will keep all messages from the date time the chat history was created
        /// Which means a user can only read messages which are created after the their chat history is created
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="targetUserId"></param>
        public void DeleteChatMessageBetweenUsers(int authUserId, int targetUserId)
        {
            _msServices.DeleteChatHistoryRecord(authUserId, targetUserId);
            
            var chatHistoryFromReceiverSide = _msServices.GetContactHistoryBetweenUsers(targetUserId, authUserId);

            if(chatHistoryFromReceiverSide == null )
            {
                _msServices.DeleteMessageFromDatabase(authUserId, targetUserId, DateTime.Now);
            }
            else
            {
                _msServices.DeleteMessageFromDatabase(authUserId, targetUserId, chatHistoryFromReceiverSide.ContactTime);

            }

            _DbContext.SaveChanges();

        }


        /// <summary>
        /// Method to get SignalR connectionId with userId
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserId(int authUserId)
        {
            return _msServices.GetConnectionIdWithUserId(authUserId);
        }


        /// <summary>
        /// Method to get all chat history 
        /// </summary>
        /// <param name="senderId"></param>
        /// <returns></returns>
        public IQueryable<ChatHistory> GetAllContactHistory(int authUserId)
        {
           
            return _msServices.GetAllChatHistory(authUserId);
        }


        /// <summary>
        /// Method to get a chat history between 2 users
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="secondUserId"></param>
        /// <returns></returns>
        public ChatHistory GetChatHistoryBetweenUsers(int authUserId, int targetUserId)
        {
            return _msServices.GetContactHistoryBetweenUsers(authUserId, targetUserId);
        }


        /// <summary>
        /// Method to add a user to friend list
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="targetUserId"></param>
        /// <returns></returns>
        public Account AddUserFriendList(int authUserId, string targetUserId)
        {
            var authUser = _umServices.FindById(authUserId);
            var targetUser = _umServices.FindByUsername(targetUserId);
            
            try
            {
                _msServices.AddContactFriendList(authUser, targetUser);
                _DbContext.SaveChanges();
                return targetUser;
            }

            catch (Exception exception)
            {
                throw exception;
            }
           
        }

        /// <summary>
        /// Method to get all friend relationships
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IEnumerable<FriendRelationship>GetAllFriendRelationships(int authUserId)
        {
     
            try
            {
                return _msServices.GetAllFriendRelationship(authUserId);
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
                _msServices.RemoveUserFromFriendList(authUserId, friendUserId);
                _DbContext.SaveChanges();
            }

            catch(ArgumentException exception)
            {
                throw (exception);
            }

        }
    }
}