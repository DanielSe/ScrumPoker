using System;
using ScrumPoker.Code;
using System.Web.Mvc;
using ScrumPoker.Models;

namespace ScrumPoker.Controllers
{
    public class IssueController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IIssueRepository _issueRepository;

        public IssueController(IRoomRepository roomRepository, IIssueRepository issueRepository)
        {
            _roomRepository = roomRepository;
            _issueRepository = issueRepository;
        }

        //
        // GET: /Issue/Create?rid={roomId}
        [HttpGet]
        public ActionResult Create(string rid)
        {
            if (string.IsNullOrEmpty(rid))
                return HttpNotFound();

            var room = _roomRepository.ReadByAdminId(rid);

            if (room == null)
                return HttpNotFound();

            var issue = new Issue {RoomId = room.RoomId, Room = room};
            return View(issue);
        }

        //
        // POST: /Issue/Create
        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            try
            {
                var room = _roomRepository.Read(issue.RoomId);

                if (room == null)
                    return HttpNotFound();

                if (!ModelState.IsValid)
                    return View(issue);

                issue.Room = room;
                _issueRepository.Create(issue);

                return RedirectToAction("Index", "Admin", new { roomAdminId = room.RoomAdminId });
            }
            catch
            {
                return View(issue);
            }
        }

        //
        // GET: /Issue/Edit/5

        public ActionResult Edit(string id)
        {
            var issue = _issueRepository.Read(id);

            if (issue == null)
                return HttpNotFound();

            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
            try
            {
                var room = _roomRepository.Read(issue.RoomId);

                if (!ModelState.IsValid)
                    return View(issue);

                _issueRepository.Update(issue);

                return RedirectToAction("Index", "Admin", new { roomAdminId = room.RoomAdminId });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex);
                return View(issue);
            }
        }

        
        //
        // GET: /Issue/Delete/5
        public ActionResult Delete(string id)
        {
            var issue = _issueRepository.Read(id);

            if (issue == null)
                return HttpNotFound();

            try
            {   
                var room = _roomRepository.Read(issue.RoomId);
                if (room.CurrentIssueId == issue.IssueId)
                {
                    room.CurrentIssueId = null;
                    _roomRepository.Update(room);
                }

                _issueRepository.Delete(issue);

                return RedirectToAction("Index", "Admin", new { roomAdminId = room.RoomAdminId });
            }
            catch
            {
                return View("Edit", issue);
            }
        }
    }
}
