using Taki.Web.Filters;
using System.Web.Mvc;

namespace Taki.Web.Controllers.Home
{
    public class TestExceptionController : Controller
    {
        // GET: TestException
        [AjaxException]
        public ActionResult Index()
        {
            return View();
        }
    }
}