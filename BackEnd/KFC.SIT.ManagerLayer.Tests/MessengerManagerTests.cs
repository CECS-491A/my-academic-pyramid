﻿using DataAccessLayer;
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
        const int testAuthUserId = 1;
        const int testContactUserId = 2;
        const string testContactUsername = "nguyentrong56@yahoo.com";
        const int conversationIdForTest = 38;


        [Fact]
        public void GetMessageInConversation_ShouldReturnConversation()
        {
            //Arrange
            List<Message> messages = null;

            //Act

            MessengerManager msManager = new MessengerManager();
            messages = msManager.GetMessageInConversation(conversationIdForTest).ToList();

            //Assert
            Assert.NotEmpty(messages);
        }

        [Fact]
        public void GetRecentMessageInConversation_ShouldReturnAMessage()
        {
            //Arrange
            Message message = null;

            //Act

            MessengerManager msManager = new MessengerManager();
            message = msManager.GetRecentMessageBetweenUser(conversationIdForTest);

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
        public void RemoveUserFromFriendList_FindDeletedFriendShouldReturnNull()
        {
            //Arrange
            FriendRelationship fs = null;

            //Act
            MessengerManager messengerManager = new MessengerManager();
            var newFriendRelationship = messengerManager.AddUserFriendList(testAuthUserId, testContactUsername);
            fs = messengerManager.RemoveUserFromFriendList(newFriendRelationship.UserId, newFriendRelationship.FriendId);

            //Assert
            Assert.NotNull(fs);
        }



    }
}
