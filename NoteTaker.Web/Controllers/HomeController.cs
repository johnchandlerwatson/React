using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using NoteTaker.Core.Command;
using NoteTaker.Core.Models;
using NoteTaker.Core.Queries;

namespace NoteTaker.Web.Controllers
{
    public class HomeController : DbContextController
    {
        private static readonly IList<Note> _notes;

        static HomeController()
        {
            var notes = Query(new GetNotesQuery());
            _notes = notes.OrderByDescending(x => x.CreatedDate).ToList();
        }     

        [HttpPost]
        public ActionResult AddNote(Note note)
        {
            _notes.Insert(0, note);
            Execute(new AddNoteCommand(note));
            return Content("Success :)");
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Notes()
        {
            return Json(_notes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}