using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BackendApi.HubConfig;
using BackendApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IHubContext<NotificationHub> _hub;

        public NotificationController(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> DoWork()
        {
            await _hub.Clients.All.SendAsync("transfernotifications", "NotificationHub service start...");
            return Accepted(1);
        }
    }
}