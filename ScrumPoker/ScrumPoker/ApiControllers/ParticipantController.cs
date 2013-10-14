using System;
using System.Linq;
using System.Web.Http;
using ScrumPoker.Code;

namespace ScrumPoker.ApiControllers
{
    public class ParticipantController : ApiController
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantController(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        [HttpGet]
        public string CastVote(string participantId, string vote)
        {
            var participant = _participantRepository.Read(participantId);

            if (participant == null)
                throw new Exception("Invalid participant identity.");

            if(string.IsNullOrEmpty(vote))
                throw new Exception("Vote required.");

            if(!participant.Room.VoteSizes.Contains(vote))
                throw new Exception("Invalid vote option.");

            participant.Vote = vote;
            _participantRepository.Update(participant);

            return "OK";
        }
    }
}
