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
      
        private MessengerManager messengerManager;
        private int authUserId ;
        private bool securityPass = false;
        public MessengerController()
        {
            messengerManager = new MessengerManager();
        }

        [HttpGet]
        [ActionName("GetAllChatHistory")]
        //[ActionName("GetContactHistory")]
        public IHttpActionResult GetAllChatHistory()

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
                var contactList = messengerManager.GetAllContactHistory(authUserId);
                var contactListDTO = new List<ContactHistoryDTO>();
                foreach (ChatHistory mch in contactList)
                {

                    contactListDTO.Add(new ContactHistoryDTO
                    {
                        ContactId = mch.ContactId,
                        ContactUsername = mch.ContactUsername,
                        ContactTime = mch.ContactTime
                    });

                }
                //string updatedToken = sm.RefreshSession(securityContext.Token);
                return Ok(new { chatHistory = contactListDTO, /*SITtoken = updatedToken*/ });

            }


            
        }

        [HttpGet]
        [ActionName("GetMessageWithUser")]
        public IHttpActionResult GetMessageWithUser(int receiverUserId)
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
            }
            var conversations =  messengerManager.GetConversationBetweenUser(authUserId, receiverUserId);

            var firstUsername = um.FindUserById(authUserId).UserName;
            var secondUsername = um.FindUserById(receiverUserId).UserName;

            var chatHistory = messengerManager.GetChatHistoryBetweenUsers(authUserId, receiverUserId);

            List<ConversationDTO> conversationDTOs = new List<ConversationDTO>();
            foreach (Conversation c in conversations)
            {
                if (c.SenderId == authUserId && (c.CreatedDate.CompareTo(chatHistory.ContactTime) ==1 || c.CreatedDate.CompareTo(chatHistory.ContactTime)==0))
                {
                    ConversationDTO conversationDTO = new ConversationDTO
                    {
                        SenderUsername = firstUsername,
                        ReceiverUsername = secondUsername,
                        MessageContent = c.MessageContent,
                        CreatedDate = c.CreatedDate
                    };
                    conversationDTOs.Add(conversationDTO);

                }

               else if(c.ReceiverId == authUserId && (c.CreatedDate.CompareTo(chatHistory.ContactTime) == 1 || c.CreatedDate.CompareTo(chatHistory.ContactTime) == 0))
                {
                    ConversationDTO conversationDTO = new ConversationDTO
                    {
                        SenderUsername = secondUsername,
                        ReceiverUsername = firstUsername,
                        MessageContent = c.MessageContent,
                        CreatedDate = c.CreatedDate
                    };
                    conversationDTOs.Add(conversationDTO);
                }

                     
            }
            //string updatedToken = sm.RefreshSession(securityContext.Token);

            return Ok(new { conversation = conversationDTOs, /*SITtoken = updatedToken*/ });

        }

        [HttpGet]
        [ActionName("GetRecentMessageWithUser")]
        public IHttpActionResult GetRecentMessageWithUser(int receiverUserId2)
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
                var firstUsername = um.FindUserById(authUserId).UserName;
                var secondUsername = um.FindUserById(receiverUserId2).UserName;
                var conservations = messengerManager.GetRecentMessageBetweenUser(authUserId, receiverUserId2);
                

                if (conservations.SenderId == authUserId)
                {
                    ConversationDTO conversationDTO = new ConversationDTO
                    {
                        SenderUsername = firstUsername,
                        ReceiverUsername = secondUsername,
                        MessageContent = conservations.MessageContent,
                        CreatedDate = conservations.CreatedDate
                    };
                    return Ok( new{ conversation = conversationDTO, /*SITtoken = updatedToken*/ });

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
                    //string updatedToken = sm.RefreshSession(securityContext.Token);
                    return Ok(new { conversation = conversationDTO, /*SITtoken = updatedToken */});


                }



            }

            return NotFound();



        }

        [HttpDelete]
        [ActionName("DeleteMessage")]
        public IHttpActionResult DeleteChatMessageBetweenUser(int targetUserId)
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
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    return Unauthorized();
                }

                try
                {
                    //string updatedToken = sm.RefreshSession(securityContext.Token);

                    messengerManager.DeleteChatMessageBetweenUsers(authUserId, targetUserId);
                    return Ok(/*new { SITtoken = updatedToken}*/);
                }

                catch (Exception exception)
                {
                    return Content(HttpStatusCode.NotFound,exception.Message);
                }
            }

          


            
            
        }


       
        [HttpGet]
        [ActionName("GetUserIdWithUsername")]
        public IHttpActionResult GetUserIdWithUsername(string username)
        {
            UserManager umManager = new UserManager();
            var user = umManager.FindByUserName(username);
            if(user != null)
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
        [ActionName("SendMessage")]
        //[ActionName("SendMessage")]
        public IHttpActionResult SendMessage([FromBody] Conversation conversation)

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
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
              
            }
            conversation.CreatedDate = DateTime.Now;
            conversation.SenderId = authUserId;
          
            try
            {
                messengerManager.SendMessageToUser(conversation);
                
                var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();

                var connectionIDList = messengerManager.GetConnectionIdWithUserId(conversation.ReceiverId);
                if(connectionIDList != null)
                {
                    foreach (ChatConnectionMapping cM in connectionIDList)
                    {
                        var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages(conversation.SenderId, conversation.ReceiverId);

                    }
                   // string updatedToken = sm.RefreshSession(securityContext.Token);
                    return Ok(/*new { SITtoken = updatedToken }*/);
                }

                else
                {
                    return Content(HttpStatusCode.NotFound, "SignalR were unable to broadcast message to receiver");
                }
               

            }
            catch(ArgumentException Exception)
            {
                return Content(HttpStatusCode.NotFound, new { /*SITtoken = updatedToken*/ });
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
                User user = um.FindByUserName(securityContext.UserName);
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                if (user == null)
                {
                    return Unauthorized(); ;
                }
            }

            //string updatedToken = sm.RefreshSession(securityContext.Token);
            try
            {
                
                messengerManager.AddUserFriendList(authUserId, addedUsername);
                
                return Ok(/*new { SITtoken = updatedToken }*/);
            }

            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    return Content(HttpStatusCode.NotAcceptable, new { error = "A User already exists in friendlist", /*SITtoken = updatedToken*/ });

                }

                else if (ex is ArgumentNullException)
                {
                    return Content(HttpStatusCode.NotFound, new { error = "Added User does not exist to be add ", /*SITtoken = updatedToken*/ });

                }
            }
            return Content(HttpStatusCode.NotAcceptable, new { error = "Unknown error", /*SITtoken = updatedToken*/ });
        }

        [HttpGet]
        [ActionName("GetFriendList")]
        public  IHttpActionResult GetFriendList()
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
                try
                {
                    var friendList = messengerManager.GetAllFriendRelationships(authUserId);
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

                catch (Exception exception)
                {
                    return Content(HttpStatusCode.NotFound, new { error = exception.Message, /*SITtoken = updatedToken*/ });
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
                catch (ArgumentException exception)
                {
                    return Content(HttpStatusCode.NotFound, new { error = exception.Message, /*SITtoken = updatedToken*/ });
                }

            }
           

        }


    }


    
}
