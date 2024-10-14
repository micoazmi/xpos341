using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiCategoryController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int IdUser = 1;

        public apiCategoryController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("GetAllData")]
        public List<TblCategory> GettAll()
        {

            List<TblCategory> data = db.TblCategories.Where(x => x.IsDelete == false).ToList();

            return data;
            //return Ok(data);
        }

        [HttpGet("GetDataById/{id}")]
        public TblCategory GetDataById(int id)
        {
            //TblCategory data = db.TblCategories.Where(x => x.Id == id).FirstOrDefault();
            //TblCategory data = db.TblCategories.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            TblCategory data = db.TblCategories.FirstOrDefault(x => x.Id == id);

            return data;
        }

        [HttpGet("CheckCategoryByName/{name}/{id}")]
        public bool CheckName(string name, int id)
        {
            TblCategory data = new TblCategory();
            if(id == 0) // create
            {
                data = db.TblCategories.Where(a => a.NameCategory == name && a.IsDelete == false).FirstOrDefault()!;
            }
            else // edit
            {
                data = db.TblCategories.Where(a => a.NameCategory == name && a.IsDelete == false && a.Id != id).FirstOrDefault()!;
            }

            if(data != null)
            {
                return true;
            }

            return false;
        }

        [HttpPost("Save")]
        public VMResponse Save(TblCategory data)
        {
            data.CreateBy = IdUser;
            data.CreateDate = DateTime.Now;
            data.IsDelete = false;
            //data.Description = data.Description ?? "";
            data.Description = data.Description ?? "Desc of " + data.NameCategory;

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
        public VMResponse Edit(TblCategory data)
        {
            TblCategory dt = db.TblCategories.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if(dt != null)
            {
                dt.NameCategory = data.NameCategory;
                dt.Description = data.Description;
                dt.UpdateBy = IdUser;
                dt.UpdateDate = DateTime.Now;

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
            TblCategory dt = db.TblCategories.Where(a => a.Id == id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.UpdateBy = createBy;
                dt.UpdateDate = DateTime.Now;

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


    }
}
