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
        // GET: /Client/{roomId}
        public ActionResult Index(string roomId)
        {
            var room = _roomRepository.Read(roomId);
            if (room == null)
                return HttpNotFound();

            var participant = GetParticipant();

            if (participant == null)
            {
                Response.Cookies.Remove("ParticipantId");
                return RedirectToAction("Join", new { roomId });
            }

            return View(new IndexViewModel { Room = room, Participant = participant });
        }

        // Shows the dashboard from a room, opened through a link on the admin page of the room
        // GET: /Rooms/Dashboard/57fhanr
        public ActionResult Dashboard(string id)
        {
            var room = _roomRepository.Read(id);

            return View(room);
        }

        //
        // GET: /Client/{roomId}/Join
        public ActionResult Join(string roomId)
        {
            var room = _roomRepository.Read(roomId);

            return View(room);
        }

        //
        // POST: /Client/{roomId}/Join
        [HttpPost]
        public ActionResult Join(string roomId, FormCollection form)
        {
            var room = _roomRepository.Read(roomId);

            var participant = new Participant
                {
                    ParticipantId = _idGenerator.CreateId(),
                    RoomId = roomId,
                    Room = room,
                    Name = form["Name"],
                    Email = form["Email"]
                };

            _participantRepository.Create(participant);

            RoomBroadcast.ParticipantJoins(participant);
            Response.AppendCookie(new HttpCookie("ParticipantId", participant.ParticipantId) { Path = "/Client/" + roomId });

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
            var participant = GetParticipant();
            participant.Vote = vote;

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult QrImage(string roomId)
        {
            var room = _roomRepository.Read(roomId);

            if (room == null)
                return HttpNotFound();

            var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions()
                        {
                            Width = 400,
                            Height = 400
                        },
                };

            var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var url = Url.Action("Index", "Client", new {roomId});
            var bitmap = writer.Write(baseUrl + url);
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.ToArray(), "image/png");
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
