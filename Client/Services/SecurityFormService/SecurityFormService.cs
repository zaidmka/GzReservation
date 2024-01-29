using GzReservation.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace GzReservation.Client.Services.SecurityFormService
{
    public class SecurityFormService:ISecurityFormService
    {
        private readonly HttpClient _http;

        public SecurityFormService(HttpClient http)
        {
            _http = http;
        }

        public List<Form> Forms { get; set; }
        public List<Form> AdminForms { get; set; }
        public string message { get; set; }

        public event Action FormChange;
        public string LastSearchText { get; set; }

        public async Task<ServiceResponse<Form>> CreateForm(Form form)
        {
            try
            {
                // Send the form to the API
                var result = await _http.PostAsJsonAsync("api/securityform", form);

                // Read the response as ServiceResponse<Form>
                var serviceResponse = await result.Content.ReadFromJsonAsync<ServiceResponse<Form>>();

                if (serviceResponse == null)
                {
                    // Handle the case where serviceResponse is null if necessary
                    return new ServiceResponse<Form>
                    {
                        Data = null,
                        Success = false,
                        Message = "Received a null response from the server"
                    };
                }

                return serviceResponse;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary

                // Return a response indicating failure
                return new ServiceResponse<Form>
                {
                    Data = null,
                    Success = false,
                    Message = $"An exception occurred: {ex.Message}"
                };
            }
        }


        public Task DeleteForm(int formId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Form>> GetForm(int formId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Form>>($"api/securityform/form/{formId}");
            return (result);
        }

        public async Task GetForms()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Form>>>("api/securityform");
            Forms = result.Data;

        }

        public async Task searchForms(string searchText)
        {
            LastSearchText = searchText;

            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Form>>>($"api/securityform/search/{searchText}");
            if (result != null || result.Data != null)
            {
                Forms = result.Data;

            }
            if (Forms.Count == 0)
            {
                message = "No Forms found.";

            }
            FormChange?.Invoke();
        }

        public async Task<Form> UpdateForm(Form form)
        {
            var result = await _http.PutAsJsonAsync("api/securityform", form);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Form>>()).Data;
        }
    }
}
