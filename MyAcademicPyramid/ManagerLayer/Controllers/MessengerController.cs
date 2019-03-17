using DataAccessLayer;
using DataAccessLayer.Models.Messenger;
using ManagerLayer.Gateways.Messenger;
using ManagerLayer.SignalRHub;
using ManagerLayer.UserManagement;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagerLayer.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        public IQueryable<Conversation> MessengerWithContact(String receiverUserName)
        {

            List<Conversation> conservations = mg.GetConservationBetweenUser(username, receiverUserName);

            return conservations.AsQueryable();

        }

        [HttpPost]

        public HttpStatusCode SendMessage([FromBody] Conversation conservation)
        {
            conservation.SenderUserName = username;
            conservation.CreatedDate = DateTime.Now;
            mg.SendMessage(conservation);
            var myHub = GlobalHost.ConnectionManager.GetHubContext<MessengerHub>();
            var result = myHub.Clients.All.FetchMessages();
            return HttpStatusCode.OK;


        }
    }
}
