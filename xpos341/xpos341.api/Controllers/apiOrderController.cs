using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class apiOrderController : ControllerBase
	{
		private readonly Xpos341Context db;
		private VMResponse response = new VMResponse();
		private int IdUser = 1;

		public apiOrderController(Xpos341Context db)
		{
			this.db = db;
		}

		[HttpGet("GetAllDataOrderHeader")]
		public List<TblOrderHeader> GetAllDataHeader()
		{
			List<TblOrderHeader> data = db.TblOrderHeaders.Where(a => a.IsDelete == false).ToList();
			return data;
		}

		[HttpGet("GetDataOrderDetail/{id}")]
		public List<VMOrderDetail> GetDataDetail(int id)
		{
			List<VMOrderDetail> data = (from d in db.TblOrderDetails
										join p in db.TblProducts on d.IdProduct equals p.Id
										where d.IsDelete == false && d.IdHeader == id
										select new VMOrderDetail
										{
											Id = d.Id,
											Qty = d.Qty,
											SumPrice = d.SumPrice,

											IdProduct = d.IdProduct,
											NameProduct = p.NameProduct,
											Price = p.Price,
											Stock = p.Stock,

										}).ToList();

			return data;
		}

		[HttpPost("SubmitOrder")]
		public VMResponse SubmitOrder(VMOrderHeader dataHeader)
		{
			TblOrderHeader head = new TblOrderHeader();
			head.CodeTransaction = GenerateCode();
			head.Amount = dataHeader.Amount;
			head.TotalQty = dataHeader.TotalQty;
			head.IdCustomer = IdUser;
			head.IsCheckout = true;
			head.IsDelete = false;
			head.CreateBy = IdUser;
			head.CreateDate = DateTime.Now;

			try
			{
				db.Add(head);
				db.SaveChanges();

				foreach (VMOrderDetail item in dataHeader.ListDetails)
				{
					TblOrderDetail detail = new TblOrderDetail();
					detail.IdHeader = head.Id;
					detail.IdProduct = item.IdProduct;
					detail.Qty = item.Qty;
					detail.SumPrice = item.SumPrice;
					detail.IsDelete = false;
					detail.CreateBy = IdUser;
					detail.CreateDate = DateTime.Now;

					try
					{
						db.Add(detail);
						db.SaveChanges();

						TblProduct dataProduct = db.TblProducts.Where(a => a.Id == item.IdProduct).FirstOrDefault()!;

						if (dataProduct != null)
						{
							dataProduct.Stock = dataProduct.Stock - item.Qty;

							db.Update(dataProduct);
							db.SaveChanges();
						}

						response.Message = "Thanks for order";
					}
					catch (Exception ex)
					{
						response.Success = false;
						response.Message = "Failed saved : " + ex.Message;
					}
				}



			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Failed saved : " + ex.Message;
			}

			return response;
		}

		[HttpGet("GenerateCode")]
		public string GenerateCode()
		{
			string code = $"XPOS-{DateTime.Now.ToString("yyyyMMdd")}-";
			string digit = "";

			TblOrderHeader data = db.TblOrderHeaders.OrderByDescending(a => a.CodeTransaction).FirstOrDefault()!;

			if (data != null)
			{
				string codeLast = data.CodeTransaction;
				string[] codeSplit = codeLast.Split("-");
				int intLast = int.Parse(codeSplit[2]) + 1;
				digit = intLast.ToString().PadLeft(5, '0');
				//digit = intLast.ToString("00000");
			}
			else
			{
				digit = "00001";
			}

			return code + digit;
		}

		[HttpGet("CountTransaction/{IdCustomer}")]
		public int CountTransaction(int IdCustomer)
		{
			int count = 0;
			count = db.TblOrderHeaders.Where(a => a.IsDelete == false && a.IdCustomer == IdCustomer).Count();
			return count;
		}

		[HttpGet("GetDataOrderHeaderDetail/{IdCustomer}")]
		public List<VMOrderHeader> GetDataOrderHeaderDetail(int IdCustomer)
		{
			List<VMOrderHeader> data = (from h in db.TblOrderHeaders
										where h.IsDelete == false && h.IdCustomer == IdCustomer
										select new VMOrderHeader
										{
											Id = h.Id,
											Amount = h.Amount,
											TotalQty = h.TotalQty,
											IsCheckout = h.IsCheckout,
											IdCustomer = h.IdCustomer,
											CodeTransaction = h.CodeTransaction,
											CreateDate = h.CreateDate,
											ListDetails = (from d in db.TblOrderDetails
															join p in db.TblProducts on d.IdProduct equals p.Id
															where d.IsDelete == false && d.IdHeader == h.Id
															select new VMOrderDetail
															{
																Id = d.Id,
																Qty = d.Qty,
																SumPrice = d.SumPrice,
																IdHeader = d.IdHeader,
																IdProduct = d.IdProduct,
																NameProduct = p.NameProduct,
																Price = p.Price,
																Stock = p.Stock,
															}).ToList()
										}).ToList();

			return data;
		}



	}
}
