using GzReservation.Client.Pages;

namespace GzReservation.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserEntity user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        Task<ServiceResponse<bool>> FirstLogin(int userId, string newPassword,string newFullName);

        int GetUserId();

        string GetUserEmail();

        Task<ServiceResponse<UserEntity>> GetUserByEmail(string email);
        Task<ServiceResponse<UserEntityChangeDetails>> ChangeUserName(UserEntityChangeDetails userEntityChange);

		Task<ServiceResponse<bool>> ChangePasswordAdmin(UserLogin request);

	}
}
