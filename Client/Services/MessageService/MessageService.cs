using System.Net.Http.Json;
using System.Text.Json;

namespace GzReservation.Client.Services.MessageService
{
	public class MessageService : IMessageService
	{
		private readonly HttpClient _http;

		public MessageService(HttpClient http)
        {
			_http = http;
		}
        public async Task<ServiceResponse<List<Message>>> SendMessageReservationAsync(int doc_no, string reservationdate, string reservationhour)
        {
            try
            {
                // Encode URL parameters
                string encodedReservationDate = Uri.EscapeDataString(reservationdate);
                string encodedReservationHour = Uri.EscapeDataString(reservationhour);

                // Construct the encoded URL
                string apiUrl = $"http://10.10.34.90:2028/api/Message/reservation/{doc_no}/{encodedReservationDate}/{encodedReservationHour}";

                var response = await _http.GetFromJsonAsync<ServiceResponse<List<Message>>>(apiUrl);

                if (response == null)
                {
                    // Handle case where response is null
                    throw new Exception("Received null response from the API.");
                }

                return response;
            }
            catch (Exception ex)
            {
                // Handle the exception or log it for debugging
                Console.WriteLine($"Error in SendMessageReservationAsync: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }



    }
}
