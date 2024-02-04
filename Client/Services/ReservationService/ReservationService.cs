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

        public async Task<ServiceResponse<Reservation>> AddReservation(ReservationDto reservationDto)
        {

                // Send the form to the API
                var result = await _http.PostAsJsonAsync("api/reservation", reservationDto);

                // Read the response as ServiceResponse<Form>
                var serviceResponse = await result.Content.ReadFromJsonAsync<ServiceResponse<Reservation>>();

                if (serviceResponse == null)
                {
                    // Handle the case where serviceResponse is null if necessary
                    return new ServiceResponse<Reservation>
                    {
                        Data = null, // Use default instead of null
                        Success = false,
                        Message = "Received a null response from the server"
                    };
                }

                return serviceResponse;
            

        }


        public async Task<ServiceResponse<List<int>>> GetReservationSpotsAsync(int entityId)
		{
			var result = await _http.GetFromJsonAsync<ServiceResponse<List<int>>>($"api/reservation/{entityId}");
			return result;
		}
	}
}
