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


        //public List<User> GetAllChatContacts(String currentUsername)
        //{

        //    using (var db = new DatabaseContext())
        //    {
        //        return db.Users.Where(u => u.UserName != currentUsername)
        //                             .ToList();
        //    }
        //}

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
        public void DeleteMessageFromDatabase(int firstUserId, int secondUserId)
        {
            _DbContext.Conversations.RemoveRange(_DbContext.Conversations
                .Where(c => (c.SenderId == firstUserId && c.ReceiverId == secondUserId)
                        || (c.SenderId == secondUserId && c.ReceiverId == firstUserId)));
        }

        /// <summary>
        /// Method to delete chat history record between 2 users
        /// The actual messages will not be delete. 
        /// This method should be combine with DeleteMessageFromDatabase method to completely delete messages
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetUserId"></param>
        public void DeleteChatHistoryRecord (int authUserId,int targetUserId)
        {
             var chatHistory = _DbContext.ChatHistory.Where(h => (h.SenderId == authUserId && h.ReceiverId == targetUserId)
                                                                           ||(h.SenderId == targetUserId && h.ReceiverId == authUserId)).FirstOrDefault();


            if(chatHistory.SenderId == authUserId  )
            {
                if(chatHistory.DeleteByReceiver == true)
                {
                    _DbContext.ChatHistory.Remove(chatHistory);
                }

                else
                {
                    _DbContext.Entry(chatHistory).State = System.Data.Entity.EntityState.Modified;
                    chatHistory.DeleteBySender = true;
                    chatHistory.TimeWhenMarkedDeleted = DateTime.Now;
                }
               
            }

            else if (chatHistory.ReceiverId == authUserId)
            {
                if(chatHistory.DeleteBySender == true)
                {
                    _DbContext.ChatHistory.Remove(chatHistory);
                }

                else
                {
                    _DbContext.Entry(chatHistory).State = System.Data.Entity.EntityState.Modified;
                    chatHistory.DeleteByReceiver = true;
                    chatHistory.TimeWhenMarkedDeleted = DateTime.Now;
                }
                
            }
    
        }

        public IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserId(int userId)
        {
            return _DbContext.ChatConnectionMappings.Where(c => c.UserId == userId).AsEnumerable();
        }

        public void AddContactHistory(User sender, User receiver)
        {
            var newMessengerContactHist = new ChatHistory
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                SenderUsername = sender.UserName,
                ReceiverUsername = receiver.UserName,
                ContactTime = DateTime.Now
            };

            var existingChatRecord = _DbContext.ChatHistory.FirstOrDefault
                (e => (e.ReceiverId == receiver.Id && e.SenderId == sender.Id) 
                ||  (e.ReceiverId == sender.Id && e.SenderId== receiver.Id)) ;

            if (existingChatRecord == null)
            {
                _DbContext.ChatHistory.Add(newMessengerContactHist);

            }

            else
            {
                _DbContext.ChatHistory.Attach(existingChatRecord);
                existingChatRecord.ContactTime = DateTime.Now;
               

            }
        }

        public IQueryable<ChatHistory> GetAllContactHistory(int senderId)
        {
            return  _DbContext.ChatHistory.Where(u => u.SenderId == senderId || u.ReceiverId == senderId).AsQueryable();
        }

        public ChatHistory GetContactHistoryBetweenUsers(int firstUserId, int secondUserId)
        {
            return _DbContext.ChatHistory.Where(u => (u.SenderId == firstUserId && u.ReceiverId == secondUserId)
                                                                || (u.SenderId == secondUserId && u.ReceiverId == firstUserId)).FirstOrDefault();
        }

        public bool IsFriend(int addingUserId, int addedUserId)
        {

            FriendRelationship fr =  _DbContext.FriendRelationships.FirstOrDefault(f => 
            (f.FriendId== addedUserId && f.UserOfRelationship.Id == addingUserId) ||
            (f.FriendId == addingUserId && f.UserOfRelationship.Id == addedUserId)
            );

            if(fr== null)
            {
                return false;
            }
            return true;


        }

        public void AddContactFriendList(User addingUser, User addedUser)
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

        public IEnumerable<FriendRelationship> GetAllFriendRelationship(int userId)
        {
            
            var user = _DbContext.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user != null)
            {
                return  user.FriendRelationship.AsEnumerable();
            }

            else
            {
                throw new ArgumentNullException("User does not exist to retrieve a friendlist");
            }
            
        }


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