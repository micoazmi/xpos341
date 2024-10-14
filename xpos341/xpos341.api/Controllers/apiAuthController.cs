using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiAuthController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int IdUser = 1;

        public apiAuthController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("CheckLogin/{email}/{password}")]
        public VMTblCustomer CheckLogin(string email, string password)
        {
            VMTblCustomer data = (from c in db.TblCustomers
                                  join r in db.TblRoles on c.IdRole equals r.Id
                                  where c.IsDelete == false && c.Email == email && c.Password == password
                                  select new VMTblCustomer
                                  {
                                      Id = c.Id,
                                      NameCustomer = c.NameCustomer,
                                      Email = email,
                                      IdRole = c.IdRole,
                                      RoleName = r.RoleName,

                                  }).FirstOrDefault()!;

            return data;
        }



    }
}
