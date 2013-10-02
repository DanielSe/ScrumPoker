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
            var roomrepo = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
            var room = roomrepo.List().FirstOrDefault(x => x.RoomAdminId == roomId);

            if (room == null)
                return HttpNotFound("Room not found.");

            return View(room);
        }

    }
}
