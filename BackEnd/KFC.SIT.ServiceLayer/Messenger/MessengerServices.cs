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
        public List<User>GetAllChatContacts(String currentUsername)
        {

            using (var db = new DatabaseContext())
            {
                return db.Users.Where(u => u.UserName != currentUsername)
                                     .ToList();
            }
        }

        public List<Conversation>GetAllConservationBetweenContact(string senderUserName, string receiverUserName)
        {
            using (var db = new DatabaseContext())
            {
               
                return db.Conservations.
                              Where(c => (c.ReceiverUserName == receiverUserName
                                  && c.SenderUserName == senderUserName) ||
                                  (c.ReceiverUserName == senderUserName
                                  && c.SenderUserName == receiverUserName))
                              .OrderBy(c => c.CreatedDate)
                              .ToList();
            }
        }

        public void SendMessage(Conversation conversation)
        {

            using (var db = new DatabaseContext())
            {
                db.Conservations.Add(conversation);
                db.SaveChanges();
            }
        }

        public void AddContactHistory(string senderUsername, string receiverUsername)
        {
            var newMessengerContactHist = new MessengerContactHist
            {
                SenderUserName = senderUsername,
                ReceiverUserName = receiverUsername,
                ContactTime = DateTime.Now
            };

            var existingChatRecord = _DbContext.MessengerContactHists.FirstOrDefault(e => e.ReceiverUserName.Equals(receiverUsername) && e.SenderUserName.Equals(senderUsername));
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

        public Task<IQueryable<MessengerContactHist>> GetAllContactHistory(string senderUsername)
        {
            return  Task.FromResult(_DbContext.MessengerContactHists.Where(u => u.SenderUserName.Equals(senderUsername)).AsQueryable());
        }

        public bool IsFriend(User addingUser, User addedUser)
        {

            FriendRelationship fr =  _DbContext.FriendRelationships.FirstOrDefault(f => 
            (f.friendId== addingUser.Id && f.UserId == addedUser.Id) ||
            (f.friendId == addedUser.Id && f.UserId == addingUser.Id)
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
                
                if(!IsFriend(addingUser, addedUser))
                {
                    var fr = new FriendRelationship
                    {
                        friendId = addedUser.Id,
                        UserOfRelationship = addingUser
                    };
                    addingUser.FriendRelationship.Add(fr);
    
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
    }
}