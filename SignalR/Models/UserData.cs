using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    public class UserData
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string UserEmail { get; set; }
    }
}