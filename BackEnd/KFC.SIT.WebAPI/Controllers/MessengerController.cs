using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.Messenger;
using WebAPI.Gateways.Messenger;
using WebAPI.UserManagement;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Signal;

namespace KFC.SIT.WebAPI
{
    
    
    public class MessengerController : ApiController
    {
        HttpContext httpContext;
        MessengerGateway mg;
        string username;
        public MessengerController()
        {
            httpContext = HttpContext.Current;
            mg = new MessengerGateway();
            username = httpContext.Request.Headers["Authorization"];

        }
        
        [HttpGet]
        [Route("{receiverUsername}")]
        public async Task<IQueryable<Conversation>> MessengerWithContact(string receiverUserName)
        {

             var conservations = await mg.GetConservationBetweenUser("nguyentrong56@gmail.com", receiverUserName);

            return conservations.AsQueryable();

        }

        [HttpGet]
        [Route("/ChatHistory")]
        public async Task<IQueryable<MessengerContactHist>> GetChatHistory(string senderUsername)
        {
            var chatHistory = await mg.GetAllContactHistory("nguyentrong56@gmail.com");
            return chatHistory.AsQueryable();
        }

        [HttpGet]
        public async Task<IQueryable<MessengerContactHist>> GetAllContactHistory()
        {
            var contactList = await mg.GetAllContactHistory("nguyentrong56@gmail.com");
            return contactList;
        }

        [HttpPost]
        public HttpStatusCode SendMessage([FromBody] Conversation conservation)
        {
            conservation.CreatedDate = DateTime.Now;
            mg.SendMessage(conservation);
            var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
            var result = myHub.Clients.All.FetchMessages(conservation);
            return HttpStatusCode.OK;


        }
    }
}
