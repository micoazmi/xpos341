using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.Services;
using xpos341.viewmodels;

namespace xpos341.Controllers
{
    public class RoleController : Controller
    {
        private RoleService roleService;
        //private int IdUser = 1;
        //VMResponse response = new VMResponse();

        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString,
                                    string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "role_name" : "";

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
            List<VMTblRole> data = await roleService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.RoleName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "role_name":
                    data = data.OrderByDescending(a => a.RoleName).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.RoleName).ToList();
                    break;
            }

            return View(PaginatedList<VMTblRole>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

		public IActionResult Create()
		{
			VMTblRole data = new VMTblRole();
			return PartialView(data);
		}

        [HttpPost]
		public async Task<IActionResult> Create(VMTblRole dataParam)
		{
			int IdUser = HttpContext.Session.GetInt32("Id") ?? 0;
			dataParam.CreatedBy = IdUser;
			VMResponse response = await roleService.Create(dataParam);

			if (response.Success)
			{
				return Json(new { dataResponse = response });
			}

			return View(dataParam);
		}

		public async Task<JsonResult> CheckNameIsExist(string roleName, int id)
		{
			bool isExist = await roleService.CheckRole(roleName, id);
			return Json(isExist);
		}

		public async Task<IActionResult> Edit(int id)
		{
			VMTblRole data = await roleService.GetDataById(id);
			return PartialView(data);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(VMTblRole dataParam)
		{
			int IdUser = HttpContext.Session.GetInt32("Id") ?? 0;
			dataParam.UpdatedBy = IdUser;
			VMResponse response = await roleService.Edit(dataParam);

			if (response.Success)
			{
				return Json(new { dataResponse = response });
			}

			return View(dataParam);
		}

		public async Task<IActionResult> Detail(int id)
		{
			VMTblRole data = await roleService.GetDataById(id);
			return PartialView(data);
		}

		public async Task<IActionResult> Delete(int id)
		{
			VMTblRole data = await roleService.GetDataById(id);
			return PartialView(data);
		}

		[HttpPost]
		public async Task<IActionResult> SureDelete(int id)
		{
			int IdUser = HttpContext.Session.GetInt32("Id") ?? 0;
			int createBy = IdUser;
			VMResponse response = await roleService.Delete(id, createBy);

			if (response.Success)
			{
				return Json(new { dataResponse = response });
			}

			return RedirectToAction("Index");
		}

        //UNTUK ATUR MENU ACCESS
        public async Task<IActionResult> Index_MenuAccess(string sortOrder, string searchString,
                                    string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "role_name" : "";

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
            List<VMTblRole> data = await roleService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.RoleName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "role_name":
                    data = data.OrderByDescending(a => a.RoleName).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.RoleName).ToList();
                    break;
            }

            return View(PaginatedList<VMTblRole>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public async Task<IActionResult> Edit_MenuAccess(int id)
        {
            VMTblRole data = await roleService.GetDataById_MenuAccess(id);
            ViewBag.role_menu = data.role_menu;
            return PartialView(data);
        }

    }
}
