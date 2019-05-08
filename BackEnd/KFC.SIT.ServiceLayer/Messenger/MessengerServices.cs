using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Messenger
{

    /// <summary>
    /// Class that provide services for Chat Messengger's functionality
    /// </summary>
    public class MessengerServices : IMessengerServices
    {
        protected DatabaseContext _DbContext;

        /// <summary>
        /// Constructor that will accept DbContext as parameter
        /// DbContext object will be pass from Manager Layera and SaveChanges method will be called from there
        /// </summary>
        /// <param name="DbContext"></param>
        public MessengerServices(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        /// <summary>
        /// Method to save the conversation to database
        /// </summary>
        /// <param name="conversation"></param>
        public Message SaveMessageToDatabase(Message message)
        {
            message.CreatedDate = DateTime.Now;
            return _DbContext.Messages.Add(message);

        }

        /// <summary>
        /// Method to delete all messages between 2 users
        /// The chat history which hold recent contact between 2 users will be delete as well
        /// </summary>
        /// <param name="firstUserId"></param>
        /// <param name="secondUserId"></param>
        public Message DeleteMessage(int messageId)
        {
            var message = _DbContext.Messages.Where(m => m.Id == messageId).SingleOrDefault();
            if (message == null)
                return null;
            return _DbContext.Messages.Remove(message);


        }

        /// <summary>
        /// Method used to get all conversations between two users
        /// </summary>
        /// <param name="firstUserId"></param> 
        /// <param name="secondUserId"></param>
        /// <returns></returns>
        public List<Message> GetAllMessagesFromConversation(int conversationId)
        {
            return _DbContext.Messages.Where(m => m.ConversationId == conversationId).ToList();

        }

        /// <summary>
        /// Get the most recent message between users
        /// </summary>
        /// <param name="firstUserId"></param>
        /// <param name="secondUserId"></param>
        /// <returns></returns>
        public Message GetMostRecentMessageConversation(int conversationId)
        {
            return _DbContext.Messages.Where(m => (m.ConversationId == conversationId)
            && (m.CreatedDate == _DbContext.Messages.Max(m2 => m2.CreatedDate))).FirstOrDefault();
        }


        /// <summary>
        /// Method to retrieve all chat history 
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IEnumerable<Conversation> GetAllConversation(int authUserId)
        {
            return _DbContext.Conversations.Where(u => u.UserId == authUserId).AsQueryable();
        }


        public Conversation GetConversationBetweenUsers(int authUserId, int targetUserId)
        {
            return _DbContext.Conversations.Where(c => c.UserId == authUserId && c.ContactUserId == targetUserId)
                                                           .FirstOrDefault();

        }


        /// <summary>
        /// Chat History is an object that contain username and userId of the one 
        /// Method to create chat history in both messenger sender and receiver
        /// If any user does not have a chat history with matches their user id 
        /// A chat history will be created and add to that user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="targetUser"></param>
        public Conversation CreateConversation(int authUserId, int targetUserId, string targetUsername)
        {
            var conversation = _DbContext.Conversations.Where(c => c.UserId == authUserId && c.ContactUserId == targetUserId).SingleOrDefault();
            if (conversation == null)
            {
                var newConversation = new Conversation
                {

                    UserId = authUserId,
                    ContactUserId = targetUserId,
                    ContactUsername = targetUsername,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                return _DbContext.Conversations.Add(newConversation);

            }
            return conversation;
        }

        /// <summary>
        /// Method to delete chat history record between 2 users
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetUserId"></param>
        public Conversation DeleteConversation(int conversationId)
        {
            var conversation = _DbContext.Conversations.Where(c => (c.Id == conversationId)).FirstOrDefault();
            if (conversation != null)
            {
                return _DbContext.Conversations.Remove(conversation);

            }
            return null;
        }

        /// <summary>
        /// If an conversation receive a new message, this function will go to the conversation and change boolean HasNewMessage to true
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public Conversation MarkConversationRead(int conversationId)
        {
            var conversation = _DbContext.Conversations.Where(c => (c.Id == conversationId)).FirstOrDefault();
            if (conversation != null)
            {
                conversation.HasNewMessage = false;
                _DbContext.Entry(conversation).State = System.Data.Entity.EntityState.Modified;


            }
            return conversation;
        }

        /// <summary>
        /// Get conversation object from conversation Id
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public Conversation GetConversationFromId(int conversationId)
        {
            return _DbContext.Conversations.Where(c => c.Id == conversationId).FirstOrDefault();
        }

        /// <summary>
        /// Get contact user id in a conversation 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public int GetContactUserIdFromConversation(int conversationId)
        {
            return _DbContext.Conversations.Where(c => c.Id == conversationId).FirstOrDefault().ContactUserId;
        }

        /// <summary>
        /// Get contact username from a conversation 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public string GetContactUsernameFromConversation(int conversationId)
        {
            return _DbContext.Conversations.Where(c => c.Id == conversationId).FirstOrDefault().ContactUsername;
        }

        /// <summary>
        /// Method to get SignalR hub connection Id which map to userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserId(int userId)
        {
            return _DbContext.ChatConnectionMappings.Where(c => c.UserId == userId).AsEnumerable();
        }

        /// <summary>
        /// Method to check if an user is a friend
        /// </summary>
        /// <param name="authUserid"></param>
        /// <param name="targetUserId"></param>
        /// <returns></returns>
        public bool IsFriend(int authUserid, int targetUserId)
        {

            FriendRelationship fr = _DbContext.FriendRelationships.FirstOrDefault(f =>
           (f.FriendId == targetUserId && f.UserOfRelationship.Id == authUserid) ||
           (f.FriendId == authUserid && f.UserOfRelationship.Id == targetUserId)
            );

            if (fr == null)
            {
                return false;
            }
            return true;


        }


        /// <summary>
        /// Method to add a user to friend list
        /// </summary>
        /// <param name="addingUser"></param>
        /// <param name="addedUser"></param>
        public FriendRelationship AddContactFriendList(int authUserId, int targetUserId)
        {


            var fr = new FriendRelationship
            {
                FriendId = targetUserId,

                UserId = authUserId
            };
            return _DbContext.FriendRelationships.Add(fr);


        }

        /// <summary>
        /// Method return all user's friends
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IEnumerable<FriendRelationship> GetAllFriendRelationship(int authUserId)
        {

            return _DbContext.FriendRelationships.Where(f => f.UserId == authUserId).AsEnumerable();


        }

        /// <summary>
        /// Method to remove a user from a friend list
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="friendUserId"></param>
        public FriendRelationship RemoveUserFromFriendList(int authUserId, int friendUserId)
        {

            var friend = _DbContext.FriendRelationships.Where(f => (f.UserId == authUserId && f.FriendId == friendUserId)).FirstOrDefault();
            if (friend != null)
            {
                return _DbContext.FriendRelationships.Remove(friend);

            }

            else
            {
                return null;
            }

        }


    }
}