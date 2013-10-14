using ScrumPoker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScrumPoker.ApiControllers
{
    public class AdminController : ApiController
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IParticipantRepository _participantRepository;

        public AdminController(IRoomRepository roomRepository, IParticipantRepository participantRepository)
        {
            _roomRepository = roomRepository;
            _participantRepository = participantRepository;
        }

        [HttpGet]
        public string KickParticipant(string roomId, string participantId)
        {
            var room = _roomRepository.ReadByAdminId(roomId);;
            if(room == null)
                throw new Exception("Invalid room.");

            var participant = room.Participants.FirstOrDefault(x => x.ParticipantId == participantId);
            if(participant == null)
                throw new Exception("Invalid participant.");

            _participantRepository.Delete(participant);
            
            return "OK";
        }

        [HttpGet]
        public string SetCurrentIssue(string roomId, string issueId)
        {
            return "OK";
        }
    }
}
