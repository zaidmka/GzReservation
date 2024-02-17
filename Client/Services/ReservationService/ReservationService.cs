using GzReservation.Client.DTOs;
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



        public async Task<ServiceResponse<List<Reservation>>> GetReservationByEntity(int entityId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Reservation>>>($"api/reservation/reservations/{entityId}");
            return result;
        }

		public async Task<ServiceResponse<List<HourAvailability>>> GetReservationByHour(DateOnly reservationDate)
		{
			try
			{
				string formattedDate = reservationDate.ToString("yyyy-MM-dd"); // Adjust the format as needed
				var result = await _http.GetFromJsonAsync<ServiceResponse<List<HourAvailability>>>($"api/Reservation/reservationhour/{formattedDate}");
				return result;
			}
			catch (Exception ex)
			{
				// Log the exception or handle it as needed
				Console.WriteLine($"Error in GetReservationByHour: {ex.Message}");
				return null; // Or handle the error as appropriate
			}
		}


		public async Task<ServiceResponse<List<int>>> GetReservationSpotsAsync(int entityId)
		{
			var result = await _http.GetFromJsonAsync<ServiceResponse<List<int>>>($"api/reservation/{entityId}");
			return result;
		}
	}
}
