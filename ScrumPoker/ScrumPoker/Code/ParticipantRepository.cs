using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            if (r.Room != null)
            {
                r.Room.Participants.Add(r);
            }

            _db.SaveChanges();

            return r;
        }

        public Participant Read(string key)
        {
            return _db.Participants.Find(key);
        }

        public Participant Update(Participant entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();

            return entity;
        }

        public void Delete(Participant entity)
        {
            entity.Room.Participants.Remove(entity);
            _db.Participants.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Participant> List()
        {
            return _db.Participants.ToList();
        }
    }
}