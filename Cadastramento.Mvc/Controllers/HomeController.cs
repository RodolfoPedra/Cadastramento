using System.Web.Mvc;

namespace Cadastramento.Mvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}