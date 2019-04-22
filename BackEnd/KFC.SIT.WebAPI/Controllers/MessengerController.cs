using DataAccessLayer.Models.Messenger;
using WebAPI.Gateways.Messenger;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Signalr;
using System.Net.Http;
using DataAccessLayer.DTOs;
using System.Collections.Generic;
using WebAPI.UserManagement;
using SecurityLayer.Authorization;
using KFC.SIT.WebAPI.Utility;
using SecurityLayer.Sessions;
using SecurityLayer.Authorization.AuthorizationManagers;
using DataAccessLayer;
using System.Web.Http.Controllers;
using DataAccessLayer.Models;

namespace KFC.SIT.WebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
 
    public class MessengerController : ApiController
    {
        HttpContext httpContext;
        MessengerManager messengerManager;
        private int authUserId ;
        private bool securityPass = false;
        public MessengerController()
        {


            
            httpContext = HttpContext.Current;
            messengerManager = new MessengerManager();
            
          
           
        }


        [HttpGet]
        [ActionName("LoadMessageContact")]
        public IQueryable<ConversationDTO> MessengerWithContact(int receiverUserId)
        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }
            var conversations =  messengerManager.GetConversationBetweenUser(authUserId, receiverUserId);

            var firstUsername = um.FindUserById(authUserId).UserName;
            var secondUsername = um.FindUserById(receiverUserId).UserName;

            var chatHistory = messengerManager.GetChatHistoryBetweenUsers(authUserId, receiverUserId);

            List<ConversationDTO> conversationDTOs = new List<ConversationDTO>();
            foreach (Conversation c in conversations)
            {
                if (c.SenderId == authUserId)
                {
                    ConversationDTO conversationDTO = new ConversationDTO
                    {
                        SenderUsername = firstUsername,
                        ReceiverUsername = secondUsername,
                        MessageContent = c.MessageContent,
                        CreatedDate = c.CreatedDate
                    };
                    if(chatHistory.DeleteByReceiver == true || chatHistory.DeleteBySender == true)
                    {
                        if(chatHistory.TimeWhenMarkedDeleted < conversationDTO.CreatedDate)
                        {
                            conversationDTOs.Add(conversationDTO);
                        }
                    }
                    
                }

               else if(c.SenderId == receiverUserId)
                {
                    ConversationDTO conversationDTO = new ConversationDTO
                    {
                        SenderUsername = secondUsername,
                        ReceiverUsername = firstUsername,
                        MessageContent = c.MessageContent,
                        CreatedDate = c.CreatedDate
                    };
                    if (chatHistory.DeleteByReceiver == true || chatHistory.DeleteBySender == true)
                    {
                        if (chatHistory.TimeWhenMarkedDeleted < conversationDTO.CreatedDate)
                        {
                            conversationDTOs.Add(conversationDTO);
                        }
                    }
     
                }

                     
            }
               
            
            return conversationDTOs.AsQueryable();

        }

        [HttpGet]
        [ActionName("LoadLatestMessageContact")]
        public async Task<ConversationDTO> GetLatestMessageWithContact(int receiverUserId2)
        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }

            var firstUsername = um.FindUserById(authUserId).UserName;
            var secondUsername = um.FindUserById(receiverUserId2).UserName;
            var conservations = await messengerManager.GetLatestMessageBetweenUser(authUserId, receiverUserId2);

            if (conservations.SenderId == authUserId)
            {
                ConversationDTO conversationDTO = new ConversationDTO
                {
                    SenderUsername = firstUsername,
                    ReceiverUsername = secondUsername,
                    MessageContent = conservations.MessageContent,
                    CreatedDate = conservations.CreatedDate
                };
                return conversationDTO;

            }

            else if (conservations.SenderId == receiverUserId2)
            {
                ConversationDTO conversationDTO = new ConversationDTO
                {
                    SenderUsername = secondUsername,
                    ReceiverUsername = firstUsername,
                    MessageContent = conservations.MessageContent,
                    CreatedDate = conservations.CreatedDate
                };

                return conversationDTO;
            }

            return null;

           

        }

        [HttpDelete]
        [ActionName("DeleteMessage")]
        public HttpResponseMessage DeleteChatMessageBetweenUser(int targetUserId)
        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
              Request.Headers
          );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }

            try
            {
                messengerManager.DeleteChatMessageBetweenUsers(authUserId, targetUserId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }


            
            
        }


        [HttpGet]
        [ActionName("GetContactHistory")]
        //[ActionName("GetContactHistory")]
        public IQueryable<ContactHistoryDTO> GetAllContactHistory()
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 //"CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }
          
          
            var contactList =  messengerManager.GetAllContactHistory(authUserId);
            var contactListDTO = new List<ContactHistoryDTO>();
            foreach(ChatHistory mch in contactList)
            {
                if(mch.SenderId == authUserId && mch.DeleteBySender==false)
                {
                    contactListDTO.Add(new ContactHistoryDTO
                    {
                        ContactId = mch.ReceiverId,
                        ContactUsername = mch.ReceiverUsername,
                        ContactTime = mch.ContactTime
                    });
                }

                else if (mch.ReceiverId == authUserId && mch.DeleteByReceiver == false)
                {
                    contactListDTO.Add(new ContactHistoryDTO
                    {
                        ContactId = mch.SenderId,
                        ContactUsername = mch.SenderUsername,
                        ContactTime = mch.ContactTime
                    });
                }


            }
            return contactListDTO.AsQueryable();
        }
        [HttpGet]
        [ActionName("GetUserIdWithUsername")]
        public HttpResponseMessage GetUserIdWithUsername(string username)
        {
            UserManager umManager = new UserManager();
            var user = umManager.FindByUserName(username);
            return Request.CreateResponse(HttpStatusCode.OK, user.Id);
        }
        [HttpGet]
        [ActionName("GetAuthUserId")]
        public HttpResponseMessage GetAuthUserId()
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                //"CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }
          
           
            return Request.CreateResponse(HttpStatusCode.OK, authUserId);
        }







        [HttpPost]
        [ActionName("SendMessage")]
        //[ActionName("SendMessage")]
        public HttpResponseMessage SendMessage([FromBody] Conversation conversation)

        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
              Request.Headers
          );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 //"CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }
            conversation.CreatedDate = DateTime.Now;
            conversation.SenderId = authUserId;

            try
            {
                messengerManager.SendMessage(conversation);

            }
            catch(ArgumentException Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User does not exist to send message");
            }


            var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
                MessengerHub mHub = new MessengerHub();
                var connectionIDList = messengerManager.GetConnectionIdWithUserId(conversation.ReceiverId);
                {
                    foreach (ChatConnectionMapping cM in connectionIDList)
                    {
                        var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages(conversation.SenderId, conversation.ReceiverId);

                    }
                }
            return Request.CreateResponse(HttpStatusCode.OK, "Message is sent successfully");
            
        }

 

        [HttpPost]
        [ActionName("AddFriend")]
        public HttpResponseMessage AddFriendContactList(string addedUsername)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 //"CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }


            try
            {
                
                messengerManager.AddUserFriendList(authUserId, addedUsername);
                return Request.CreateResponse(HttpStatusCode.OK, "The User was added to friendlist");
            }

            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "A User already exists in friendlist");

                }

                else if (ex is ArgumentNullException)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Added User does not exist to be add ");

                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Unknow error");
        }

        [HttpGet]
        [ActionName("GetFriendList")]
        public  HttpResponseMessage GetFriendList()
        {
            //This is just for test. Remove when token implemenation is finished 
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                Request.Headers
            );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 //"CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }

            try
            {
                var friendList = messengerManager.GetFriendRelationships(authUserId);
                List<FriendContactDTO> friendListDTO = new List<FriendContactDTO>();
                var userManager = new UserManager();
                foreach (FriendRelationship friend in friendList)
                {
                    friendListDTO.Add(new FriendContactDTO
                    {
                        FriendId = friend.FriendId,
                        FriendUsername = userManager.FindUserById(friend.FriendId).UserName
                    });
                    
                }
                return Request.CreateResponse(HttpStatusCode.OK, friendListDTO);
            }

            catch (Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);
            }
         


        }

        [HttpDelete]
        [ActionName("RemoveFriendFromList")]
        public HttpResponseMessage DeleteFriend(int friendId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                   Request.Headers
               );
            if (securityContext == null)
            {
                securityPass = false;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                securityPass = false;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                // "CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                securityPass = false;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    securityPass = false;
                }
            }
            try
            {
                messengerManager.RemoveUserFromFriendList(authUserId, friendId);
                return Request.CreateResponse(HttpStatusCode.OK, "Friend is succesfully removed from the list");
            }
            catch(ArgumentException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);
            }

        }


    }


    
}
