using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.Services;
using xpos341.viewmodels;

namespace xpos341.Controllers
{
	public class CustomerController : Controller
	{
		private CustomerService customerService;
		private RoleService roleService;
		private int IdUser = 1;
		//VMResponse response = new VMResponse();

		public CustomerController(CustomerService customerService, RoleService roleService)
		{
			this.customerService = customerService;
			this.roleService = roleService;
		}


		public async Task<IActionResult> Index(string sortOrder, string searchString,
									string currentFilter, int? pageNumber, int? pageSize)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.CurrentPageSize = pageSize;
			ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_customer" : "";

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
			List<VMTblCustomer> data = await customerService.GetAllData();

			if (!string.IsNullOrEmpty(searchString))
			{
				data = data.Where(a => a.NameCustomer.ToLower().Contains(searchString.ToLower())).ToList();
			}

			switch (sortOrder)
			{
				case "name_customer":
					data = data.OrderByDescending(a => a.NameCustomer).ToList();
					break;
				default:
					data = data.OrderBy(a => a.NameCustomer).ToList();
					break;
			}

			return View(PaginatedList<VMTblCustomer>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
		}

        public async Task<IActionResult> Create()
        {
            VMTblCustomer data = new VMTblCustomer();
			List<VMTblRole> listRole = await roleService.GetAllData();

			ViewBag.ListRole = listRole;
			return PartialView(data);
        }

		public async Task<JsonResult> CheckEmailIsExist(string email, int id)
		{
			bool isExist = await customerService.CheckEmail(email, id);
			return Json(isExist);
		}

		[HttpPost]
		public async Task<IActionResult> Create(VMTblCustomer dataParam)
		{
			int IdUser = HttpContext.Session.GetInt32("Id") ?? 0;
			dataParam.CreateBy = IdUser;
			//dataParam.CreateBy = 1;
			VMResponse response = await customerService.Create(dataParam);

			if (response.Success)
			{
				return Json(new { dataResponse = response });
			}

			return View(dataParam);
		}

		public async Task<IActionResult> Edit(int id)
		{
			VMTblCustomer data = await customerService.GetDataById(id);
			List<VMTblRole> listRole = await roleService.GetAllData();

			ViewBag.ListRole = listRole;
			return PartialView(data);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(VMTblCustomer dataParam)
		{
			int IdUser = HttpContext.Session.GetInt32("Id") ?? 0;
			dataParam.UpdateBy = IdUser;
			VMResponse response = await customerService.Edit(dataParam);

			if (response.Success)
			{
				return Json(new { dataResponse = response });
			}

			return View(dataParam);
		}

		public async Task<IActionResult> Detail(int id)
		{
			VMTblCustomer data = await customerService.GetDataById(id);

			return PartialView(data);
		}

		public async Task<IActionResult> Delete(int id)
		{
			VMTblCustomer data = await customerService.GetDataById(id);

			return PartialView(data);
		}

		[HttpPost]
		public async Task<IActionResult> SureDelete(int id)
		{
			int IdUser = HttpContext.Session.GetInt32("Id") ?? 0;
			int createBy = IdUser;
			VMResponse response = await customerService.Delete(id);

			if (response.Success)
			{
				return Json(new { dataResponse = response });
			}

			return RedirectToAction("Index");
		}

	}
}
