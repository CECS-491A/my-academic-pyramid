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
        [Route("/LoadMessageContact")]
        //[ActionName("LoadMessageContact")]
        //[Route("api/Messenger/LoadMessageContact/{receiverUserName}")]
        public async Task<IQueryable<Conversation>> MessengerWithContact(string receiverUserName)
        {

            var conservations = await messengerManager.GetConservationBetweenUser("nguyentrong56@gmail.com", receiverUserName);

            return conservations.AsQueryable();

        }



        [HttpGet]
        [Route("/GetContactHistory")]
        //[ActionName("GetContactHistory")]
        public async Task<IQueryable<MessengerContactHist>> GetAllContactHistory()
        {
            var contactList = await messengerManager.GetAllContactHistory("nguyentrong56@gmail.com");
            return contactList;
        }

        [HttpPost]
        [Route("/SendMessage")]
        //[ActionName("SendMessage")]
        public HttpStatusCode SendMessage([FromBody] Conversation conservation)
        {
            conservation.CreatedDate = DateTime.Now;
            messengerManager.SendMessage(conservation);
            var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
            MessengerHub mHub = new MessengerHub();
            var connectionId = mHub.GetConnectionString(conservation.SenderUserName);
            var result = myHub.Clients.Client(connectionId).FetchMessages(conservation.SenderUserName, conservation.ReceiverUserName);
            return HttpStatusCode.OK;


        }

        [HttpPost]
        [ActionName("AddFriend")]
        public HttpResponseMessage AddFriendContactList(int addedUserId)
        {
            int addingUserId = 1;

            try
            {
                messengerManager.AddUserFriendList(addingUserId, addedUserId);
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
    }
}
