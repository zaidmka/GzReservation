using System.Net.Http.Json;

namespace GzReservation.Client.Services.ReservationService
{
	public class ReservationService : IReservationService
	{
		private readonly HttpClient _http;

		public ReservationService(HttpClient http)
        {
			_http = http;
		}
        public async Task<ServiceResponse<List<int>>> GetReservationSpotsAsync(int entityId)
		{
			var result = await _http.GetFromJsonAsync<ServiceResponse<List<int>>>($"api/reservation/{entityId}");
			return result;
		}
	}
}
