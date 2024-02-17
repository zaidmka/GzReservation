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

       
}
