using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.Services;
using xpos341.viewmodels;

namespace xpos341.Controllers
{
    public class CategoryTryController : Controller
    {
        private readonly Xpos341Context db;
        private readonly CategoryTryService categoryTryService;

        public CategoryTryController(Xpos341Context _db)
        {
            db = _db;
            this.categoryTryService = new CategoryTryService(db);
        }

        public IActionResult Index()
        {
            List<VMTblCategory> dataView = categoryTryService.GetAllData();
            return View(dataView);
        }

        public IActionResult Create()
        {
            VMTblCategory dataView = new VMTblCategory();
            return PartialView(dataView);
        }

        [HttpPost]
        public IActionResult Create(VMTblCategory dataView)
        {
            VMResponse response = new VMResponse();

            if (ModelState.IsValid)
            {
				response = categoryTryService.Create(dataView);

                if(response.Success)
                {
					return RedirectToAction("Index");
				}
			}

            response.Entity = dataView;
            return View(response.Entity);
            
        }

        public IActionResult Edit(int id)
        {
            VMTblCategory dataView = categoryTryService.GetById(id);
            //ViewBag.databaru = dataView;
            return PartialView(dataView);
        }

        [HttpPost]
        public IActionResult Edit(VMTblCategory dataView)
        {
            VMResponse response = new VMResponse();

            if (ModelState.IsValid)
            {
				response = categoryTryService.Edit(dataView);

                if(response.Success)
                {
                    return RedirectToAction("Index"); 
                }
			}

            response.Entity = dataView;
            return View(response.Entity);
        }

        public IActionResult Detail(int id)
        {
            VMTblCategory dataView = categoryTryService.GetById(id);
            return PartialView(dataView);
        }

        public IActionResult Delete(int id)
        {
			VMTblCategory dataView = categoryTryService.GetById(id);
			return PartialView(dataView);
		}

        [HttpPost]
        public IActionResult Delete(VMTblCategory dataView)
        {
            // bisa seperti ini
			//VMResponse response = categoryTryService.SoftDelete(dataView);

            // bisa juga seperti ini
			VMResponse response = new VMResponse();

            response = categoryTryService.SoftDelete(dataView);
            if (response.Success)
            {
                return RedirectToAction("Index");
			}

            return View(response.Entity);
        }
    }
}
