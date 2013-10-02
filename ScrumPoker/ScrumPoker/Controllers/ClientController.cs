using System.Web;
using System.Web.Mvc;
using Ninject;
using ScrumPoker.Models;

namespace ScrumPoker.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/

        public ActionResult Index(string roomId)
        {
            var participantCookie = Request.Cookies["ParticipantId"];
            
            if (participantCookie == null)
                return RedirectToAction("Join", new {roomId});

            var roomrepo = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
            var room = roomrepo.Read(roomId);

            return View(room);
        }

        public ActionResult Join(string roomId)
        {
            var roomrepo = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
            var room = roomrepo.Read(roomId);

            return View(room);
        }

        [HttpPost]
        public ActionResult Join(string roomId, FormCollection form)
        {
            var roomrepo = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
            var room = roomrepo.Read(roomId);

            var participant = new Participant
                {
                    ParticipantId = ScrumPokerKernel.Instance.Get<IIdGenerator<string>>().CreateId(),
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

        public ActionResult Vote(string roomId, string issueId, string voteSize)
        {
            return RedirectToAction("Index");
        }

    }
}
