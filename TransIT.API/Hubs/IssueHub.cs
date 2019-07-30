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
<<<<<<< HEAD
                Context.User.FindFirst(ROLE.ROLE_SCHEMA)?.Value);
=======
                Context.User.FindFirst(RoleNames.Schema)?.Value
                );
>>>>>>> d3247408ee43def9136905acb2e7d45d485aaae9
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
<<<<<<< HEAD
                Context.User.FindFirst(ROLE.ROLE_SCHEMA)?.Value);
=======
                Context.User.FindFirst(RoleNames.Schema)?.Value
                );
>>>>>>> d3247408ee43def9136905acb2e7d45d485aaae9
            await base.OnDisconnectedAsync(exception);
        }
    }
}
