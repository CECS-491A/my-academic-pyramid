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
        const int conversationIdForTest = 37;
        [Fact]
        public void CreateConversation_ShouldReturnAConversation()
        {
            //Arrange
            string targetUsername = "nguyentrong56@gmail.com";

            Conversation returnConversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                try
                {
                    returnConversation = msService.CreateConversation(testAuthUserId, testContactUserId, targetUsername);
                    db.SaveChanges();
                }

                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //Assert
            Assert.NotNull(returnConversation);
        }

        [Fact]
        public void DeleteConversation_ShouldReturnAConversation()
        {
            //Arrange
            string targetUsername = "nguyentrong56@gmail.com";

            Conversation conversation = null;
            Conversation foundConversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                conversation = msService.CreateConversation(testAuthUserId, testContactUserId, targetUsername);
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
                conversation = msService.GetConversationFromId(conversationIdForTest);

            }

            //Assert
            Assert.NotNull(conversation);

        }

        [Fact]
        public void GetContactUserIdFromConversation_ShouldReturnUserId()
        {
            //Arrange 
            int expected = 0 ;
            int actual;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                actual = msService.GetContactUserIdFromConversation(conversationIdForTest);

            }

            //Assert
            Assert.NotEqual(expected, actual);

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
                fr = msService.AddContactFriendList(testAuthUserId, testContactUserId);

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
            int conversationId = conversationIdForTest;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                messages = msService.GetAllMessagesFromConversation(conversationId);

            }

            //Assert
            Assert.NotEmpty(messages);

        }

        [Fact]
        public void GetRecentMessageFromConversation_ShouldReturnAMessage()
        {
            //Arrange 
            Message message = null;
            int conversationId = conversationIdForTest;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                try
                {
                    message = msService.GetMostRecentMessageConversation(conversationId);
                }

                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            //Assert
            Assert.NotNull(message);

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
            Message message = new Message
            {
                ConversationId = conversationIdForTest,
                OutgoingMessage = true,
                MessageContent = "testContent2",
                CreatedDate = DateTime.Now
            };
            Message returnMessage = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                try
                {
                    message = msService.SaveMessageToDatabase(message);
                    db.SaveChanges();
                }

                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    returnMessage = msService.DeleteMessage(message.Id);
                    db.SaveChanges();
                }

                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            //Assert
            Assert.NotNull(returnMessage);
        }



       



    }
}
