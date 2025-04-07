using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    public class NotificationModel
    {
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public string OnclickUrl { get; set; }
    }
}