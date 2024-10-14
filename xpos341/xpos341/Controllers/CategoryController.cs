using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.Services;
using xpos341.viewmodels;

namespace xpos341.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        private int IdUser = 1;

        public CategoryController(CategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, 
                                    string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_order" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            // Get data from API
            List<TblCategory> data = await categoryService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameCategory.ToLower().Contains(searchString.ToLower())
                    || a.Description.ToLower().Contains(searchString.ToLower())).ToList();
			}

            switch (sortOrder)
            {
                case "name_order":
                    data = data.OrderByDescending(a => a.NameCategory).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.NameCategory).ToList();
                    break;
            }

            return View(PaginatedList<TblCategory>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            TblCategory data = new TblCategory();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblCategory dataParam)
        {
            dataParam.CreateBy = IdUser;
            VMResponse response = await categoryService.Create(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return View(dataParam);
        }

        public async Task<JsonResult> CheckNameIsExist(string nameCategory, int id)
        {
            bool isExist = await categoryService.CheckCategoryByName(nameCategory, id);
            return Json(isExist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory dataParam)
        {
            dataParam.UpdateBy = IdUser;
            VMResponse response = await categoryService.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return View(dataParam);
        }

		public async Task<IActionResult> Detail(int id)
		{
			TblCategory data = await categoryService.GetDataById(id);
			return PartialView(data);
		}

		public async Task<IActionResult> Delete(int id)
		{
			TblCategory data = await categoryService.GetDataById(id);
			return PartialView(data);
		}

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = IdUser;
            VMResponse response = await categoryService.Delete(id, createBy);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return RedirectToAction("Index");
        }

	}
}
