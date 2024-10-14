using AutoMapper;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.Services
{
	public class CategoryTryService
	{

		private readonly Xpos341Context db;
        VMResponse response = new VMResponse();
        int IdUser = 1;

        public CategoryTryService(Xpos341Context _db)
        {
            db = _db;
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblCategory, VMTblCategory>();
                cfg.CreateMap<VMTblCategory, TblCategory>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }

        public List<VMTblCategory> GetAllData()
        {
            List<TblCategory> dataModel = db.TblCategories.Where(a => a.IsDelete == false).ToList();

            List<VMTblCategory> dataView = GetMapper().Map<List<VMTblCategory>>(dataModel);

            return dataView;
        }

        public VMResponse Create(VMTblCategory dataView)
        {
            TblCategory dataModel = GetMapper().Map<TblCategory>(dataView);
            dataModel.IsDelete = false;
            dataModel.CreateBy = IdUser;
            dataModel.CreateDate = DateTime.Now;

            try
            {
                db.Add(dataModel);
                db.SaveChanges();

                response.Message = "Data success saved";
                response.Entity = dataModel;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed saved : " + ex.Message;
                response.Entity = dataView;
            }

            return response;

        }

        public VMTblCategory GetById(int id)
        {
            //TblCategory dataModel = db.TblCategories.Where(a => a.Id == id).FirstOrDefault();
            TblCategory dataModel = db.TblCategories.Find(id);
            VMTblCategory dataView = GetMapper().Map<VMTblCategory>(dataModel);
            return dataView;
        }

        public VMResponse Edit(VMTblCategory dataView)
        {
            TblCategory dataModel = db.TblCategories.Find(dataView.Id);
            dataModel.NameCategory = dataView.NameCategory;
            dataModel.Description = dataView.Description;
            dataModel.UpdateBy = IdUser;
            dataModel.UpdateDate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();

                response.Message = "Data success saved";
                response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed saved : " + ex.Message;
				response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
			}
            return response;
        }

        public VMResponse SoftDelete(VMTblCategory dataView)
        {
            //TblCategory dataModel = db.TblCategories.Find(dataView.Id);
            TblCategory dataModel = db.TblCategories.FirstOrDefault(a => a.Id == dataView.Id);
            dataModel.IsDelete = true;
            dataModel.UpdateBy = IdUser;
			dataModel.UpdateDate = DateTime.Now;

			try
            {
                db.Update(dataModel);
                db.SaveChanges();

                response.Message = "Data success deleted";
				response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
			}
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed deleted : " + ex.Message;
				response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
			}
            return response;

        }

    }
}
