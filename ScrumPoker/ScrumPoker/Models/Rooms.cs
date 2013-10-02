using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumPoker.Models
{
    public class Rooms : ICrud<Room, string>
    {
        private readonly List<Room> _rooms = new List<Room>();
        private readonly IIdGenerator<string> _idGenerator;

        public Rooms(IIdGenerator<string> idGenerator)
        {
            _idGenerator = idGenerator;
            _rooms = new List<Room>();
        }

        public Room Create(Room room)
        {
            if (string.IsNullOrEmpty(room.RoomId))
                room.RoomId = _idGenerator.CreateId();
            if (string.IsNullOrEmpty(room.RoomAdminId))
                room.RoomAdminId = _idGenerator.CreateId();

            _rooms.Add(room);

            return room;
        }

        public Room Read(string roomId)
        {
            return _rooms.FirstOrDefault(x => x.RoomId == roomId);
        }

        public Room Update(Room room)
        {
            Delete(room);
            Create(room);
            
            return room;
        }

        public void Delete(Room room)
        {
            _rooms.RemoveAll(x => x.RoomId == room.RoomId);
        }

        public IEnumerable<Room> List()
        {
            return _rooms.AsReadOnly();
        }
    }
}