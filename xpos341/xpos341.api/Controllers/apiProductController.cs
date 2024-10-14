using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class apiProductController : ControllerBase
	{

		private readonly Xpos341Context db;
		private VMResponse response = new VMResponse();
		private int IdUser = 1;

		public apiProductController(Xpos341Context db)
		{
			this.db = db;
		}

		[HttpGet("GetAllData")]
		public List<VMTblProduct> GetAllData()
		{
			List<VMTblProduct> data = (from p in db.TblProducts
									   join v in db.TblVariants on p.IdVariant equals v.Id
									   join c in db.TblCategories on v.IdCategory equals c.Id
									   where p.IsDelete == false
									   select new VMTblProduct
									   {
										   Id = p.Id,
										   NameProduct = p.NameProduct,
										   Price = p.Price,
										   Stock = p.Stock,
										   Image = p.Image,

										   IdVariant = p.IdVariant,
										   NameVariant = v.NameVariant,

										   IdCategory = v.IdCategory,
										   NameCategory = c.NameCategory,

										   CreateDate = p.CreateDate
									   }).ToList();

			return data;
		}

		//LEFT JOIN
		[HttpGet("GetAllData_LeftJoin")]
		public List<VMTblProduct> GetAllData_LeftJoin()
		{
			List<VMTblProduct> data = (from p in db.TblProducts
									   join v in db.TblVariants on p.IdVariant equals v.Id
									   join c in db.TblCategories on v.IdCategory equals c.Id into tc from tcategory in tc.DefaultIfEmpty()
									   where p.IsDelete == false && tcategory == null
									   select new VMTblProduct
									   {
										   Id = p.Id,
										   NameProduct = p.NameProduct,
										   Price = p.Price,
										   Stock = p.Stock,
										   Image = p.Image,

										   IdVariant = p.IdVariant,
										   NameVariant = v.NameVariant,

										   IdCategory = v.IdCategory,
										   NameCategory = tcategory.NameCategory ?? "", // INI WALAU NULL TETAP MUNCUL JIKA WHERE DIATAS TIDAK DI VALIDASI

										   CreateDate = p.CreateDate
									   }).ToList();

			return data;
		}

		[HttpGet("GetDataById/{id}")]
		public VMTblProduct GetDataById(int id)
		{
			VMTblProduct data = (from p in db.TblProducts
									   join v in db.TblVariants on p.IdVariant equals v.Id
									   join c in db.TblCategories on v.IdCategory equals c.Id
									   where p.IsDelete == false && p.Id == id
									   select new VMTblProduct
									   {
										   Id = p.Id,
										   NameProduct = p.NameProduct,
										   Price = p.Price,
										   Stock = p.Stock,
										   Image = p.Image,

										   IdVariant = p.IdVariant,
										   NameVariant = v.NameVariant,

										   IdCategory = v.IdCategory,
										   NameCategory = c.NameCategory,

										   CreateDate = p.CreateDate
									   }).FirstOrDefault()!;

			return data;
		}

		[HttpGet("CheckByName/{name}/{id}/{idVariant}")]
		public bool CheckName(string name, int id, int idVariant)
		{
			TblProduct data = new TblProduct();

			if (id == 0)
			{
				data = db.TblProducts.Where(a => a.NameProduct == name && a.IsDelete == false
						&& a.IdVariant == idVariant).FirstOrDefault()!;
				//data = db.TblVariants.FirstOrDefault(a => a.NameVariant == name && a.IsDelete == false
				//&& a.IdCategory == idCategory);
			}
			else
			{
				data = db.TblProducts.Where(a => a.NameProduct == name && a.IsDelete == false
						&& a.IdVariant == idVariant && a.Id != id).FirstOrDefault()!;
			}

			if (data != null)
			{
				return true;
			}

			return false;
		}

		[HttpPost("Save")]
		public VMResponse Save(TblProduct data)
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
		public VMResponse Edit(TblProduct data)
		{

			TblProduct dt = db.TblProducts.FirstOrDefault(a => a.Id == data.Id)!;
			//TblVariant dt = db.TblVariants.Where(a => a.Id == data.Id).FirstOrDefault()!;

			if (dt != null)
			{
				dt.NameProduct = data.NameProduct;
				dt.IdVariant = data.IdVariant;
				dt.Price = data.Price;
				dt.Stock = data.Stock;
				if(data.Image != null)
				{
					dt.Image = data.Image;
				}
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
			TblProduct dt = db.TblProducts.Where(a => a.Id == id).FirstOrDefault()!;

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



	}
}
