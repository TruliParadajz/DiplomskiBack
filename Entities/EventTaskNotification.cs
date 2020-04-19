using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class EventTaskNotification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserNotificationId { get; set; }
        public int UserId { get; set; }
        public int EventTaskId { get; set; }
        public int EmailNotification { get; set; }
        public int AppNotification { get; set; }

        public User User { get; set; }
        public UserNotification UserNotification { get; set; }
        public EventTask EventTask { get; set; }
    }
}
