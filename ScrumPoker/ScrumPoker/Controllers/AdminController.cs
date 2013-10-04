using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using ScrumPoker.Models;

namespace ScrumPoker.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/{roomId}
        public ActionResult Index(string roomId)
        {
            var roomrepo = ScrumPokerKernel.Instance.Get<IRoomRepository>();
            var room = roomrepo.List().FirstOrDefault(x => x.RoomAdminId == roomId);

            if (room == null)
                return HttpNotFound("Room not found.");

            return View(room);
        }


        public ActionResult SetIssue(string roomId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetIssue(string roomId, FormCollection form)
        {
            var roomrepo = ScrumPokerKernel.Instance.Get<IRoomRepository>();
            var room = roomrepo.List().FirstOrDefault(x => x.RoomAdminId == roomId);

            var issue = new Issue()
                {
                    IssueId = ScrumPokerKernel.Instance.Get<IIdGenerator<string>>().CreateId(),
                    Name = form["Name"],
                    Description = form["Description"]
                };

            room.CurrentIssue = issue;
            foreach (var p in room.Participants)
                p.Vote = null;

            return RedirectToAction("Index", new {roomId});
        }

    }
}
