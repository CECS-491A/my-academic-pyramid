using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ServiceLayer.Messenger
{
    public class MessengerServices
    {
        protected DatabaseContext _DbContext;
        public MessengerServices(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }
        public List<User> GetAllChatContacts(String currentUsername)
        {

            using (var db = new DatabaseContext())
            {
                return db.Users.Where(u => u.UserName != currentUsername)
                                     .ToList();
            }
        }

        public List<Conversation> GetAllConservationBetweenContact(int senderId, int receiverId)
        {
            using (var db = new DatabaseContext())
            {
                return db.Conservations.
                              Where(c => (c.SenderId == senderId && c.ReceiverId == receiverId)
                                         || c.SenderId == receiverId && c.ReceiverId == senderId)
                              .OrderBy(c => c.CreatedDate)
                              .ToList();
            }
        }

        public Conversation GetLatestMessageBetweenContact(int senderId, int receiverId)
        {
            using (var db = new DatabaseContext())
            {

                return db.Conservations.
                              Where(c => ((c.ReceiverId == receiverId
                                  && c.SenderId == senderId) ||
                                  (c.ReceiverId == senderId
                                  && c.SenderId == receiverId))
                                  && c.CreatedDate == db.Conservations.Max(m => m.CreatedDate)).FirstOrDefault();
            }
        }

        public void SaveMessageToDatabase(Conversation conversation)
        {

            using (var db = new DatabaseContext())
            {
                db.Conservations.Add(conversation);
                db.SaveChanges();
            }
        }

        //public void DeleteMessage(string senderUsername, string receiverUsername)
        //{

        //        _DbContext.Conservations.RemoveRange(_DbContext.Conservations.Where(c => ((c.ReceiverId.Equals(receiverUsername)
        //                          && c.SenderUserName == senderUsername))));

        //}

        public void DeleteChatContactHistory (int senderId,int receiverId)
        {
             var chatHistory = _DbContext.MessengerContactHists.Where(h => h.SenderId == senderId && h.ReceiverId == receiverId).Single();

            if(chatHistory != null)
            {
                _DbContext.MessengerContactHists.Remove(chatHistory);
            }

            else
            {
                throw new ArgumentNullException("Chat history does not exist to be removed ");
            }
            

            





        }

        public IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserId(int userId)
        {
            return _DbContext.ChatConnectionMappings.Where(c => c.UserId == userId).AsEnumerable();
        }

        public void AddContactHistory(User sender, User receiver)
        {
            var newMessengerContactHist = new MessengerContactHist
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                SenderUsername = sender.UserName,
                ReceiverUsername = receiver.UserName,
                ContactTime = DateTime.Now
            };

            var existingChatRecord = _DbContext.MessengerContactHists.FirstOrDefault(e => e.ReceiverId.Equals(receiver.Id) && e.SenderId.Equals(sender.Id));
            if (existingChatRecord == null)
            {
                _DbContext.MessengerContactHists.Add(newMessengerContactHist);

            }

            else
            {
                _DbContext.MessengerContactHists.Attach(existingChatRecord);
                existingChatRecord.ContactTime = DateTime.Now;
               

            }
        }

        public IQueryable<MessengerContactHist> GetAllContactHistory(int senderId)
        {
            return  _DbContext.MessengerContactHists.Where(u => u.SenderId == senderId).AsQueryable();
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