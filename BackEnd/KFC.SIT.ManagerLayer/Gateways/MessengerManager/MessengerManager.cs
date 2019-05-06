using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using ServiceLayer.Messenger;
using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static ServiceLayer.ServiceExceptions.MessengerServiceException;

namespace WebAPI.Gateways.Messenger
{
    /// <summary>
    /// The messengere manager that contains functionalities for message feature 
    /// </summary>
    public class MessengerManager
    {
        // Messenger services that will take responbility of sending , retrieveing, deleting messages, add and remove friend
        private IMessengerServices _msServices;

        // Usermanager services that will be used to retrieve user's information
        private IUserManagementServices _umServices;

        protected DatabaseContext _DbContext;
        public MessengerManager()
        {
            _DbContext = new DatabaseContext();
            _msServices = new MessengerServices(_DbContext);
            _umServices = new UserManagementServices(_DbContext);
        }

        public IEnumerable<Message> GetMessageInConversation(int conversationId)
        {
            _msServices.MarkConversationRead(conversationId);
            _DbContext.SaveChanges();
            return _msServices.GetAllMessagesFromConversation(conversationId).AsEnumerable();

        }


        /// <summary>
        /// Method to get most recent message between current authenticated user and other user
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        public Message GetRecentMessageBetweenUser(int conversationId)
        {
            return _msServices.GetMostRecentMessageConversation(conversationId);

        }

        public Conversation GetConversationBetweenUsers(int authUserId, int targetUserId)
        {
            return _msServices.GetConversationBetweenUsers(authUserId, targetUserId);

        }

        /// <summary>
        /// Method to send a message to another user
        /// Then messenger services will try to create a chat history first
        /// Then save the message to database. Dbcontext will call SaveChanges after  those 2 operations finish.
        /// </summary>
        /// <param name="conversation"></param>
        public Message SaveMessageToDatabase(Message message, int authUserId, int contactUserId)
        {
            // On the auth user's side, try to check if there is a conversation with the contact
            Conversation authUserConversation = _msServices.GetConversationBetweenUsers(authUserId, contactUserId);

            // On the contact user's side, try to check if there is a conversation with the contact
            Conversation contactUserConversation = _msServices.GetConversationBetweenUsers(contactUserId, authUserId);

            // Retrieve auth user name using  auth user id
            var authUsername = _umServices.FindById(authUserId).UserName;

            // Retrieve contact user name using contact Id
            var contactUsername = _umServices.FindById(contactUserId).UserName;

            // If there is no conversation with the contact on auth user'side 
            if (authUserConversation == null)
            {
                // Create one 
                authUserConversation = _msServices.CreateConversation(authUserId, contactUserId, contactUsername);

                // Temporary assign a negative conversation id to avoid conflict when saving both coversations at same time 
                authUserConversation.Id = -1;
            }

            if (contactUserConversation == null)
            {
                contactUserConversation = _msServices.CreateConversation(contactUserId, authUserId, authUsername);
                contactUserConversation.Id = -2;
            }

            // Create a message which will be saved in auth user's conversation 
            var authUserMessage = new Message
            {
                // Refer the message to auth user 's conversation 
                ConversationId = authUserConversation.Id,
                MessageContent = message.MessageContent,
                OutgoingMessage = true,
                CreatedDate = DateTime.Now

            };

            // Create a message which will be saved in contact user's conversation 
            var targetUserMessage = new Message
            {
                ConversationId = contactUserConversation.Id,
                MessageContent = message.MessageContent,
                OutgoingMessage = false,
                CreatedDate = DateTime.Now

            };

            // Mark the conversation of contact user that has new message
            contactUserConversation.HasNewMessage = true;

            //Update time
            authUserConversation.ModifiedDate = DateTime.Now;
            contactUserConversation.ModifiedDate = DateTime.Now;

            // Save both messages to message table 
            _msServices.SaveMessageToDatabase(authUserMessage);
            _msServices.SaveMessageToDatabase(targetUserMessage);

            _DbContext.SaveChanges();
            return authUserMessage;

        }

        /// <summary>
        /// Method to delete conversation between current authenticated user and other user
        /// This just delete the covnersation owned by auth user. The conversation own by other use will not be deleted
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="targetUserId"></param>
        public Conversation DeleteConversation(int conversationid)
        {
            var conversation = _msServices.DeleteConversation(conversationid);
            if (conversation != null)
            {
                _DbContext.SaveChanges();
                return conversation;
            }
            return null;
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
        public IEnumerable<Conversation> GetAllConversations(int authUserId)
        {

            return _msServices.GetAllConversation(authUserId);
        }


        /// <summary>
        /// Method get contact userId from a conversation
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public int GetContactUserIdFromConversation(int conversationId)
        {
            return _msServices.GetContactUserIdFromConversation(conversationId);
        }

        public string GetContactUsernameFromConversation(int conversationId)
        {
            return _DbContext.Conversations.Where(c => c.Id == conversationId).FirstOrDefault().ContactUsername;
        }

        public Conversation GetConversationFromId(int conversationId)
        {
            return _DbContext.Conversations.Where(c => c.Id == conversationId).FirstOrDefault();
        }


        /// <summary>
        /// Method to add a user to friend list
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="targetUserId"></param>
        /// <returns></returns>
        public FriendRelationship AddUserFriendList(int authUserId, string targetUserId)
        {
            //var authUser = _umServices.FindById(authUserId);
            var targetUser = _umServices.FindByUsername(targetUserId);

            if (targetUser == null)
            {
                throw new MessageReceiverNotFoundException();
            }

            var fs = _msServices.AddContactFriendList(authUserId, targetUser.Id);


            _DbContext.SaveChanges();
            return fs;




        }

        /// <summary>
        /// Method to get all friend relationships
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IEnumerable<FriendRelationship> GetAllFriendRelationships(int authUserId)
        {

            return _msServices.GetAllFriendRelationship(authUserId);

        }

        public FriendRelationship RemoveUserFromFriendList(int authUserId, int friendUserId)
        {
            var fs = _msServices.RemoveUserFromFriendList(authUserId, friendUserId);
            _DbContext.SaveChanges();
            return fs;
        }
    }
}