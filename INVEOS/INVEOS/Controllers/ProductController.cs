using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INVEOS.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("product/list")]
        public async Task<ActionResult> List()
        {
            //get all products
            IEnumerable<Models.Product> products = await Functions.APIService.GetList<Models.Product>("Product");
            return View();
        }


        [HttpGet]
        [Route("product/add")]
        public async Task<ActionResult> Add()
        {
            //get all products
            IEnumerable<Models.Category> categories = await Functions.APIService.GetList<Models.Category>("Category");

            ViewBag.Category = categories.Select(c => new SelectListItem()
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IDProduct,CategoryID,Name,Code,Cost,Price,Quantity,Description,Status,SuplierID,Image")] INVEOS.Models.Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Functions.APIService.Set(product, "Product");
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

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
