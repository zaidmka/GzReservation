using GzReservation.Shared;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http.Json;

namespace GzReservation.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;

        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

		public async Task<ServiceResponse<bool>> ChangePasswordAdmin(UserLogin request)
		{
			var result = await _http.PostAsJsonAsync("api/Auth/AdminChangePassword", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
		}

		public async Task<ServiceResponse<bool>> Firstlogin(UserFirstLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/firstLogin", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<UserEntity>> GetUserInfo(string userEmail)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<UserEntity>>($"api/auth/{userEmail}");
            return result;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

		public async Task<ServiceResponse<UserEntityChangeDetails>> UserChangeDetails(UserEntityChangeDetails userEntityChange)
		{
			var result = await _http.PostAsJsonAsync("api/auth/changeUserEntityName", userEntityChange);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<UserEntityChangeDetails>>();
		}
	}
}
