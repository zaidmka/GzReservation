﻿namespace GzReservation.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserEntity user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);

        int GetUserId();

        string GetUserEmail();

        Task<UserEntity> GetUserByEmail(string email);
    }
}
