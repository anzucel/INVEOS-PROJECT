using INVEOS.Models;
using Microsoft.AspNetCore.Mvc;

namespace INVEOS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly InveosContext _context;

        public CategoryController()
        {
            _context = new InveosContext();
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            try
            {
                IEnumerable<Models.Category> categories = await Functions.APIService.GetList<Models.Category>("Category");
                return View(categories);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View();
                //return RedirectToAction("Dashboard", "Graphics");
                throw;
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Description,Image")] INVEOS.Models.Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Functions.APIService.Set(category, "Category");
                    //TempData["categoryCreated"] = "Category created";
                    ViewBag.JavaScriptFunction = string.Format("successfulAlert()");
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
        [Route("Category/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await Functions.APIService.Delete<IActionResult>(id, "Category");
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                Models.Category category = await Functions.APIService.GetByID<Models.Category>(id, "Category");
                return View(category);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Code,Description,Image")] Models.Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Functions.APIService.Edit<Models.Category>(category, category.CategoryId, "Category");
                    //ViewBag.JavaScriptFunctionn = string.Format("successfulEditAlert()");
                    TempData["categoryEdited"] = "Category edited";
                    //return RedirectToAction(nameof(List));
                }

                return Redirect("Edit?id=" + category.CategoryId);
            }
            catch (Exception e)
            {
                TempData["error"] = "Ha ocurrido un error al editar la categoría, intente nuevamente";
                return Redirect("Edit?id=" + category.CategoryId);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id != 0)
            {
                Models.Category category = await Functions.APIService.GetByID<Models.Category>(id, "Category");
                return View(category);
            }
            return RedirectToAction(nameof(List));
        }
    }
}
