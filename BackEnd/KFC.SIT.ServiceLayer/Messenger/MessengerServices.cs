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
    public class MessengerServices
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
        /// Method used to get all conversations between two users
        /// </summary>
        /// <param name="firstUserId"></param> 
        /// <param name="secondUserId"></param>
        /// <returns></returns>
        public List<Conversation> GetAllMessagesBetweenUsers(int firstUserId, int secondUserId)
        {
            using (var db = new DatabaseContext())
            {
                return db.Conversations.
                              Where(c => (c.SenderId == firstUserId && c.ReceiverId == secondUserId)
                                         || c.SenderId == secondUserId && c.ReceiverId == firstUserId)
                              .OrderBy(c => c.CreatedDate)
                              .ToList();
            }
        }

        /// <summary>
        /// Get the most recent message between users
        /// </summary>
        /// <param name="firstUserId"></param>
        /// <param name="secondUserId"></param>
        /// <returns></returns>
        public Conversation GetMostRecentMessageBetweenUsers(int firstUserId, int secondUserId)
        {
            using (var db = new DatabaseContext())
            {

                return db.Conversations.
                              Where(c => ((c.ReceiverId == secondUserId
                                  && c.SenderId == firstUserId) ||
                                  (c.ReceiverId == firstUserId
                                  && c.SenderId == secondUserId))
                                  && c.CreatedDate == db.Conversations.Max(m => m.CreatedDate)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Method to save the conversation to database
        /// </summary>
        /// <param name="conversation"></param>
        public void SaveMessageToDatabase(Conversation conversation)
        {

            using (var db = new DatabaseContext())
            {
                db.Conversations.Add(conversation);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method to delete all messages between 2 users
        /// The chat history which hold recent contact between 2 users will be delete as well
        /// </summary>
        /// <param name="firstUserId"></param>
        /// <param name="secondUserId"></param>
        public void DeleteMessageFromDatabase(int firstUserId, int secondUserId,DateTime TimeToDeleteBackward)
        {
            _DbContext.Conversations.RemoveRange(_DbContext.Conversations
                .Where(c => ((c.SenderId == firstUserId && c.ReceiverId == secondUserId)
                        || (c.SenderId == secondUserId && c.ReceiverId == firstUserId))
                        && c.CreatedDate <= TimeToDeleteBackward));
        }


        /// <summary>
        /// Method to retrieve all chat history 
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IQueryable<ChatHistory> GetAllChatHistory(int authUserId)
        {
            return _DbContext.ChatHistory.Where(u => u.UserId == authUserId).AsQueryable();
        }


        public ChatHistory GetContactHistoryBetweenUsers(int firstUserId, int secondUserId)
        {
            return _DbContext.ChatHistory.Where(u => u.UserId == firstUserId && u.ContactId == secondUserId)
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
        public void CreateChatHistory(Account authUser, Account targetUser)
        {
            var foundChatHistoryFromSender = GetContactHistoryBetweenUsers(authUser.Id, targetUser.Id);
            if(foundChatHistoryFromSender == null)
            {
                var chatHistoryForSender = new ChatHistory
                {
                    UserId = authUser.Id,
                    ContactId = targetUser.Id,
                    ContactUsername = targetUser.UserName,
                    ContactTime = DateTime.Now
                };

                _DbContext.ChatHistory.Add(chatHistoryForSender);

            }
            
            var foundChatHistoryFromReceiver = GetContactHistoryBetweenUsers(targetUser.Id, authUser.Id);

            if (foundChatHistoryFromReceiver == null)
            {
                var chatHistoryForReceiver = new ChatHistory
                {
                    UserId = targetUser.Id,
                    ContactId = authUser.Id,
                    ContactUsername = authUser.UserName,
                    ContactTime = DateTime.Now
                };

                _DbContext.ChatHistory.Add(chatHistoryForReceiver);

            }

   
        }

        /// <summary>
        /// Method to delete chat history record between 2 users
        /// The actual messages will not be delete. 
        /// This method should be combine with DeleteMessageFromDatabase method to completely delete messages
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetUserId"></param>
        public void DeleteChatHistoryRecord(int authUserId, int targetUserId)
        {
            var chatHistory = _DbContext.ChatHistory.Where(c => (c.ContactId == targetUserId && c.UserId == authUserId)).FirstOrDefault();
            _DbContext.ChatHistory.Remove(chatHistory);

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

            FriendRelationship fr =  _DbContext.FriendRelationships.FirstOrDefault(f => 
            (f.FriendId== targetUserId && f.UserOfRelationship.Id == authUserid) ||
            (f.FriendId == authUserid && f.UserOfRelationship.Id == targetUserId)
            );

            if(fr== null)
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
        public void AddContactFriendList(Account addingUser, Account addedUser)
        {

            if (addedUser != null)
            {
                
                if(!IsFriend(addingUser.Id, addedUser.Id))
                {
                    var fr = new FriendRelationship
                    {
                        FriendId = addedUser.Id,
                       
                        UserOfRelationship = addingUser
                    };
                    _DbContext.FriendRelationships.Add(fr);
    
                }

                else
                {
                    throw new InvalidOperationException("User is already in the friendlist");
                }
            }

            else
            {
                throw new ArgumentNullException("Added User does not exist to be add");
            }
        }


        /// <summary>
        /// Method return all user's friends
        /// </summary>
        /// <param name="authUserId"></param>
        /// <returns></returns>
        public IEnumerable<FriendRelationship> GetAllFriendRelationship(int authUserId)
        {
            
            var user = _DbContext.Users.Where(u => u.Id == authUserId).FirstOrDefault();

            if (user != null)
            {
                return  user.FriendRelationship.AsEnumerable();
            }

            else
            {
                throw new ArgumentNullException("User does not exist to retrieve a friendlist");
            }
            
        }



        /// <summary>
        /// Method to remove a user from a friend list
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="friendUserId"></param>
        public void RemoveUserFromFriendList(int authUserId, int friendUserId)
        {
         
            var friend = _DbContext.FriendRelationships.Where(f => (f.UserId == authUserId && f.FriendId == friendUserId)).FirstOrDefault();
            if (friend != null)
            {
                _DbContext.FriendRelationships.Remove(friend);
            }

            else
            {
                throw new ArgumentNullException("Friend does not exist to be removed ");
            }

        }

    
    }
}