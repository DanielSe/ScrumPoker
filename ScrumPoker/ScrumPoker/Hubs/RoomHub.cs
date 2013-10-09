using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumPoker.Hubs
{
    public class RoomHub : Hub
    {
        public Task JoinRoom(string roomId, string participantId)
        {
            return Groups.Add(Context.ConnectionId, roomId);
        }

        public Task LeaveRoom(string roomId, string participantId)
        {
            return Groups.Remove(Context.ConnectionId, roomId);
        }
    }
}