using System;
using System.Collections.Generic;
using ScrumPoker.Models;

namespace ScrumPoker.Code
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly ScrumPokerContext _db;
        private readonly IIdGenerator<string> _idGenerator;

        public ParticipantRepository(ScrumPokerContext context, IIdGenerator<string> idGenerator)
        {
            _db = context;
            _idGenerator = idGenerator;
        }

        public Participant Create(Participant entity)
        {
            if (string.IsNullOrEmpty(entity.ParticipantId))
                entity.ParticipantId = _idGenerator.CreateId();

            var r = _db.Participants.Add(entity);
            _db.SaveChanges();

            return r;
        }

        public Participant Read(string key)
        {
            throw new NotImplementedException();
        }

        public Participant Update(Participant entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Participant entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Participant> List()
        {
            throw new NotImplementedException();
        }
    }
}