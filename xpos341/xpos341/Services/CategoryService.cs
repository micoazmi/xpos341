using Newtonsoft.Json;
using System.Text;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.Services
{
	public class CategoryService
	{
		private static readonly HttpClient client = new HttpClient();
		private IConfiguration configuration;
		private string RouteAPI = "";
		private VMResponse response = new VMResponse();

        public CategoryService(IConfiguration _configuration)
        {
            configuration = _configuration;
			RouteAPI = configuration["RouteAPI"]!;
        }

		public async Task<List<TblCategory>> GetAllData()
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + "apiCategory/GetAllData");
			List<TblCategory> data = JsonConvert.DeserializeObject<List<TblCategory>>(apiResponse)!;

			return data;
		}

		public async Task<VMResponse> Create(TblCategory dataParam)
		{
			// proses convert dari object ke string
			string json = JsonConvert.SerializeObject(dataParam);

			// proses mengubah string json lalu dikirim sebagai request body
			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			// proses memanggil API dan mengirimkan body
			var request = await client.PostAsync(RouteAPI + "apiCategory/Save", content);

			if(request.IsSuccessStatusCode)
			{
				// proses membaca response dari API
				var apiResponse = await request.Content.ReadAsStringAsync();

				// proses convert hasil response dari API ke object
				response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
			}
			else
			{
				response.Success = false;
				response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
			}

			return response;
		}

		public async Task<bool> CheckCategoryByName(string nameCategory, int id)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCategory/CheckCategoryByName/{nameCategory}/{id}");
			bool data = JsonConvert.DeserializeObject<bool>(apiResponse);

			return data;
		}

		public async Task<TblCategory> GetDataById( int id )
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCategory/GetDataById/{id}");
			TblCategory data = JsonConvert.DeserializeObject<TblCategory>(apiResponse)!;

			return data;
		}

		public async Task<VMResponse> Edit(TblCategory dataParam)
		{
			// proses convert dari object ke string
			string json = JsonConvert.SerializeObject(dataParam);

			// proses mengubah string json lalu dikirim sebagai request body
			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			// proses memanggil API dan mengirimkan body
			var request = await client.PutAsync(RouteAPI + "apiCategory/Edit", content);

			if (request.IsSuccessStatusCode)
			{
				// proses membaca response dari API
				var apiResponse = await request.Content.ReadAsStringAsync();

				// proses convert hasil response dari API ke object
				response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
			}
			else
			{
				response.Success = false;
				response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
			}

			return response;
		}

		public async Task<VMResponse> Delete(int id, int createdBy)
		{
			var request = await client.DeleteAsync(RouteAPI + $"apiCategory/Delete/{id}/{createdBy}");

			if (request.IsSuccessStatusCode)
			{
				//proses membaca response dari API
				var apiResponse = await request.Content.ReadAsStringAsync();

				// proses convert hasil response dari API ke object
				response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
			}
			else
			{
				response.Success = false;
				response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
			}

			return response;
		}


	}
}
