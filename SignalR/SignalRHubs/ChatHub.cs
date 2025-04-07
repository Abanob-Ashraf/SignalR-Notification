using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.SignalRHubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        [HubMethodName("sendMessage")]
        public void SendMessage(Message message)
        {
            Clients.All.newMessage(message);

            RealTimeDB realTimeDB = new RealTimeDB();
            message.Date = DateTime.Now;
            realTimeDB.Messages.Add(message);
            realTimeDB.SaveChanges();
        }
    }
}