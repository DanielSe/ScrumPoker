using System.Web;
using System.Web.Mvc;
using Ninject;
using ScrumPoker.Models;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace ScrumPoker.Controllers
{
    public class ClientController : Controller
    {
        private ICrud<Room, string> _rooms = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
        private IIdGenerator<string> _idGenerator = ScrumPokerKernel.Instance.Get<IIdGenerator<string>>();

        //
        // GET: /Client/
        public ActionResult Index(string roomId)
        {
            var participantCookie = Request.Cookies["ParticipantId"];
            
            if (participantCookie == null)
                return RedirectToAction("Join", new {roomId});

            var room = _rooms.Read(roomId);

            return View(new IndexViewModel { Room = room, Participant = GetParticipant(roomId) });
        }

        public ActionResult Join(string roomId)
        {
            var room = _rooms.Read(roomId);

            return View(room);
        }

        [HttpPost]
        public ActionResult Join(string roomId, FormCollection form)
        {
            var room = _rooms.Read(roomId);

            var participant = new Participant
                {
                    ParticipantId = _idGenerator.CreateId(),
                    Name = form["Name"],
                    Email = form["Email"]
                };
            
            Response.AppendCookie(new HttpCookie("ParticipantId", participant.ParticipantId) { Path = "/Client/" + roomId });

            room.Participants.Add(participant);

            return RedirectToAction("Index", new { roomId });
        }

        public ActionResult Leave(string roomId)
        {
            return RedirectToAction("Index", "Rooms");
        }

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

            var room = _rooms.Read(roomId);
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
