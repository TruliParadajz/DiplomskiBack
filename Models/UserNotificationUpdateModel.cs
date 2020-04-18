using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    public class UserNotificationUpdateModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool EmailNotification { get; set; }
        public bool AppNotification { get; set; }
        public double Hours { get; set; }
    }
}
