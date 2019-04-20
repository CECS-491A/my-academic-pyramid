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
using WebAPI.Signal;
using System.Net.Http;
using DataAccessLayer.DTOs;
using System.Collections.Generic;
using WebAPI.UserManagement;

namespace KFC.SIT.WebAPI
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
 
    public class MessengerController : ApiController
    {
        HttpContext httpContext;
        MessengerManager messengerManager;
        string username;
        public MessengerController()
        {
            httpContext = HttpContext.Current;
            messengerManager = new MessengerManager();
            username = httpContext.Request.Headers["Authorization"];
           
        }

        [HttpGet]
        [ActionName("LoadMessageContact")]
        public IQueryable<Conversation> MessengerWithContact(int receiverUserId)
        {

            var conservations =  messengerManager.GetConversationBetweenUser(2, receiverUserId);

            return conservations.AsQueryable();

        }

        [HttpGet]
        [ActionName("LoadLatestMessageContact")]
        public async Task<Conversation> GetLatestMessageWithContact(int receiverUserId2)
        {

            var conservations = await messengerManager.GetLatestMessageBetweenUser(2, receiverUserId2);

            return conservations;

        }



        [HttpGet]
        [ActionName("GetContactHistory")]
        //[ActionName("GetContactHistory")]
        public IQueryable<MessengerContactHist> GetAllContactHistory()
        {
            var contactList =  messengerManager.GetAllContactHistory(2);
            return contactList;
        }

        [HttpPost]
        [ActionName("SendMessage")]
        //[ActionName("SendMessage")]
        public HttpResponseMessage SendMessage([FromBody] Conversation conversation)
        {
            conversation.CreatedDate = DateTime.Now;

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
                var connectionIDList = messengerManager.GetConnectionIdWithUserId(conversation.SenderId);
                {
                    foreach (ChatConnectionMapping cM in connectionIDList)
                    {
                        var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages(conversation.SenderId, conversation.ReceiverId);

                    }
                }
            return Request.CreateResponse(HttpStatusCode.OK, "Message is sent successfully");
            

            
            


            
            //var result = myHub.Clients.All.FetchMessages(conservation.SenderUserName, conservation.ReceiverUserName);

            


        }

        [HttpPost]
        [ActionName("AddFriend")]
        public HttpResponseMessage AddFriendContactList(string addedUsername)
        {
            int addingUserId = 2;
     

            try
            {
                
                messengerManager.AddUserFriendList(addingUserId, addedUsername);
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
            int userId = 2;

            try
            {
                var friendList = messengerManager.GetFriendRelationships(userId);
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
            int testAuthUserId = 2;
            try
            {
                messengerManager.RemoveUserFromFriendList(testAuthUserId, friendId);
                return Request.CreateResponse(HttpStatusCode.OK, "Friend is succesfully removed from the list");
            }
            catch(ArgumentException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);
            }

        }


    }


    
}
