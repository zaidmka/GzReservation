﻿using GzReservation.Shared;

namespace GzReservation.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
        Task<ServiceResponse<bool>> Firstlogin(UserFirstLogin request);
        Task<ServiceResponse<UserEntity>> GetUserInfo(string userEmail);
        Task<ServiceResponse<UserEntityChangeDetails>> UserChangeDetails(UserEntityChangeDetails userEntityChange);
		Task<ServiceResponse<bool>> ChangePasswordAdmin(UserLogin request);


	}
}
