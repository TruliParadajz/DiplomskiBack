using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.HubConfig
{
    public interface INotificationHub
    {
        Task Send(string message);
    }
}
