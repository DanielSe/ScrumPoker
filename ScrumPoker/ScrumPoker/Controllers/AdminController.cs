using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using ScrumPoker.Code;
using ScrumPoker.Models;

namespace ScrumPoker.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IIssueRepository _issueRepository;

        public AdminController(IRoomRepository roomRepository, IIssueRepository issueRepository)
        {
            _roomRepository = roomRepository;
            _issueRepository = issueRepository;
        }

        //
        // GET: /Admin/{roomId}
        public ActionResult Index(string roomId)
        {
            var room = _roomRepository.List().FirstOrDefault(x => x.RoomAdminId == roomId);

            if (room == null)
                return HttpNotFound("Room not found.");

            return View(room);
        }


        public ActionResult CreateIssue(string roomId)
        {
            return View(new Issue());
        }

        [HttpPost]
        public ActionResult CreateIssue(string roomId, Issue issue)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(issue);

                var room = _roomRepository.Read(roomId);
                issue.Room = room;

                issue = _issueRepository.Create(issue);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(issue);
            }
        }

        public ActionResult SetIssue(string roomId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetIssue(string roomId, FormCollection form)
        {
            var room = _roomRepository.List().FirstOrDefault(x => x.RoomAdminId == roomId);

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
