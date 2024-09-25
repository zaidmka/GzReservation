using System.Net.Http.Json;

namespace GzReservation.Client.Services.AnnouncementService
{
    public class AnnouncementService:IAnnouncementService
    {
		private readonly HttpClient _http;

		public AnnouncementService(HttpClient http)
        {
			_http = http;
		}

		public async Task<ServiceResponse<List<Dbmessage>>> GetActiveDbMessage()
		{
			try
			{
				var response = await _http.GetFromJsonAsync<ServiceResponse<List<Dbmessage>>>("/api/Announcement/active");
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
