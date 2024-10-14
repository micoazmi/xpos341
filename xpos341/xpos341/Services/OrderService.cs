using Newtonsoft.Json;
using System.Text;
using xpos341.viewmodels;

namespace xpos341.Services
{
	public class OrderService
	{
		private static readonly HttpClient client = new HttpClient();
		private IConfiguration configuration;
		private string RouteAPI = "";
		private VMResponse response = new VMResponse();

        public OrderService(IConfiguration _configuration)
        {
            configuration = _configuration;
			RouteAPI = configuration["RouteAPI"]!;
		}

        public async Task<VMResponse> SubmitOrder(VMOrderHeader dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);

            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiOrder/SubmitOrder", content);

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

        public async Task<List<VMOrderHeader>> GetDataOrderHeaderDetail(int IdCustomer)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiOrder/GetDataOrderHeaderDetail/{IdCustomer}");
            List<VMOrderHeader> data = JsonConvert.DeserializeObject<List<VMOrderHeader>>(apiResponse)!;

            return data;
        }

        public async Task<int> CountTransaction(int IdCustomer)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiOrder/CountTransaction/{IdCustomer}");
            int data = JsonConvert.DeserializeObject<int>(apiResponse)!;

            return data;
        }

    }
}
