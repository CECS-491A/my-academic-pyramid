using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using ServiceLayer.Messenger;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceLayer.Tests
{

    public class MessengerServicesTest
    {
  
        const int testAuthUserId = 1;
        const int testContactUserId = 2;
        const string testContactUsername = "nguyentrong56@yahoo.com";
         int conversationIdForTest = 1;
        [Fact]
        public void CreateConversation_ShouldReturnAConversation()
        {
            //Arrange
       

            Conversation returnConversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);

                    returnConversation = msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                    db.SaveChanges();

            }

            //Assert
            Assert.NotNull(returnConversation);
        }

        [Fact]
        public void DeleteConversation_ShouldReturnAConversation()
        {
            //Arrange
       
            Conversation conversation = null;
            Conversation foundConversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                conversation = msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();


                conversation = msService.DeleteConversation(conversation.Id);
                db.SaveChanges();

                foundConversation = db.Conversations.Where(c => c.Id == conversation.Id).FirstOrDefault();

            }

            //Assert
            Assert.Null(foundConversation);
        }

        [Fact]
        public void GetAllConversation_ShouldReturnACollectionOfConversation()
        {
            //Arrange 
            IEnumerable<Conversation> conversations = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();
                conversations = msService.GetAllConversation(testAuthUserId);
                
            }

            //Assert
            Assert.NotNull(conversations);

        }

        [Fact]
        public void GetaConversationBetweenUser_ShouldReturnAConversation()
        {
            //Arrange 
            Conversation conversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();
             
                conversation = msService.GetConversationBetweenUsers(testAuthUserId, testContactUserId);

            }

            //Assert
            Assert.NotNull(conversation);

        }

        [Fact]
        public void GetaConversationById_ShouldReturnAConversation()
        {
            //Arrange 
            Conversation conversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                var returnConversation = msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();
                conversation = msService.GetConversationFromId(returnConversation.Id);

            }

            //Assert
            Assert.NotNull(conversation);

        }


        [Fact]
        public void AddContactToFriendList_ShouldReturnFriendRelationship()
        {
            //Arrange 
            FriendRelationship fr = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                msService.RemoveUserFromFriendList(testAuthUserId, testContactUserId);
                db.SaveChanges();
                bool isFriend = msService.IsFriend(testAuthUserId, testContactUserId);
                if(!isFriend)
                {
                    fr = msService.AddContactFriendList(testAuthUserId, testContactUserId);
                }
                

                db.SaveChanges();

            }

            //Assert
            Assert.NotNull(fr);

        }

        [Fact]
        public void ChekifUsersAreFriend_ShouldReturnTrue()
        {
            //Arrange 
            bool fr = false;
           

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                msService.AddContactFriendList(testAuthUserId, testContactUserId);
                db.SaveChanges();
                fr = msService.IsFriend(testAuthUserId, testContactUserId);

            }

            //Assert
            Assert.True(fr);

        }

        [Fact]
        public void GetAllFriendRelationship_ShouldReturnListOfFriend()
        {
            //Arrange 
            List<FriendRelationship> friends = new List<FriendRelationship>();


            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
               
                msService.AddContactFriendList(testAuthUserId, testContactUserId);
                db.SaveChanges();
                friends = msService.GetAllFriendRelationship(testAuthUserId).ToList();

            }

            //Assert
            Assert.NotEmpty(friends);

        }

        [Fact]
        public void RemoveContactFromFriendList_FindDeletedFriendShouldReturnNull()
        {
            //Arrange 
            FriendRelationship DeletedFriend = null; 

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);  
                msService.AddContactFriendList(testAuthUserId, testContactUserId);
                db.SaveChanges();
                var friends = msService.GetAllFriendRelationship(testAuthUserId).ToList();
                FriendRelationship FriendRelationship = friends.ElementAt(0);
                msService.RemoveUserFromFriendList(FriendRelationship.UserId, FriendRelationship.FriendId);
                db.SaveChanges();

                DeletedFriend = db.FriendRelationships.Where(f => f.UserId == FriendRelationship.UserId && f.FriendId == FriendRelationship.FriendId).FirstOrDefault();
            }

            //Assert
            Assert.Null(DeletedFriend);

        }


        [Fact]
        public void GetAllMessageFromConversation_ShouldReturnListOfMessage()
        {
            //Arrange 
            List<Message> messages = null;
  
            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                var conversation = msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();
                Message message = new Message
                {
                    ConversationOfMessage = conversation,
                    OutgoingMessage = true,
                    MessageContent = "testContent2",
                    CreatedDate = DateTime.Now
                };

                message = msService.SaveMessageToDatabase(message);
                db.SaveChanges();

                var foundConversation = db.Conversations.Where(c => c.UserId == testAuthUserId && c.ContactUserId == testContactUserId).FirstOrDefault();
                messages = msService.GetAllMessagesFromConversation(conversation.Id);

            }

            //Assert
            Assert.NotEmpty(messages);

        }

        [Fact]
        public void GetRecentMessageFromConversation_ShouldReturnAMessage()
        {
            //Arrange 
            Message expected = null;
            
            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                var conversation = msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();
                var  message = new Message
                {
                    ConversationOfMessage = conversation,
                    OutgoingMessage = true,
                    MessageContent = "testContent2",
                    CreatedDate = DateTime.Now
                };

                message = msService.SaveMessageToDatabase(message);
                db.SaveChanges();
                var foundConversation = db.Conversations.Where(c => c.UserId == testAuthUserId && c.ContactUserId == testContactUserId).FirstOrDefault();

                expected = msService.GetMostRecentMessageConversation(foundConversation.Id);


            }

            //Assert
            Assert.NotNull(expected);

        }


        [Fact]
        public void SaveMessageToDatabase_ShouldReturnAMessage()
        {
            //Arrange
            Message message = new Message
            {
                ConversationId = conversationIdForTest,
                OutgoingMessage = true,
                MessageContent = "testContent",
                CreatedDate = DateTime.Now
            };
            Message returnMessage = null;

            //Act
            using (var db = new DatabaseContext() )
            {
                MessengerServices msService = new MessengerServices(db);
                try
                {
                    returnMessage = msService.SaveMessageToDatabase(message);
                    db.SaveChanges();
                }

                catch(DbUpdateException ex )
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            //Assert
            Assert.NotNull(returnMessage);
            
        }

        [Fact]
        public void DeleteMessage_ShouldReturnAMessage()
        {
            //Arrange
            Message returnMessage = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                var conversation = msService.CreateConversation(testAuthUserId, testContactUserId, testContactUsername);
                db.SaveChanges();
                Message message = new Message
                {
                    ConversationOfMessage = conversation,
                    OutgoingMessage = true,
                    MessageContent = "testContent2",
                    CreatedDate = DateTime.Now
                };

                message = msService.SaveMessageToDatabase(message);
                db.SaveChanges();
                var foundMessage = db.Messages.Where(m => m.MessageContent.Equals("testContent2")).FirstOrDefault();
                returnMessage = msService.DeleteMessage(foundMessage.Id);
                db.SaveChanges();


                //Assert
                Assert.NotNull(returnMessage);
            }
        }



       



    }
}
