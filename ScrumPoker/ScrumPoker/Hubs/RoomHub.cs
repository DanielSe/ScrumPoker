using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScrumPoker.Code;

namespace ScrumPoker.Hubs
{
    public class RoomHub : Hub
    {
        private readonly IRoomRepository _roomRepository;

        public RoomHub(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task JoinRoom(string roomId, string participantId)
        {
            await Groups.Add(Context.ConnectionId, roomId);

            var room = _roomRepository.Read(roomId);
            var participants = room.Participants.Select(p => new {p.ParticipantId, p.Name, p.Email}).ToArray();
            Clients.Caller.initRoom(new {participants});
        }

        public Task LeaveRoom(string roomId, string participantId)
        {
            return Groups.Remove(Context.ConnectionId, roomId);
        }
    }
}