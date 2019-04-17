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

namespace KFC.SIT.WebAPI
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
 
    public class MessengerController : ApiController
    {
        HttpContext httpContext;
        MessengerGateway messengerManager;
        string username;
        public MessengerController()
        {
            httpContext = HttpContext.Current;
            messengerManager = new MessengerGateway();
            username = httpContext.Request.Headers["Authorization"];
           
        }

        [HttpGet]
        [ActionName("LoadMessageContact")]
        public IQueryable<Conversation> MessengerWithContact(string receiverUserName)
        {

            var conservations =  messengerManager.GetConservationBetweenUser("nguyentrong56@gmail.com", receiverUserName);

            return conservations.AsQueryable();

        }

        [HttpGet]
        [ActionName("LoadLatestMessageContact")]
        public async Task<Conversation> GetLatestMessageWithContact(string receiverUserName2)
        {

            var conservations = await messengerManager.GetLatestMessageBetweenUser("nguyentrong56@gmail.com", receiverUserName2);

            return conservations;

        }



        [HttpGet]
        [ActionName("GetContactHistory")]
        //[ActionName("GetContactHistory")]
        public IQueryable<MessengerContactHist> GetAllContactHistory()
        {
            var contactList =  messengerManager.GetAllContactHistory("nguyentrong56@gmail.com");
            return contactList;
        }

        [HttpPost]
        [ActionName("SendMessage")]
        //[ActionName("SendMessage")]
        public HttpStatusCode SendMessage([FromBody] Conversation conservation)
        {
            conservation.CreatedDate = DateTime.Now;
            messengerManager.SendMessage(conservation);
            var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
            MessengerHub mHub = new MessengerHub();


            var connectionIDList =  messengerManager.GetConnectionIdWithUserName(conservation.SenderUserName);
            {
                foreach(ChatConnectionMapping cM in connectionIDList)
                {
                    var result = myHub.Clients.Client(cM.ConnectionId).FetchMessages(conservation.SenderUserName, conservation.ReceiverUserName);

                }
            }
            //var result = myHub.Clients.All.FetchMessages(conservation.SenderUserName, conservation.ReceiverUserName);

            return HttpStatusCode.OK;


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
            string username = "nguyentrong56@gmail.com";

            try
            {
                var friendList = messengerManager.GetFriendRelationships(username);
                List <FriendContactDTO> friendListDTO = new List<FriendContactDTO>();

                foreach(FriendRelationship friend in friendList)
                {
                    friendListDTO.Add(new FriendContactDTO
                    {
                        FriendUsername = friend.friendUsername

                    });
                }


                
                return Request.CreateResponse(HttpStatusCode.OK, friendListDTO);
            }

            catch(Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Friendlist is not avalaible to retrieve");
            }
            

            


        }


    }
}
