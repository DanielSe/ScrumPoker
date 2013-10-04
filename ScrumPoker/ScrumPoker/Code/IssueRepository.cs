using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ScrumPoker.Models;

namespace ScrumPoker.Code
{
    public class IssueRepository : IIssueRepository
    {
        private readonly ScrumPokerContext _db = new ScrumPokerContext();
        private readonly IIdGenerator<string> _idg;

        public IssueRepository(IIdGenerator<string> idg)
        {
            _idg = idg;
        }

        public Issue Create(Issue entity)
        {
            if (string.IsNullOrEmpty(entity.IssueId))
                entity.IssueId = _idg.CreateId();

            var r = _db.Issues.Add(entity);
            _db.SaveChanges();

            return r;
        }

        public Issue Read(string key)
        {
            return _db.Issues.Find(key);
        }

        public Issue Update(Issue entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();

            return entity;
        }

        public void Delete(Issue entity)
        {
            _db.Issues.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Issue> List()
        {
            return _db.Issues.AsEnumerable();
        }
    }
}