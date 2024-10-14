using Newtonsoft.Json;
using System.Text;
using xpos341.viewmodels;

namespace xpos341.Services
{
	public class CustomerService
	{
		private static readonly HttpClient client = new HttpClient();
		private IConfiguration configuration;
		private string RouteAPI = "";
		private VMResponse response = new VMResponse();

		public CustomerService(IConfiguration _configuration)
		{
			configuration = _configuration;
			RouteAPI = configuration["RouteAPI"]!;
		}

		public async Task<List<VMTblCustomer>> GetAllData()
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + "apiCustomer/GetAllData");
			List<VMTblCustomer> data = JsonConvert.DeserializeObject<List<VMTblCustomer>>(apiResponse)!;

			return data;
		}

		public async Task<bool> CheckEmail(string email, int id)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCustomer/CheckByEmail/{email}/{id}");
			bool data = JsonConvert.DeserializeObject<bool>(apiResponse);

			return data;
		}

		public async Task<VMResponse> Create(VMTblCustomer dataParam)
		{
			// proses convert dari object ke string
			string json = JsonConvert.SerializeObject(dataParam);

			// proses mengubah string json lalu dikirim sebagai request body
			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			// proses memanggil API dan mengirimkan body
			var request = await client.PostAsync(RouteAPI + "apiCustomer/Save", content);

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

		public async Task<VMTblCustomer> GetDataById(int id)
		{

			string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCustomer/GetDataById/{id}");
			VMTblCustomer data = JsonConvert.DeserializeObject<VMTblCustomer>(apiResponse)!;

			return data;
		}

		public async Task<VMResponse> Edit(VMTblCustomer dataParam)
		{
			string json = JsonConvert.SerializeObject(dataParam);

			StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

			var request = await client.PutAsync(RouteAPI + "apiCustomer/Edit", content);

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
			var request = await client.DeleteAsync(RouteAPI + $"apiCustomer/Delete/{id}");

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
