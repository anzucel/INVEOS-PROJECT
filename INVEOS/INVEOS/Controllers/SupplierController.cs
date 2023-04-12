using Microsoft.AspNetCore.Mvc;

namespace INVEOS.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            IEnumerable<Models.Supplier> suppliers = await Functions.APIService.GetList<Models.Supplier>("Supplier");
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
    }
}
