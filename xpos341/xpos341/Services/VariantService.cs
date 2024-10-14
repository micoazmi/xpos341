using Newtonsoft.Json;
using System.Text;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.Services
{
	public class VariantService
	{
		private static readonly HttpClient client = new HttpClient();
		private IConfiguration configuration;
		private string RouteAPI = "";
		private VMResponse response = new VMResponse();

        public VariantService(IConfiguration _configuration)
        {
			configuration = _configuration;
			RouteAPI = configuration["RouteAPI"]!;
        }

		public async Task<List<VMTblVariant>> GetAllData()
		{
			string apiResponse = await client.GetStringAsync(RouteAPI + "apiVariant/GetAllData");
			List<VMTblVariant> data = JsonConvert.DeserializeObject<List<VMTblVariant>>(apiResponse)!;

			return data;
		}

		public async Task<bool> CheckByName(string name, int id, int idCategory)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/CheckByName/{name}/{id}/{idCategory}");
			bool data = JsonConvert.DeserializeObject<bool>(apiResponse);

			return data;
		}

		public async Task<VMResponse> Create(VMTblVariant dataParam)
		{
			string json = JsonConvert.SerializeObject(dataParam);

			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			var request = await client.PostAsync(RouteAPI + "apiVariant/Save", content);

			if (request.IsSuccessStatusCode)
			{
				var apiResponse = await request.Content.ReadAsStringAsync();

				response = JsonConvert.DeserializeObject<VMResponse>(apiResponse);
			}
			else
			{
				response.Success = false;
				response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
			}

			return response;
		}

		public async Task<VMTblVariant> GetDataById(int id)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/GetDataById/{id}");
			VMTblVariant data = JsonConvert.DeserializeObject<VMTblVariant>(apiResponse)!;

			return data;
		}

		public async Task<VMResponse> Edit(VMTblVariant dataParam)
		{
			string json = JsonConvert.SerializeObject(dataParam);

			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			var request = await client.PutAsync(RouteAPI + "apiVariant/Edit", content);

			if (request.IsSuccessStatusCode)
			{
				var apiResponse = await request.Content.ReadAsStringAsync();

				response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
			}
			else
			{
				response.Success = false;
				response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
			}

			return response;
		}

		public async Task<VMResponse> Delete(int id)
		{
			var request = await client.DeleteAsync(RouteAPI + $"apiVariant/Delete/{id}");

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

		public async Task<VMResponse> MultipleDelete(List<int> listId)
		{
			string json = JsonConvert.SerializeObject(listId);

			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			var request = await client.PutAsync(RouteAPI + "apiVariant/MultipleDelete", content);

			if (request.IsSuccessStatusCode)
			{
				var apiResponse = await request.Content.ReadAsStringAsync();

				response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
			}
			else
			{
				response.Success = false;
				response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
			}

			return response;
		}

		public async Task<List<VMTblVariant>> GetDataByIdCategory(int id)
		{
			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/GetDataByIdCategory/{id}");
			List<VMTblVariant> data = JsonConvert.DeserializeObject<List<VMTblVariant>>(apiResponse)!;

			return data;
		}
        // http//piVariant/GetDataByIdCategory/id?=2&name=
    }
}
