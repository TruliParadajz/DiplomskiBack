using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    public class EmailNotificationModel
    {
        public int UserId { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int EventTaskId { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public double Hours { get; set; }
    }
}
