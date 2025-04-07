using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.SignalRHubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        //[HubMethodName("pushNotification")]
        //public void PushNotification(string title, string message)
        //{
        //    Clients.All.newNotification(title, message);
        //}
    }
}