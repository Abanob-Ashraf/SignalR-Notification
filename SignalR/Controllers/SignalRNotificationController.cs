using Microsoft.AspNet.SignalR;
using SignalR.Models;
using SignalR.SignalRHubs;
using System.Web.Http;

namespace SignalR.Controllers
{
    [RoutePrefix("api/signalr")]
    public class SignalRNotificationController : ApiController
    {
        private readonly IHubContext _hubContext;
        public SignalRNotificationController()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        }
        [HttpPost]
        [Route("sendNotification")]
        public IHttpActionResult PushNotification(NotificationModel notificationModel)
        {
            if (notificationModel == null)
            {
                return BadRequest("Invalid request");
            }
            _hubContext.Clients.All.newNotification(notificationModel);
            return Ok(notificationModel);
        }
    }
}
