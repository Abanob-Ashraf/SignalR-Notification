using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SignalR.Models
{
    public partial class RealTimeDB : DbContext
    {
        public RealTimeDB()
            : base("name=RealTimeDB")
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<UserData> UsersData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
