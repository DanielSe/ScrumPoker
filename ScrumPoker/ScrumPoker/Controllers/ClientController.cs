using System.IO;
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
using ZXing;
using ZXing.Common;
using System.Drawing.Imaging;

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
        // GET: /Client/{id}
        public ActionResult Index(string id)
        {
            var room = _roomRepository.Read(id);
            if (room == null)
                return HttpNotFound();

            var participant = GetParticipant();

            if (participant == null)
            {
                Response.Cookies.Remove("ParticipantId");
                return RedirectToAction("Join", new { id });
            }

            return View(new IndexViewModel { Room = room, Participant = participant });
        }

        

        //
        // GET: /Client/{roomId}/Join
        public ActionResult Join(string id)
        {
            var room = _roomRepository.Read(id);

            if (room == null)
                return HttpNotFound();

            var participant = new Participant
                {
                    Room = room,
                    RoomId = room.RoomId
                };

            return View(participant);
        }

        //
        // POST: /Client/{roomId}/Join
        [HttpPost]
        public ActionResult Join(string id, Participant participant)
        {
            var room = _roomRepository.Read(id);

            if (room == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
                return View(participant);

            try
            {
                participant.Room = room;
                participant.ParticipantId = _idGenerator.CreateId();

                _participantRepository.Create(participant);

                RoomBroadcast.ParticipantJoins(participant);
                Response.AppendCookie(new HttpCookie("ParticipantId", participant.ParticipantId) {Path = "/Client/" + id});

                return RedirectToAction("Index", new { id = room.RoomId });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e);
                return View(participant);
            }
        }

        //
        //
        public ActionResult Leave(string id)
        {
            // TODO
            return RedirectToAction("Index", "Rooms");
        }

        //
        //
        public ActionResult Vote(string id, string vote)
        {
            var participant = GetParticipant();
            participant.Vote = vote;

            return RedirectToAction("Index");
        }





        private Participant GetParticipant()
        {
            var cookie = Request.Cookies["ParticipantId"];
            if (cookie == null)
                return null;

            var pid = cookie.Value;
            if (string.IsNullOrEmpty(pid))
                return null;

            var participant = _participantRepository.Read(pid);
            return participant;
        }




        public class IndexViewModel
        {
            public Room Room { get; set; }
            public Participant Participant { get; set; }
        }
    }
}
