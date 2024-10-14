using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using xpos341.Services;
using xpos341.viewmodels;

namespace xpos341.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        //private RoleService roleService;
        VMResponse response = new VMResponse();

        public AuthController(AuthService _authService)
        {
            authService = _authService;
            //roleService = _roleService;
        }

        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> LoginSubmit(string email, string password)
        {
            VMTblCustomer customer = await authService.CheckLogin(email, password);

            if (customer != null)
            {
                response.Message = $"Hello, { customer.NameCustomer } Welcome to XPOS";
                HttpContext.Session.SetInt32("IdCustomer", customer.Id);
                HttpContext.Session.SetString("NameCustomer", customer.NameCustomer);
                HttpContext.Session.SetInt32("IdRole", customer.IdRole ?? 0);
            }
            else
            {
                response.Success = false;
                response.Message = $"Oops, {email} not found or password is wrong, please check it";
            }

            return Json(new { dataResponse = response });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
