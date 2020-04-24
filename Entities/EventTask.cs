using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class EventTask
    {
        public int Id { get; set; }
        public DateTime StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public string Title { get; set; }
        public string Colour { get; set; }
        public bool? Resizable { get; set; }
        public bool? Draggable { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
