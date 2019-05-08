using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using ServiceLayer.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Gateways.Messenger;
using Xunit;
namespace ManagerLayer.Tests
{
    public class MessengerManagerTests
    {
        const int testAuthUserId = 3;
        const int testContactUserId = 4;
        const string testContactUsername = "tntsmart1990@gmail.com";
        const int conversationIdForTest = 38;


        [Fact]
        public void GetAllMessageInConversation_ShouldReturnConversation()
        {
            // Arrange
            Message newMessage = new Message
            {
                MessageContent = "testContent3"
            };

            List<Message> messages = null;


            //Act

            MessengerManager msManager = new MessengerManager();
            var createdMessage = msManager.SaveMessageToDatabase(newMessage, testAuthUserId, testContactUserId);
            messages = msManager.GetMessageInConversation(createdMessage.ConversationId).ToList();

            //Assert
            Assert.NotEmpty(messages);
        }

        [Fact]
        public void GetRecentMessageInConversation_ShouldReturnAMessage()
        {
            // Arrange
            Message newMessage = new Message
            {
                MessageContent = "testContent3"
            };

            Message message ;


            //Act

            MessengerManager msManager = new MessengerManager();
            var createdMessage = msManager.SaveMessageToDatabase(newMessage, testAuthUserId, testContactUserId);
            message = msManager.GetRecentMessageBetweenUser(createdMessage.ConversationId);

            //Assert
            Assert.NotNull(message);
        }

        [Fact]
        public void SaveMessageToDatabase_ShouldReturnMessageWithoutException()
        {
            //Arrange
            Message newMessage = new Message
            {
                MessageContent = "testContent3"
            };

            Message expected = null;

            //Act

            MessengerManager msManager = new MessengerManager();
            expected = msManager.SaveMessageToDatabase(newMessage, testAuthUserId, testContactUserId);

            //Assert
            Assert.NotNull(expected);
        }


        [Fact]
        public void DeleteConversation_FindDeletedConversationShouldReturnNull()
        {
            //Arrange
            Conversation conversation = null;
            Conversation foundConversation = null;

            //Act
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                conversation = msService.CreateConversation(testAuthUserId, testContactUserId, "nguyentrong56@yahoo.com");
                db.SaveChanges();

                MessengerManager messengerManager = new MessengerManager();
                conversation = messengerManager.DeleteConversation(conversation.Id);


                foundConversation = db.Conversations.Where(c => c.Id == conversation.Id).FirstOrDefault();

            }

            //Assert
            Assert.Null(foundConversation);
        }


        [Fact]
        public void GetAllConversations_ShouldReturnListOfConversation()
        {
            //Arrange
            List<Conversation> conversations = null;
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                msService.CreateConversation(testAuthUserId, testContactUserId, "nguyentrong56@yahoo.com");
                db.SaveChanges();
            }

            //Act

            MessengerManager messengerManager = new MessengerManager();
            conversations = messengerManager.GetAllConversations(testAuthUserId).ToList();
            //Assert
            Assert.NotEmpty(conversations);
        }

        [Fact]
        public void GetConversationFromId_ShouldReturnConversation()
        {
            //Arrange
             Conversation conversation = null;
            Conversation expected = null;
            using (var db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                conversation = msService.CreateConversation(testAuthUserId, testContactUserId, "nguyentrong56@yahoo.com");
                db.SaveChanges();
            }

            //Act
            MessengerManager messengerManager = new MessengerManager();
            expected = messengerManager.GetConversationFromId(conversation.Id);

            //Assert
            Assert.NotNull(expected);
        }

        [Fact]
        public void AddUserToFriendList_ShouldReturnAFriendUserNoExceptionThrown()
        {
            //Arrange
            FriendRelationship friendUser = null;

            //Act
            MessengerManager messengerManager = new MessengerManager();
            bool isFriend;
            using (DatabaseContext db = new DatabaseContext())
            {
                MessengerServices msService = new MessengerServices(db);
                isFriend = msService.IsFriend(testAuthUserId, testContactUserId);
                if (isFriend == true)
                {
                    messengerManager.RemoveUserFromFriendList(testAuthUserId, testContactUserId);
                }
                db.SaveChanges();
            }

            friendUser = messengerManager.AddUserFriendList(testAuthUserId, testContactUsername);

            //Assert
            Assert.NotNull(friendUser);
        }

        [Fact]
        public void GetAllFriendRelationships_ShouldReturnListOfFriend()
        {
            //Arrange
            List<FriendRelationship> friendList = null;

            //Act
            MessengerManager messengerManager = new MessengerManager();
            friendList = messengerManager.GetAllFriendRelationships(testAuthUserId).ToList();

            //Assert
            Assert.NotNull(friendList);
        }

        [Fact]
        public void RemoveUserFromFriendList_ShouldReturnFriendRelationship()
        {
            //Arrange
            FriendRelationship fs = null;

            //Act
            MessengerManager messengerManager = new MessengerManager();
            messengerManager.RemoveUserFromFriendList(testAuthUserId , testContactUserId);
            messengerManager.AddUserFriendList(testAuthUserId, testContactUsername);

            using (DatabaseContext db = new DatabaseContext())
            {
                var foundRelationship = db.FriendRelationships.Where(f => f.UserId == testAuthUserId && f.FriendId == testContactUserId).FirstOrDefault();
                fs = messengerManager.RemoveUserFromFriendList(foundRelationship.UserId, foundRelationship.FriendId);
            }
                
            

            //Assert
            Assert.NotNull(fs);
        }



    }
}
