using Newtonsoft.Json;
using System.Text;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.Services
{
    public class RoleService
    {
        private static readonly HttpClient client = new HttpClient(); //GET API
        private IConfiguration configuration; // GET VARIABEL DI APPSETTING
        private string RouteAPI = "";
        private VMResponse response = new VMResponse();

        public RoleService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"]!;
        }

        public async Task<List<VMTblRole>> GetAllData()
        {

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiRole/GetAllRole");
            List<VMTblRole> data = JsonConvert.DeserializeObject<List<VMTblRole>>(apiResponse)!;

            return data;
        }

		public async Task<VMResponse> Create(VMTblRole dataParam)
		{
			// proses convert dari object ke string
			string json = JsonConvert.SerializeObject(dataParam);

			// proses mengubah string json lalu dikirim sebagai request body
			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			// proses memanggil API dan mengirimkan body
			var request = await client.PostAsync(RouteAPI + "apiRole/CreateRole", content);

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

		public async Task<bool> CheckRole(string roleName, int id)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/CheckRole/{roleName}/{id}");
			bool data = JsonConvert.DeserializeObject<bool>(apiResponse);

			return data;
		}

		public async Task<VMTblRole> GetDataById(int id)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/GetDataById/{id}");
			VMTblRole data = JsonConvert.DeserializeObject<VMTblRole>(apiResponse)!;

			return data;
		}

		public async Task<VMResponse> Edit(VMTblRole dataParam)
		{
			string json = JsonConvert.SerializeObject(dataParam);

			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			var request = await client.PutAsync(RouteAPI + "apiRole/Edit", content);

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

		public async Task<VMResponse> Delete(int id, int createdBy)
		{
			var request = await client.DeleteAsync(RouteAPI + $"apiRole/Delete/{id}/{createdBy}");

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

		public async Task<VMTblRole> GetDataById_MenuAccess(int id)
		{
			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/GetDataById_MenuAccess/{id}");
			VMTblRole data = JsonConvert.DeserializeObject<VMTblRole>(apiResponse)!;

			return data;
		}
	}
}
