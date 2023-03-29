using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INVEOS.Controllers
{
    public class GraphicsController : Controller
    {
        // GET: GraphicsController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Route("graphics/dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: GraphicsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GraphicsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GraphicsController/Create
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

        // GET: GraphicsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GraphicsController/Edit/5
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

        // GET: GraphicsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GraphicsController/Delete/5
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
