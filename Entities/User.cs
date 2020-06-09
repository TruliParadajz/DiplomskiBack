using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Entities
{
    public class User
    {
        public User()
        {
            Events = new HashSet<EventTask>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Power { get; set; }

        public ICollection<EventTask> Events { get; set; }
        public UserNotification UserNotification { get; set; }
        public EventTaskNotification EventTaskNotification { get; set; }
    }
}
