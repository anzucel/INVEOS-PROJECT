using Microsoft.AspNetCore.Mvc;

namespace INVEOS.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
    }
}
