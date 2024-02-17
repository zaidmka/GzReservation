
using GzReservation.Client.Pages;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;

namespace GzReservation.Client.Services.OracleService
{
    public class OracleService : IOracleService
    {
        private readonly HttpClient _http;

        public OracleService(HttpClient http)
        {
            _http = http;
        }

        public Form OracleData { get; set; }
        public List<PreReservation> ValidRecords { get; set; }

        public event Action OracleChange;

        public async Task GetDataAsync(int docNo)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Form>>($"api/oracle/{docNo}");
            if (result != null && result.Data != null)
            {
                OracleData = result.Data;
                OracleChange?.Invoke(); // If you want to notify subscribers about the change
            }
            else
            {
                // Handle the case where result or result.Data is null
                OracleData = null;
            }
        }

        public async Task<ServiceResponse<List<PreReservation>>> GetPreValidRecords(int entityId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<PreReservation>>>($"api/oracle/entity/{entityId}"); //change from api/oracle/entity to api/test/entity for testing ! 
			return result;
        }

    }
}
