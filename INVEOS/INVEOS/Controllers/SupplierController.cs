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
            try
            {
                IEnumerable<Models.Supplier> suppliers = await Functions.APIService.GetList<Models.Supplier>("Supplier");
                return View(suppliers);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View();
                //return RedirectToAction("Dashboard", "Graphics");
                throw;

                throw;
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("SupplierId,Name,Email,Phone,Address")] INVEOS.Models.Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Functions.APIService.Set(supplier, "Supplier");
                    ViewBag.JavaScriptFunction = string.Format("successfulAlert()");
                }
                else
                {
                    TempData["error"] = "No se ha podido agregar el proveedor";
                }

            return View("Add");
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View("Add");
                throw;
            }
        }

        [HttpGet]   
        [Route("Supplier/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await Functions.APIService.Delete<IActionResult>(id, "Supplier");
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id != 0)
            {
                Models.Supplier supplier = await Functions.APIService.GetByID<Models.Supplier>(id, "Supplier");
                return View(supplier);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                Models.Supplier supplier = await Functions.APIService.GetByID<Models.Supplier>(id, "Supplier");
                return View(supplier);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,Name,Email,Phone,Address,AddressId")] Models.Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Functions.APIService.Edit<Models.Supplier>(supplier, supplier.SupplierId, "Supplier");
                    //ViewBag.JavaScriptFunctionn = string.Format("successfulEditAlert()");
                    TempData["categoryEdited"] = "Category edited";
                    //return RedirectToAction(nameof(List));
                }

                return Redirect("Edit?id=" + supplier.SupplierId);
            }
            catch (Exception e)
            {
                TempData["error"] = "Ha ocurrido un error al editar información, intente nuevamente";
                return Redirect("Edit?id=" + supplier.SupplierId);
                throw;
            }
        }
    }
}
