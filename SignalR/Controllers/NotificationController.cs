using System.Data.Entity;
using System;
using System.Web.Mvc;
using SignalR.Models;
using System.Linq;

namespace SignalR.Controllers
{
    public class NotificationController : Controller
    {
        private readonly RealTimeDB _RealTimeDB;

        public NotificationController()
        {
            _RealTimeDB = new RealTimeDB();
        }

        //[HttpPost]
        //public ActionResult TrackUser()
        //{
        //    //int userId = User.Identity.Name; // Get notificationUser ID from authentication
        //    //if (!string.IsNullOrEmpty(userId))
        //    //{
        //    var users = _RealTimeDB.UsersData.ToList();
        //    var userEmail = users.FirstOrDefault(v => v.UserEmail == User.Identity.Email)?.UserEmail;
        //    var notificationUser = _RealTimeDB.Notifications.FirstOrDefault(c => c.UserEmail == userEmail);
        //    if (notificationUser != null)
        //    {
        //        notificationUser.LastSeen = DateTime.UtcNow; // Track last seen
        //    }
        //    else
        //    {
        //        notificationUser = new Notification();
        //        notificationUser.UserName = User.Identity.Name;              
        //        notificationUser.UserEmail = User.Identity.Email;
        //        notificationUser.LastSeen = DateTime.UtcNow;
        //        _RealTimeDB.Notifications.Add(notificationUser);
        //    }
        //    _RealTimeDB.SaveChanges();

        //    //}
        //    return Json(new { success = true });
        //}


    }
}
