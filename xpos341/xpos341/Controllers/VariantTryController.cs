using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.Services;
using xpos341.viewmodels;

namespace xpos341.Controllers
{
    public class VariantTryController : Controller
    {
        private readonly Xpos341Context db;
        private readonly CategoryTryService categoryTryService;
        private readonly VariantTryService variantTryService;

        private static VMPage page = new VMPage();

        public VariantTryController(Xpos341Context _db)
        {
            db = _db;
            categoryTryService = new CategoryTryService(db);
            variantTryService = new VariantTryService(db);
        }

        public IActionResult Index(VMPage pg)
        {
            ViewBag.idSort = string.IsNullOrEmpty(pg.sortOrder) ? "id_desc" : "";
            ViewBag.nameSort = pg.sortOrder == "name" ? "name_Desc" : "name";
            ViewBag.currentSort = pg.sortOrder;
            ViewBag.currentShowData = pg.showData;

            if(pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.currentFilter;
                //pg.currentFilter = pg.searchString;
            }

            ViewBag.currentFilter = pg.currentFilter;
            //ViewBag.searchString = pg.searchString;

            List<VMTblVariant> dataView = variantTryService.GetAllData();

            if (!string.IsNullOrEmpty(pg.searchString))
            {
                dataView = dataView.Where(a => a.NameVariant.ToLower().Contains(pg.searchString.ToLower())
                    || a.NameCategory.ToLower().Contains(pg.searchString.ToLower())
				).ToList();
            }

            switch (pg.sortOrder)
            {
                case "name_desc":
                    dataView = dataView.OrderByDescending(a => a.NameVariant).ToList();
                    break;
				case "name":
					dataView = dataView.OrderBy(a => a.NameVariant).ToList();
					break;
				case "id_desc":
					dataView = dataView.OrderByDescending(a => a.Id).ToList();
					break;
                default:
					dataView = dataView.OrderBy(a => a.Id).ToList();
					break;
			}

            int pageSize = pg.showData ?? 3;

            page = pg;

            return View(PaginatedList<VMTblVariant>.CreateAsync(dataView, pg.pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            VMTblVariant dataView = new VMTblVariant();
            ViewBag.DropdownCategory = categoryTryService.GetAllData();
            return PartialView(dataView);
        }

        [HttpPost]
        public IActionResult Create(VMTblVariant dataView)
        {
            VMResponse response = new VMResponse();

            response = variantTryService.Create(dataView);

            if(response.Success)
            {
                return RedirectToAction("Index", page);
            }

            ViewBag.DropdownCategory = categoryTryService.GetAllData();
            response.Entity = dataView;

            return View(response.Entity);
        }

        public IActionResult Edit(int id)
        {
            VMTblVariant dataView = variantTryService.GetById(id);
            ViewBag.DropDownCategory = categoryTryService.GetAllData();

            return PartialView(dataView);
        }

        [HttpPost]
        public IActionResult Edit(VMTblVariant dataView)
        {
			VMResponse response = new VMResponse();

			response = variantTryService.Edit(dataView);

			if (response.Success)
			{
				return RedirectToAction("Index", page);
			}

			ViewBag.DropdownCategory = categoryTryService.GetAllData();
			response.Entity = dataView;

			return View(response.Entity);
		}

		public IActionResult Detail(int id)
		{
			VMTblVariant dataView = variantTryService.GetById(id);
			ViewBag.DropDownCategory = categoryTryService.GetAllData();

			return PartialView(dataView);
		}

		public IActionResult Delete(int id)
		{
			VMTblVariant dataView = variantTryService.GetById(id);
			return PartialView(dataView);
		}

        [HttpPost]
        public IActionResult Delete(VMTblVariant dataView)
        {
            VMResponse response = variantTryService.Delete(dataView);

            if(response.Success)
            {
                return RedirectToAction("Index", page);
            }
			response.Entity = dataView;
			return View(response.Entity);
		}
	}

}
