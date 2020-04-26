using BackendApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.HubConfig
{
    public class NotificationHub : Hub
    {
        static HashSet<HubConnectionsModel> CurrentConnections = new HashSet<HubConnectionsModel>();

        [HubMethodName("transfernotifications")]
        public async Task Send(EmailNotificationModel emailModel)
        {
            var connectionId = GetAllActiveConnections()
                .Where(x => x.UserName == emailModel.UserId.ToString())
                .FirstOrDefault()
                .ConnectionId;

            await Clients.Client(connectionId).SendAsync("transfernotifications", emailModel);
        }

        public override Task OnConnectedAsync()
        {
            HubConnectionsModel model = new HubConnectionsModel()
            {
                ConnectionId = Context.ConnectionId,
                UserName = Context.User.Identity.Name
            };
            CurrentConnections.Add(model);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connection = CurrentConnections.FirstOrDefault(x => x.UserName == Context.User.Identity.Name);

            if (connection != null)
            {
                CurrentConnections.Remove(connection);
            }
            return base.OnDisconnectedAsync(exception);
        }

        //return list of all active connections
        public List<HubConnectionsModel> GetAllActiveConnections()
        {
            return CurrentConnections.ToList();
        }
    }
}