using System.Drawing.Imaging;
using System.IO;
using ScrumPoker.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;

namespace ScrumPoker.Controllers
{
    public class DashboardController : Controller
    {
        private IRoomRepository _roomRepository;

        public DashboardController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: /Dashboard/{id}
        [HttpGet]
        public ActionResult Index(string id)
        {
            var room = _roomRepository.Read(id);

            if (room == null)
                return HttpNotFound();

            return View(room);
        }

        // GET: /Dashboard/{id}/QrImage
        [HttpGet]
        public ActionResult QrImage(string id)
        {
            var room = _roomRepository.Read(id);

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
            var url = Url.Action("Index", "Client", new { id });
            var bitmap = writer.Write(baseUrl + url);
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.ToArray(), "image/png");
        }
    }
}
