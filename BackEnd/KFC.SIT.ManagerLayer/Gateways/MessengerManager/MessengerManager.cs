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




        public IEnumerable<Message> GetMessageInConversation(int conversationId)
        {
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
        /// First check if the sender and receive exists
        /// Then messenger services will try to create a chat history first
        /// Then save the message to database. Dbcontext will call SaveChanges after  those 2 operations finish.
        /// </summary>
        /// <param name="conversation"></param>
        public Message SaveMessageToDatabase(Message message, int authUserId, int contactUserId)
        {

            Conversation authUserConversation = _msServices.GetConversationBetweenUsers(authUserId,contactUserId);
            Conversation targetUserConversation;
            var authUsername  = _umServices.FindById(authUserId).UserName;
            var contactUsername = _umServices.FindById(contactUserId).UserName;

            if (authUserConversation == null)
            {
                authUserConversation = _msServices.CreateConversation(authUserId, contactUserId, contactUsername);
                
                authUserConversation.Id = -1;
                
                targetUserConversation = _msServices.GetConversationBetweenUsers(contactUserId, authUserId);
                
                if (targetUserConversation == null)
                {
                    targetUserConversation = _msServices.CreateConversation(contactUserId, authUserId, authUsername);
                    targetUserConversation.Id = -2;
                }
                
            }

            else
            {
                targetUserConversation = _msServices.GetConversationBetweenUsers(contactUserId,authUserId);
                if (targetUserConversation == null)
                {
                    targetUserConversation = _msServices.CreateConversation(contactUserId, authUserId, contactUsername);
                    targetUserConversation.Id = -2;
                }
            }


            var authUserMessage = new Message
                {
                    
                    ConversationId = authUserConversation.Id,
                    MessageContent = message.MessageContent,
                    OutgoingMessage = true,
                    CreatedDate = DateTime.Now

                };
                

                var targetUserMessage = new Message
                {
                    ConversationId = targetUserConversation.Id,
                    MessageContent = message.MessageContent,
                    OutgoingMessage = false,
                    CreatedDate = DateTime.Now

                };

            authUserConversation.HasNewMessage = true;
            targetUserConversation.HasNewMessage = true;



            _msServices.SaveMessageToDatabase(authUserMessage);
            _msServices.SaveMessageToDatabase(targetUserMessage);

            try
            {
                _DbContext.SaveChanges();
                return message;

            }

            catch (DbUpdateException ex)
            {
                if(ex.InnerException ==  null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                   
                }

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
        public Conversation DeleteConversation(int conversationid)
        {
            var conversation = _msServices.DeleteConversation(conversationid);   
            if(conversation !=null)
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
            return _msServices.GetContactUserIdFromConversation( conversationId);
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
            var authUser = _umServices.FindById(authUserId);
            var targetUser = _umServices.FindByUsername(targetUserId);

           var fs = _msServices.AddContactFriendList(authUserId, targetUser.Id);

            if (fs != null)
            {
                try
                {

                    _DbContext.SaveChanges();
                    return fs;
                }

                catch (DbUpdateException ex)
                {
                    if (ex.InnerException == null)
                    {
                        throw ex.InnerException;
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

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

            try
            {
                var fs = _msServices.RemoveUserFromFriendList(authUserId, friendUserId);
                _DbContext.SaveChanges();
                return fs;
               
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException == null)
                {
                    throw ex.InnerException;
                }
                else
                {
                   if (ex.InnerException == null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
                }
            }
            return null;

        }
    }
}