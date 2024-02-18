using GzReservation.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace GzReservation.Client.Services.SecurityFormService
{
    public class SecurityFormService:ISecurityFormService
    {
        private readonly HttpClient _http;

        public SecurityFormService(HttpClient http)
        {
            _http = http;
        }

       
}}
