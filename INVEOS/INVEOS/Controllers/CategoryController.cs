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
            IEnumerable<Models.Category> categories = await Functions.APIService.GetList<Models.Category>("Category");
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Description,Image")] INVEOS.Models.Category category)
        {
            if (ModelState.IsValid)
            {
                await Functions.APIService.Set(category, "Category");
            }

            return RedirectToAction(nameof(List));
        }

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
            if (ModelState.IsValid)
            {
                await Functions.APIService.Edit<Models.Category>(category, category.CategoryId, "Category");
                return RedirectToAction(nameof(List));
            }
            return View(category);
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
