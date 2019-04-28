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
using System.Data.Entity.Infrastructure;
using static ServiceLayer.ServiceExceptions.SignalRException;

namespace KFC.SIT.WebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class MessengerController : ApiController
    {

        private MessengerManager messengerManager;
        private int authUserId;
        private bool securityPass = false;
        public MessengerController()
        {
            messengerManager = new MessengerManager();
        }

        [HttpGet]
        [ActionName("GetAllConversation")]
        //[ActionName("GetContactHistory")]
        public IHttpActionResult GetAllConversation()

        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                var conversationList = messengerManager.GetAllConversations(authUserId);

                if (conversationList != null)
                {
                    var conversationDTOList = new List<ConversationDTO>();
                    foreach (Conversation c in conversationList)
                    {
                        var contactUsername = um.FindUserById(c.ContactUserId).UserName;
                        conversationDTOList.Add(new ConversationDTO
                        {
                            Id = c.Id,
                            ContactUsername = contactUsername,
                            CreatedDate = c.CreatedDate
                        });

                    }
                    return Ok(new { conversations = conversationDTOList, /*SITtoken = updatedToken*/ });
                }
                return Content(HttpStatusCode.NotFound, "User does not have any conversation");
                //string updatedToken = sm.RefreshSession(securityContext.Token);


            }

        }

        [HttpDelete]
        [ActionName("DeleteConversation")]
        public IHttpActionResult DeleteConversation(int conversationId)
        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
              Request.Headers
          );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                um = new UserManager();
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                try
                {
                    //string updatedToken = sm.RefreshSession(securityContext.Token);

                    var conversation = messengerManager.DeleteConversation(conversationId);
                    return Ok(new { conversation = conversation }/*new { SITtoken = updatedToken}*/);
                }

                catch (Exception exception)
                {
                    return Content(HttpStatusCode.NotFound, exception.Message);
                }
            }

        }

        [HttpGet]
        [ActionName("GetMessageInConversation")]
        public IHttpActionResult GetMessageInConversation(int conversationId)
        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {

                authUserId = um.FindByUserName(securityContext.UserName).Id;

                var authUsername = securityContext.UserName;
                var conversation = messengerManager.GetConversationFromId(conversationId);
                var contactUsername = messengerManager.GetContactUsernameFromConversation(conversationId);

                var frontEndDislayUsername = "";
                var messageList = messengerManager.GetMessageInConversation(conversationId);
                if (messageList != null)
                {
                    List<StoredMessageDTO> messageDTOList = new List<StoredMessageDTO>();
                    foreach (Message m in messageList)
                    {

                        if (m.OutgoingMessage == true)
                        {
                            frontEndDislayUsername = securityContext.UserName;
                        }
                        else
                        {
                            frontEndDislayUsername = contactUsername;
                        }
                        messageDTOList.Add(new StoredMessageDTO
                        {
                            SenderUsername = frontEndDislayUsername,
                            MessageContent = m.MessageContent,

                        });
                    }
                    return Ok(new { messages = messageDTOList, /*SITtoken = updatedToken*/ });
                }

                return Content(HttpStatusCode.NotFound, "No message in conversation");


            }

        }

        [HttpGet]
        [ActionName("GetRecentMessage")]
        public IHttpActionResult GetRecentMessageWithUser(int conversationId2)
        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                var authUsername = securityContext.UserName;
                var conversation = messengerManager.GetConversationFromId(conversationId2);
                var contactUsername = conversation.ContactUsername;
                var recentMessage = messengerManager.GetRecentMessageBetweenUser(conversationId2);
                var senderUsername = "";

                if (recentMessage != null)
                {
                    if (recentMessage.OutgoingMessage == true)
                    {
                        senderUsername = securityContext.UserName;
                    }
                    else
                    {
                        senderUsername = contactUsername;
                    }
                    var StoredMessageDTO = new StoredMessageDTO
                    {
                        SenderUsername = senderUsername,
                        MessageContent = recentMessage.MessageContent,

                    };
                    return Ok(new { message = StoredMessageDTO });
                }

                return Content(HttpStatusCode.NotFound, "No message in conversation");
            }







        }

        



        [HttpGet]
        [ActionName("GetUserIdWithUsername")]
        public IHttpActionResult GetUserIdWithUsername(string username)
        {
            UserManager umManager = new UserManager();
            var user = umManager.FindByUserName(username);
            if (user != null)
            {

                return Ok(user.Id);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [ActionName("GetAuthUserId")]
        public IHttpActionResult GetAuthUserId()
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanSendMessage"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized(); ;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                //string updatedToken = sm.RefreshSession(securityContext.Token);

                return Ok(new { authUserId = authUserId, /*SITtoken = updatedToken*/ });
            }



        }


        [HttpPost]
        [ActionName("SendMessageExistingConversation")]
        //[ActionName("SendMessageExistingConversation")]
        public IHttpActionResult SendMessageWithExistingConversation([FromBody] MessageDTO messageDTO)

        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
              Request.Headers
          );
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 "CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized(); ;
            }
            else
            {
                UserManager um = new UserManager();
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                var contactId = messengerManager.GetContactUserIdFromConversation(messageDTO.ConversationId);
                var message = new Message
                {
                    ConversationId = messageDTO.ConversationId,
                    MessageContent = messageDTO.MessageContent,
                    OutgoingMessage = true,
                    CreatedDate = DateTime.Now
                };

                try
                {
                    messengerManager.SaveMessageToDatabase(message, authUserId, contactId);
                }
                catch (DbUpdateException ex)
                {
                    return Content(HttpStatusCode.InternalServerError, "There is an error when saving message to database");
                }

                var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
                var authUsername = securityContext.UserName;
                var connectionIDList = messengerManager.GetConnectionIdWithUserId(contactId);
                if (connectionIDList != null)
                {
                    foreach (ChatConnectionMapping cM in connectionIDList)
                    {
                        var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages();

                    }
                    // string updatedToken = sm.RefreshSession(securityContext.Token);
                }
                
                var StoredMessageDTO = new StoredMessageDTO
                {
                    SenderUsername = authUsername,
                    MessageContent = messageDTO.MessageContent,

                };

                return Ok(new { message = StoredMessageDTO }/*new { SITtoken = updatedToken }*/);
            }

        }




        [HttpPost]
        [ActionName("SendMessageWithNewConversation")]
        //[ActionName("SendMessageExistingConversation")]
        public IHttpActionResult SendMessageWithNewConversation([FromBody] NewConversationMessageDTO newConversationMessageDTO)

        {
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(Request.Headers);
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 "CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized(); ;
            }
            else
            {
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                var contactUser = um.FindByUserName(newConversationMessageDTO.ContactUsername);
                Message returnMessage;
              

                if (contactUser != null)
                {
                    var message = new Message
                    {
                        MessageContent = newConversationMessageDTO.MessageContent,
                        OutgoingMessage = true,
                        CreatedDate = DateTime.Now
                    };

                    try
                    {
                        returnMessage =  messengerManager.SaveMessageToDatabase(message, authUserId, contactUser.Id);

                    }

                    catch (DbUpdateException ex)
                    {
                        return Content(HttpStatusCode.InternalServerError, "There is an error when saving message to database");
                    }

                    var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
                    var connectionIDList = messengerManager.GetConnectionIdWithUserId(contactUser.Id);

                    if (connectionIDList != null)
                    {
                        foreach (ChatConnectionMapping cM in connectionIDList)
                        {
                            var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages();

                        }
                        // string updatedToken = sm.RefreshSession(securityContext.Token);

                    }
                    return Ok(new { message = message }/*new { SITtoken = updatedToken }*/);
                }
                return Content(HttpStatusCode.NotFound, "Receiver does not exist to receive message");
            }
        }


        [HttpPost]
        [ActionName("AddFriend")]
        public IHttpActionResult AddFriendContactList(string addedUsername)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 "CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized(); ;
            }
            else
            {
                UserManager um = new UserManager();
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                FriendRelationship friendRelationship = null;
                try
                {
                    friendRelationship = messengerManager.AddUserFriendList(authUserId, addedUsername);

                }

                catch (DbUpdateException ex)
                {
                    return Content(HttpStatusCode.InternalServerError, "There is an error when adding a friend to database");
                }

                if (friendRelationship == null)
                {
                    return Content(HttpStatusCode.NotAcceptable, new { error = "A User already exists in friendlist", /*SITtoken = updatedToken*/ });
                }

                return Ok(new { friend = friendRelationship }/*new { SITtoken = updatedToken }*/);
            }

            //string updatedToken = sm.RefreshSession(securityContext.Token);



        }

        [HttpGet]
        [ActionName("GetFriendList")]
        public IHttpActionResult GetFriendList()
        {
            //This is just for test. Remove when token implemenation is finished 
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }
            SessionManager sm = new SessionManager();


            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                 "CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized(); ;
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                var friendList = messengerManager.GetAllFriendRelationships(authUserId);
                if (friendList != null)
                {
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
                    //string updatedToken = sm.RefreshSession(securityContext.Token);
                    return Ok(new { friendList = friendListDTO, /*SITtoken = updatedToken */});
                }

                else
                {
                    return Content(HttpStatusCode.NotFound, "User does not have any friend");
                }

                

            }





        }

        [HttpDelete]
        [ActionName("RemoveFriendFromList")]
        public IHttpActionResult DeleteFriend(int friendId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                   Request.Headers
               );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();


            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
             {
                "CanSendMessage"
             };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                try
                {
                    //string updatedToken = sm.RefreshSession(securityContext.Token);
                    messengerManager.RemoveUserFromFriendList(authUserId, friendId);
                    return Ok(/*new { SITtoken = updatedToken }*/);
                }
                catch (DbUpdateException ex)
                {
                    return Content(HttpStatusCode.InternalServerError, "There is a error removing user from friend list"  /*SITtoken = updatedToken*/ );
                }

            }


        }


    }



}
