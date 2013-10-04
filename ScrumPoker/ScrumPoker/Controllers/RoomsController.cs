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
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // Shows a list of all available rooms or allows searching for rooms.
        // GET: /Rooms/

        public ActionResult Index()
        {
            var rooms = _roomRepository.List();

            return View(rooms);
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
                var theRoom = _roomRepository.Create(room);
                
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
    }
}
