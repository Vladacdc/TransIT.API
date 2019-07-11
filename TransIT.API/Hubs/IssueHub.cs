using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TransIT.API.Extensions;

namespace TransIT.API.Hubs
{
    [Authorize(Roles = "ENGINEER")]
    public class IssueHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                Context.User.FindFirst(RoleConsts.RoleSchema)?.Value
                );
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                Context.User.FindFirst(RoleConsts.RoleSchema)?.Value
                );
            await base.OnDisconnectedAsync(exception);
        }
    }
}
