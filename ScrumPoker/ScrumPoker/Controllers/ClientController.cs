using System.Web;
using System.Web.Mvc;
using Ninject;
using ScrumPoker.Code;
using ScrumPoker.Hubs;
using ScrumPoker.Models;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.AspNet.SignalR;

namespace ScrumPoker.Controllers
{
    public class ClientController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IParticipantRepository _participantRepository;

        private IIdGenerator<string> _idGenerator = ScrumPokerKernel.Instance.Get<IIdGenerator<string>>();

        public ClientController(IRoomRepository roomRepository, IParticipantRepository participantRepository)
        {
            _roomRepository = roomRepository;
            _participantRepository = participantRepository;
        }

        //
        // GET: /Client/
        public ActionResult Index(string roomId)
        {
            var participantCookie = Request.Cookies["ParticipantId"];
            
            if (participantCookie == null)
                return RedirectToAction("Join", new {roomId});

            var room = _roomRepository.Read(roomId);

            return View(new IndexViewModel { Room = room, Participant = GetParticipant(roomId) });
        }

        // Shows the dashboard from a room, opened through a link on the admin page of the room
        // GET: /Rooms/Dashboard/57fhanr
        public ActionResult Dashboard(string id)
        {
            var room = _roomRepository.Read(id);

            return View(room);
        }

        //
        //
        public ActionResult Join(string roomId)
        {
            var room = _roomRepository.Read(roomId);

            return View(room);
        }

        //
        //
        [HttpPost]
        public ActionResult Join(string roomId, FormCollection form)
        {
            var room = _roomRepository.Read(roomId);

            var participant = new Participant
                {
                    ParticipantId = _idGenerator.CreateId(),
                    RoomId = roomId,
                    Name = form["Name"],
                    Email = form["Email"]
                };

            RoomBroadcast.ParticipantJoins(participant);

            Response.AppendCookie(new HttpCookie("ParticipantId", participant.ParticipantId) { Path = "/Client/" + roomId });

            room.Participants.Add(participant);

            return RedirectToAction("Index", new { roomId });
        }

        //
        //
        public ActionResult Leave(string roomId)
        {
            return RedirectToAction("Index", "Rooms");
        }

        //
        //
        public ActionResult Vote(string roomId, string vote)
        {
            var participant = GetParticipant(roomId);
            participant.Vote = vote;

            return RedirectToAction("Index");
        }









        private Participant GetParticipant(string roomId)
        {
            var cookie = Request.Cookies["ParticipantId"];
            if (cookie == null)
                return null;

            var pid = cookie.Value;
            if (string.IsNullOrEmpty(pid))
                return null;

            var room = _roomRepository.Read(roomId);
            if (room == null)
                return null;

            var participant = room.Participants.FirstOrDefault(x => x.ParticipantId == pid);
            return participant;
        }




        public class IndexViewModel
        {
            public Room Room { get; set; }
            public Participant Participant { get; set; }
        }
    }
}
