using CustomFrame.Web.Filters;
using System.Web.Mvc;

namespace CustomFrame.Web.Controllers.Home
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