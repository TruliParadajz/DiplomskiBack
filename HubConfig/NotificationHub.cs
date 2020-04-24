using BackendApi.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BackendApi.HubConfig
{
    public class NotificationHub : Hub
    {
        [HubMethodName("transfernotifications")]
        public async Task Send(EmailNotificationModel emailModel)
        {
            await Clients.User(emailModel.UserId.ToString()).SendAsync("transfernotifications", emailModel);
        }
    }
}
