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
using static ServiceLayer.ServiceExceptions.MessengerServiceException;

namespace KFC.SIT.WebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class MessengerController : ApiController
    {

        private MessengerManager messengerManager;
        private int authUserId;
        public MessengerController()
        {
            messengerManager = new MessengerManager();
        }

        /// <summary>
        /// Return all conversations of authenticated user Id
        /// Auth User Id will be get from the security context which from the token 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAllConversation")]
        public IHttpActionResult GetAllConversation()

        {
            //The authentication part start from here
            //Create security context from the token 
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Unauthorized();
            }

            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            //Create authorization manager from security context
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            
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

                //Get auth User Id from auth username
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                //Return all conversations of Auth User
                var conversationList = messengerManager.GetAllConversations(authUserId);

                //From here, create conversation transfer objects to send to front end
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
                            HasNewMessage = c.HasNewMessage,
                            CreatedDate = c.ModifiedDate.ToString("MMMM dd yyyy hh:mm:ss")
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
            //The authentication part start from here
            //Create security context from the token 
            
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
              Request.Headers
          );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
           
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
                //Find auth user Id from auth username
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                try
                {
                    //string updatedToken = sm.RefreshSession(securityContext.Token);

                    //Delete conversation using conversationId
                    var conversation = messengerManager.DeleteConversation(conversationId);
                    return Ok(new { conversation = conversation }/*new { SITtoken = updatedToken}*/);
                }

                catch (DbUpdateException ex)
                {
                    return Content(HttpStatusCode.NotFound, "conversation does not exist to be delete");
                }
            }

        }

        /// <summary>
        /// Controller to get all message in a conversation using conversation Id
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetMessageInConversation")]
        public IHttpActionResult GetMessageInConversation(int conversationId)
        {
            //The authentication part start from here
            //Create security context from the token 
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Unauthorized();
            }

            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
           
            List<string> requiredClaims = new List<string>()
            {
                "CanSendMessage"
            };

            //Create authorization manager from security context to check claims
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                var authUsername = securityContext.UserName;

                //Find auth user Id from auth username
                authUserId = um.FindByUserName(authUsername).Id;

                // Get conversation from conversation Id
                var conversation = messengerManager.GetConversationFromId(conversationId);

                // Get contact username in the conversation 
                var contactUsername = messengerManager.GetContactUsernameFromConversation(conversationId);

                //Temporary username used to decide which username to display in the message 
                var temPUsername = "";

                //Get all messages in the conversation
                var messageList = messengerManager.GetMessageInConversation(conversationId);
                if (messageList != null)
                {
                    // Create list of messageDTO for transfer
                    List<StoredMessageDTO> messageDTOList = new List<StoredMessageDTO>();
                    foreach (Message m in messageList)
                    {
                        // If true the message is come from the authenticated user
                        if (m.OutgoingMessage == true)
                        {
                            // Set temporary username to auth username 
                            temPUsername = securityContext.UserName;
                        }
                        else
                        {
                            //Otherwise, set the temp username to contactname
                            temPUsername = contactUsername;
                        }

                        //Add StoreMessageDTO to messageDTO List
                        //Stored Message DTO is sent from back end 
                        // is used to differenate between the MessageDTO sent from the front end
                        messageDTOList.Add(new StoredMessageDTO
                        {
                            Id = m.Id,
                            SenderUsername = temPUsername,
                            MessageContent = m.MessageContent,

                        });
                    }
                    return Ok(new { messages = messageDTOList, contactUsername = contactUsername /*SITtoken = updatedToken*/ });
                }

                return Content(HttpStatusCode.NotFound, "No message in conversation");


            }

        }

        [HttpGet]
        [ActionName("GetRecentMessage")]
        public IHttpActionResult GetRecentMessageWithUser(int conversationId2)
        {
            //The authentication part start from here
            //Create security context from the token 
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Unauthorized();
            }

            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            
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

                //Get conversation between auth user and contact user
                var conversation = messengerManager.GetConversationFromId(conversationId2);
                if(conversation != null)
                {
                    //Get contact user name
                    var contactUsername = conversation.ContactUsername;

                    //Return the most recent message from the conversation 
                    var recentMessage = messengerManager.GetRecentMessageBetweenUser(conversationId2);
                    var senderUsername = "";


                    if (recentMessage != null)
                    {
                        //If true , the message is sent from auth user
                        //Set the display username in front end to auth username 
                        if (recentMessage.OutgoingMessage == true)
                        {
                            senderUsername = securityContext.UserName;
                        }

                        //If false , set the display user name in front end to contactUsername
                        else
                        {
                            senderUsername = contactUsername;
                        }

                        //Create messageDTO for transger 
                        var StoredMessageDTO = new StoredMessageDTO
                        {
                            Id = recentMessage.Id,
                            ConversationId = conversationId2,
                            SenderUsername = senderUsername,
                            MessageContent = recentMessage.MessageContent,

                        };

                        //return the message
                        return Ok(new { message = StoredMessageDTO });
                    }
                }
              

                return Content(HttpStatusCode.NotFound, "No message in conversation");
            }

        }


        [HttpGet]
        [ActionName("GetUserIdWithUsername")]
        public IHttpActionResult GetUserIdWithUsername(string username)
        {
            UserManager umManager = new UserManager();
            //The authentication part start from here
            //Create security context from the token 
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

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );

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
          

        }

        [HttpGet]
        [ActionName("GetAuthUserIdAndUsername")]
        public IHttpActionResult GetAuthUserIdAndUsername()
        {
            //The authentication part start from here
            //Create security context from the token 
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

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
      
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
                authUserId = user.Id;
                var authUsername = user.UserName;
                //string updatedToken = sm.RefreshSession(securityContext.Token);

                return Ok(new { authUserId = authUserId, authUsername = authUsername/*SITtoken = updatedToken*/ });
            }



        }


        [HttpPost]
        [ActionName("SendMessageExistingConversation")]
        //[ActionName("SendMessageExistingConversation")]
        public IHttpActionResult SendMessageWithExistingConversation([FromBody] MessageDTO messageDTO)

        {
            //The authentication part start from here
            //Create security context from the token 
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
              Request.Headers
          );
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }

            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
  
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

                //Get auth user Id from security context
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                // Get contactUserId from the conversation
                var contactId = messengerManager.GetContactUserIdFromConversation(messageDTO.ConversationId);
                var authUsername = securityContext.UserName;
                //Map the messageDTO from front end to message object to save in the system
                var message = new Message
                {
                    ConversationId = messageDTO.ConversationId,
                    MessageContent = messageDTO.MessageContent,
                    OutgoingMessage = true,
                    CreatedDate = DateTime.Now
                };

                //Save the message to database
                try
                {
                    messengerManager.SaveMessageToDatabase(message, authUserId, contactId);
                }
                catch (DbUpdateException ex)
                {
                    return Content(HttpStatusCode.InternalServerError, "There is an error when saving message to database");
                }

                // Set up SignalR Hub to broadcast the FetchMessage command in receiver
                var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();

                // Get the message receiver's connection ID to broadcast FetchMessage command to
                // Lookup by contactId
                var connectionIDList = messengerManager.GetConnectionIdWithUserId(contactId);
                if (connectionIDList != null)
                {
                    // When message is saved to database, it will be saved to both auth user's conversation and receiver's conversation
                    //Get the conversation id of the conversation that just receive the new message from auth user.
                    //This conversation is the one belongs to receiver/.
                    int conversationIdToFetchMessage = messengerManager.GetConversationBetweenUsers(contactId, authUserId).Id;

                    //Then broadcast that conversation id to the recever only using connection Id
                    //Then the receiver will know the which conversation that has new message ,and fetch the message
                    foreach (ChatConnectionMapping cM in connectionIDList)
                    {

                        //ask the front end client to fetch the message from the conversation with given conversation Id
                        var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages(conversationIdToFetchMessage);

                    }
                    // string updatedToken = sm.RefreshSession(securityContext.Token);
                }

                // Create a messageDTO include in the response to the auth user
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
            //The authentication part start from here
            //Create security context from the token 
            UserManager um = new UserManager();
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(Request.Headers);
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }

            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            //Create authorization manager from security context to check claims
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
                // Find auth user id from auth user name 
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                // Find contact user name
                var contactUser = um.FindByUserName(newConversationMessageDTO.ContactUsername);
                Message returnMessage;
              
                
                if (contactUser != null)
                {
                    // Map the message to store in database 
                    var message = new Message
                    {
                        MessageContent = newConversationMessageDTO.MessageContent,
                        OutgoingMessage = true,
                        CreatedDate = DateTime.Now
                    };

                    // Save message to database
                    try
                    {
                        returnMessage =  messengerManager.SaveMessageToDatabase(message, authUserId, contactUser.Id);

                    }

                    catch (DbUpdateException ex)
                    {
                        return Content(HttpStatusCode.InternalServerError, "There is an error when saving message to database");
                    }

                    // Set up SignalR Hub to broadcast the FetchMessage command in receiver
                    var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();

                    // Get the message receiver's connection ID to broadcast FetchMessage command to
                    // Lookup by contactId
                    var connectionIDList = messengerManager.GetConnectionIdWithUserId(contactUser.Id);

                    
                    if (connectionIDList != null)
                    {
                        // When message is saved to database, it will be saved to both auth user's conversation and receiver's conversation
                        //Get the conversation id of the conversation that just receive the new message from auth user.
                        //This conversation is the one belongs to receiver/.
                        int conversationIdToFetchMessage = messengerManager.GetConversationBetweenUsers(contactUser.Id, authUserId).Id;

                        //Then broadcast that conversation id to the recever only using connection Id
                        //Then the receiver will know the which conversation that has new message ,and fetch the message
                        foreach (ChatConnectionMapping cM in connectionIDList)
                        {

                            var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages(conversationIdToFetchMessage);

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
            //The authentication part start from here
            //Create security context from the token 
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                 Request.Headers
             );
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }

            //Validate the token 
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            //Create authorization manager from security context to check claims
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

                // Get authUserId from auth username 
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                FriendRelationship friendRelationship = null;
                try
                {
                    // Try to add friend
                    friendRelationship = messengerManager.AddUserFriendList(authUserId, addedUsername);

                }

                catch (Exception ex)
                {
                    if(ex is MessageReceiverNotFoundException)
                    {
                        return Content(HttpStatusCode.NotFound, "User with the username does not exist to be added");
                    }

                    else if(ex is DuplicatedFriendException)
                    {
                        return Content(HttpStatusCode.Conflict, "User with the username is already in friend list ");
                    }

                    else if(ex is DbUpdateException)
                    {
                        return Content(HttpStatusCode.InternalServerError, "There is a error when saving friend relationship to database");

                    }
                }

                // Create FriendRelationship DTO to return to the front end to render the friendlist
                var friendRelationshipDTO = new FriendRelationshipDTO
                {
                    FriendId = friendRelationship.FriendId,
                    FriendUsername = um.FindUserById(friendRelationship.FriendId).UserName
                };

                return Ok(new { friend = friendRelationshipDTO }/*new { SITtoken = updatedToken }*/);
            }

            //string updatedToken = sm.RefreshSession(securityContext.Token);

        }

        [HttpGet]
        [ActionName("GetFriendList")]
        public IHttpActionResult GetFriendList()
        {
            //The authentication part start from here
            //Create security context from the token 
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized(); ;
            }

            //Validate the token 
            SessionManager sm = new SessionManager();


            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized(); ;
            }

            //Create authorization manager from security context to check claims
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

                // Get authUserId from auth username 
                authUserId = um.FindByUserName(securityContext.UserName).Id;

                // Get all friends in the friendlist
                var friendList = messengerManager.GetAllFriendRelationships(authUserId);
                if (friendList != null)
                {
                    // From here is creating list of friend to return to the UI for render
                    List<FriendRelationshipDTO> friendListDTO = new List<FriendRelationshipDTO>();
                 
                    foreach (FriendRelationship friend in friendList)
                    {
                        friendListDTO.Add(new FriendRelationshipDTO
                        {
                            FriendId = friend.FriendId,
                            FriendUsername = um.FindUserById(friend.FriendId).UserName
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
            //The authentication part start from here
            //Create security context from the token 
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

            //Create authorization manager from security context to check claims
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
           

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

                // Get authUserId from auth username 
                authUserId = um.FindByUserName(securityContext.UserName).Id;
                try
                {
                    //string updatedToken = sm.RefreshSession(securityContext.Token);

                    // Try to remove a user from the friendlist
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
