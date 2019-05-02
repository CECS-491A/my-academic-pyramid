using System.Collections.Generic;
using DataAccessLayer.Models.Messenger;

namespace ServiceLayer.Messenger
{
    public interface IMessengerServices
    {
        FriendRelationship AddContactFriendList(int authUserId, int targetUserId);
        Conversation CreateConversation(int authUserId, int targetUserId, string targetUsername);
        Conversation DeleteConversation(int conversationId);
        Message DeleteMessage(int messageId);
        IEnumerable<Conversation> GetAllConversation(int authUserId);
        IEnumerable<FriendRelationship> GetAllFriendRelationship(int authUserId);
        List<Message> GetAllMessagesFromConversation(int conversationId);
        IEnumerable<ChatConnectionMapping> GetConnectionIdWithUserId(int userId);
        int GetContactUserIdFromConversation(int conversationId);
        string GetContactUsernameFromConversation(int conversationId);
        Conversation GetConversationBetweenUsers(int authUserId, int targetUserId);
        Conversation GetConversationFromId(int conversationId);
        Message GetMostRecentMessageConversation(int conversationId);
        bool IsFriend(int authUserid, int targetUserId);
        Conversation MarkConversationRead(int conversationId);
        FriendRelationship RemoveUserFromFriendList(int authUserId, int friendUserId);
        Message SaveMessageToDatabase(Message message);
    }
}