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

        public IssueRepository(ScrumPokerContext context, IIdGenerator<string> idg)
        {
            _db = context;
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
            var i = _db.Issues.Find(entity.IssueId);
            _db.Entry(i).CurrentValues.SetValues(entity);
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