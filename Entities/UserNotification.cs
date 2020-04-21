using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class UserNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool EmailNotification { get; set; }
        public bool AppNotification { get; set; }
        public double Hours { get; set; }
    }
}
