using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using ScrumPoker.Models;

namespace ScrumPoker.Controllers
{
    public class RoomsController : Controller
    {
        // Shows a list of all available rooms or allows searching for rooms.
        // GET: /Rooms/

        public ActionResult Index()
        {
            var roomrepo = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
            var rooms = roomrepo.List();

            return View(rooms);
        }


        // Shows the dashboard from a room, opened through a link on the admin page of the room
        // GET: /Rooms/Dashboard/57fhanr
        public ActionResult Dashboard(string id)
        {
            var roomrepo = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
            var room = roomrepo.Read(id);

            return View(room);
        }

        // Shows the interface to create a new room
        // GET: /Rooms/Create
        public ActionResult Create()
        {
            return View(new Room());
        }

        // 
        // POST: /Rooms/Create
        [HttpPost]
        public ActionResult Create(Room room)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(room);

                // TODO: Add insert logic here
                var rooms = ScrumPokerKernel.Instance.Get<ICrud<Room, string>>();
                var theRoom = rooms.Create(room);
                
                return RedirectToAction("Index", "Admin", new { roomId = theRoom.RoomAdminId });
            }
            catch
            {
                return View(room);
            }
        }

        //
        // GET: /Rooms/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Rooms/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // GET: /Rooms/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Rooms/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public class CreateRoomViewModel
        {
            
            public string Name { get; set; }
            public string Description { get; set; }
            public string VoteSizes { get; set; }

            public string AdminEmail { get; set; }
        }
    }
}
