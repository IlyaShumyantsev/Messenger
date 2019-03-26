using BusinessLogic;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private DataManager dataManager;
        public SearchController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string firstName, string surname)
        {
            IEnumerable<User> model = dataManager.Users.GetUsers().Where(x => x.FirstName.ToLowerInvariant().StartsWith(firstName.ToLowerInvariant()) && x.Surname.ToLowerInvariant().StartsWith(surname.ToLowerInvariant()));
            return View(model);
        }
    }
}