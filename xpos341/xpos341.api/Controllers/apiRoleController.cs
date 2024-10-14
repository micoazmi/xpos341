using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.api.Services;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiRoleController : ControllerBase
    {

        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private RolesService rolesService;
        private int IdUser = 1;

        public apiRoleController(Xpos341Context _db)
        {
            db = _db;
            rolesService = new RolesService(db);
        }

        [HttpGet("GetAllRole")]
        public List<VMTblRole> GetAllRole()
        {
            List<VMTblRole> data = (from r in db.TblRoles
                                    //join c in db.TblCustomers on r.Id equals c.IdRole
                                    where r.IsDelete == false
                                    select new VMTblRole
                                    {
                                        Id = r.Id,
                                        RoleName = r.RoleName,
                                        CreatedBy = r.CreatedBy,
                                        CreatedDate = r.CreatedDate,

										UpdatedBy = r.UpdatedBy,
										UpdatedDate = r.UpdatedDate,

									}).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMTblRole GetDataById(int id)
        {
            VMTblRole data = (from r in db.TblRoles
                                //join c in db.TblCustomers on r.Id equals c.IdRole
                                where r.IsDelete == false && r.Id == id
							    select new VMTblRole
                                {
                                    Id = r.Id,
                                    RoleName = r.RoleName,
                                    CreatedBy = r.CreatedBy,
                                    CreatedDate = r.CreatedDate,

                                    UpdatedBy = r.UpdatedBy,
                                    UpdatedDate = r.UpdatedDate,

                                }).FirstOrDefault()!;
            return data;
        }

        [HttpGet("CheckRole/{roleName}/{id}")]
        public bool CheckRole(string roleName, int id)
        {
            TblRole data = new TblRole();

            if (id == 0)
            {
                data = db.TblRoles.Where(a => a.RoleName == roleName && a.IsDelete == false).FirstOrDefault()!;
                //data = db.TblVariants.FirstOrDefault(a => a.NameVariant == name && a.IsDelete == false
                //&& a.IdCategory == idCategory);
            }
            else
            {
                data = db.TblRoles.Where(a => a.RoleName == roleName && a.IsDelete == false
                        && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)
            {
                return true;
            }

            return false;
        }

        [HttpPost("CreateRole")]
        public VMResponse CreateRole(TblRole data)
        {
            data.CreatedBy = IdUser;
            data.CreatedDate = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();

                response.Message = "Data Success saved";
                //response.Entity = data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Save Failed" + ex.Message;
            }

            return response;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(TblRole data)
        {
            TblRole dt = db.TblRoles.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.RoleName = data.RoleName;
                dt.UpdatedBy = IdUser;
                dt.UpdatedDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Data Success Saved";
                    //response.Entity = data;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Failed saved : " + ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Data not found";
            }

            return response;

        }

        [HttpDelete("Delete/{id}/{createBy}")]
        public VMResponse Delete(int id, int createBy)
        {
            TblRole dt = db.TblRoles.Where(a => a.Id == id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.UpdatedBy = createBy;
                dt.UpdatedDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Data Success deleted";
                    //response.Entity = data;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Failed deleted : " + ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Data not found";
            }

            return response;
        }

        //UNTUK ATUR MENU ACCESS
        [HttpGet("GetDataById_MenuAccess/{id}")]
        public async Task<VMTblRole> DataById_MenuAccess(int id)
        {

            VMTblRole result = db.TblRoles.Where(a => a.Id == id)
                                .Select(a => new VMTblRole()
                                {
                                    Id = a.Id,
                                    RoleName = a.RoleName,
                                })
                                .FirstOrDefault()!;

            result.role_menu = await rolesService.GetMenuAccessParentChildByRoleID(result.Id, 0, false);
            return result;

        }


        //INI CARA : UPDATE ISDELETE TRUE (ALL) & INSERT (ALL)
        [HttpPut("Edit_MenuAccess")]
        public VMResponse Edit_MenuAccess(VMTblRole data)
        {
            TblRole dt = db.TblRoles.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.RoleName = data.RoleName;
                dt.UpdatedBy = IdUser;
                dt.UpdatedDate = DateTime.Now;

                try
                {
                    db.Update(dt);

                    // SAVE MenuAccess
                    if(data.role_menu.Count > 0)
                    {
                        //Remove MenuAccess
                        List<TblMenuAccess> ListMenuAccessRemove = db.TblMenuAccesses.Where(a => a.RoleId == data.Id).ToList();
                        if (ListMenuAccessRemove.Count > 0)
                        {
                            foreach (TblMenuAccess item in ListMenuAccessRemove)
                            {
                                item.IsDelete = true;
                                item.UpdatedBy = IdUser;
                                item.UpdatedDate = DateTime.Now;
                                db.Update(item);
                            }
                        }

                        //Insert MenuAccess
                        List<TblMenuAccess> listMenuAccessAdd = data.role_menu.Where(a => a.is_selected = true)
                                                                .Select(a => new TblMenuAccess()
                                                                {
                                                                    RoleId = data.Id,
                                                                    MenuId = a.IdMenu,
                                                                    IsDelete = false,
                                                                    CreatedBy = IdUser,
                                                                    CreatedDate = DateTime.Now
                                                                }).ToList();
                        foreach (TblMenuAccess item in listMenuAccessAdd)
                        {
                            db.Add(item);
                        }

                    }

                    db.SaveChanges();
                    response.Message = "Data success saved";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Failed saved" + ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Data not found";
            }

            return response;

        }


    }
}
