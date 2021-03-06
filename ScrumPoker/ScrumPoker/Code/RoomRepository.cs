﻿using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ScrumPoker.Models;

namespace ScrumPoker.Code
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ScrumPokerContext _db;
        private readonly IIdGenerator<string> _idg;

        public RoomRepository(ScrumPokerContext context, IIdGenerator<string> idg)
        {
            _db = context;
            _idg = idg;
        }

        public Room Create(Room entity)
        {
            if (string.IsNullOrEmpty(entity.RoomId))
                entity.RoomId = _idg.CreateId();

            if (string.IsNullOrEmpty(entity.RoomAdminId))
                entity.RoomAdminId = _idg.CreateId();

            var r = _db.Rooms.Add(entity);
            _db.SaveChanges();
            return r;
        }

        public Room Read(string key)
        {
            var room = _db.Rooms.Find(key);
            return room;
        }

        public Room Update(Room entity)
        {
            var r = _db.Rooms.Find(entity.RoomId);
            _db.Entry(r).CurrentValues.SetValues(entity);
            _db.SaveChanges();

            return r;
        }

        public void Delete(Room entity)
        {
            _db.Rooms.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Room> List()
        {
            return _db.Rooms.AsEnumerable();
        }

        public Room ReadByAdminId(string adminId)
        {
            return _db.Rooms.SingleOrDefault(x => x.RoomAdminId == adminId);
        }
    }
}