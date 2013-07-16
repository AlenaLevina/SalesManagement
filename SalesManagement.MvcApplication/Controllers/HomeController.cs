using System.Web.Mvc;

namespace SalesManagement.MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect(Url.Action("ViewProfile", "Account"));
        }

#if DEBUG
        public ActionResult Home()
        {
            return View("Index");
        }
#endif

    }
}
