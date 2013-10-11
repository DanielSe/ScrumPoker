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
        public ActionResult Index(string roomAdminId)
        {
            var room = FindRoomByAdminId(roomAdminId);

            if (room == null)
                return HttpNotFound("Room not found.");

            return View(room);
        }


        public ActionResult CreateIssue(string roomAdminId)
        {
            var room = FindRoomByAdminId(roomAdminId);
            var issue = new Issue() {Room = room, RoomId = room.RoomId};
            return View(issue);
        }

        [HttpPost]
        public ActionResult CreateIssue(string roomAdminId, Issue issue)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(issue);

                issue = _issueRepository.Create(issue);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(issue);
            }
        }

        public ActionResult SetIssue(string roomAdminId, string issueId)
        {
            var room = FindRoomByAdminId(roomAdminId);

            var issue = room.Issues.FirstOrDefault(x => x.IssueId == issueId);

            if (issue == null)
                throw new Exception("Issue with is " + issueId + " was not found in room " + room.Name + " (" + room.RoomId + ")");

            room.CurrentIssueId = issueId;
            _roomRepository.Update(room);

            RoomBroadcast.NewIssue(issue);

            return RedirectToAction("Index", new {roomId = roomAdminId});
        }


        private Room FindRoomByAdminId(string roomAdminId)
        {
            return _roomRepository.List().FirstOrDefault(x => x.RoomAdminId == roomAdminId);
        }
    }
}
