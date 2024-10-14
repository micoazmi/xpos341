using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class apiCustomerController : ControllerBase
	{
		private readonly Xpos341Context db;
		private VMResponse response = new VMResponse();
		private int IdUser = 1;

		public apiCustomerController(Xpos341Context db)
		{
			this.db = db;
		}

		[HttpGet("GetAllData")]
		public List<VMTblCustomer> GetAllData()
		{

			List<VMTblCustomer> data = (from c in db.TblCustomers
										join r in db.TblRoles on c.IdRole equals r.Id
										where c.IsDelete == false
										select new VMTblCustomer
										{
										Id = c.Id,
										NameCustomer = c.NameCustomer,
										Email = c.Email,
										//Password = c.Password,
										Address = c.Address,
										Phone = c.Phone,
										CreateBy = c.CreateBy,
										CreateDate = c.CreateDate,

										RoleName = r.RoleName,

										}).ToList();

			return data;
		}

		[HttpGet("GetDataById/{id}")]
		public VMTblCustomer GetDataById(int id)
		{
			VMTblCustomer data = (from c in db.TblCustomers
								 join r in db.TblRoles on c.IdRole equals r.Id
								 where c.IsDelete == false && c.Id == id
								 select new VMTblCustomer
								 {
									 Id = c.Id,
									 NameCustomer = c.NameCustomer,
									 Email = c.Email,
									 Password = c.Password,
									 Address = c.Address,
									 Phone = c.Phone,
									 IdRole = c.IdRole,
									 RoleName = r.RoleName,

								 }).FirstOrDefault()!;
			return data;
		}

		[HttpGet("CheckByEmail/{email}/{id}")]
		public bool CheckByEmail(string email, int id)
		{
			TblCustomer data = new TblCustomer();

			if (id == 0)
			{
				data = db.TblCustomers.Where(a => a.Email == email && a.IsDelete == false).FirstOrDefault()!;
				//data = db.TblVariants.FirstOrDefault(a => a.NameVariant == name && a.IsDelete == false
				//&& a.IdCategory == idCategory);
			}
			else
			{
				data = db.TblCustomers.Where(a => a.Email == email && a.IsDelete == false && a.Id != id).FirstOrDefault()!;
			}

			if (data != null)
			{
				return true;
			}

			return false;

		}

		[HttpPost("Save")]
		public VMResponse Save(TblCustomer data)
		{
			data.CreateBy = IdUser;
			data.CreateDate = DateTime.Now;
			data.IsDelete = false;

			try
			{
				db.Add(data);
				db.SaveChanges();

				response.Message = "Data success saved";
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Failed saved : " + ex.Message;
			}

			return response;

		}

		[HttpPut("Edit")]
		public VMResponse Edit(TblCustomer data)
		{

			TblCustomer dt = db.TblCustomers.FirstOrDefault(a => a.Id == data.Id)!;
			//TblVariant dt = db.TblVariants.Where(a => a.Id == data.Id).FirstOrDefault()!;

			if (dt != null)
			{
				dt.NameCustomer = data.NameCustomer;
				dt.Email = data.Email;
				dt.IdRole = data.IdRole;
				dt.Phone = data.Phone;
				dt.Address = data.Address;
				dt.UpdateBy = IdUser;
				dt.UpdateDate = DateTime.Now;

				try
				{
					db.Update(dt);
					db.SaveChanges();

					response.Message = "Data success saved";

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

		[HttpDelete("Delete/{id}")]
		public VMResponse Delete(int id)
		{
			TblCustomer dt = db.TblCustomers.Where(a => a.Id == id).FirstOrDefault()!;

			if (dt != null)
			{
				dt.IsDelete = true;
				dt.UpdateBy = IdUser;
				dt.UpdateDate = DateTime.Now;

				try
				{
					db.Update(dt);
					db.SaveChanges();

					response.Message = "Data success deleted";
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

		[HttpPut("MultipleDelete")]
		public VMResponse MultipleDelete(List<int> listId)
		{
			if (listId.Count > 0)
			{
				foreach (int item in listId)
				{
					TblCustomer dt = db.TblCustomers.Where(a => a.Id == item).FirstOrDefault()!;
					dt.IsDelete = true;
					dt.UpdateBy = IdUser;
					dt.UpdateDate = DateTime.Now;
					db.Update(dt);
				}

				try
				{
					db.SaveChanges();

					response.Message = "Data success deleted";
				}
				catch (Exception ex)
				{
					response.Success = false;
					response.Message = "Failed deleted" + ex.Message;
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
