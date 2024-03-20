using GzReservation.Client.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GzReservation.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _context.usersentity.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }

        public async Task<ServiceResponse<UserEntity>> GetUserByEmail(string email)
        {
            var user = await _context.usersentity
                .Include(e=>e.Entity)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new ServiceResponse<UserEntity> { Data = null, Success = false, Message = "User not found." };
            }
            else
            {
                return new ServiceResponse<UserEntity> { Data = user, Success = true };
            }
        }


        public string GetUserEmail()
        {
            throw new NotImplementedException();
        }

        public int GetUserId()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.usersentity
                .Include(e => e.Entity)
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()))
                ;
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
                response.Success = true;
                response.Message = "okay";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(UserEntity user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "اويليي يا ربااااااااااااااااااااك , ولك اليوزر موجود!"
                };
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.usersentity.Add(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse<int> { Success = true, Message = "حبيبي ابو حسين انه خادمكم الصغير, تم اضافة اليوزر وتدلل", Data = user.Id };

        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.usersentity.AnyAsync(user => user.Email.ToLower()
                .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.fullname),
               // new Claim(ClaimTypes.GivenName, user.Entity.entity_name),
               // new Claim(ClaimTypes.PostalCode, user.EntityId.ToString()),
                new Claim(ClaimTypes.Country, user.new_user),



            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> FirstLogin(int userId, string newPassword, string newFullName)
        {
            var user = await _context.usersentity.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                { Data = false,
                    Success = false,
                    Message = "User not found."
                };
            }
            if(user.new_user == "no")
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "المستخدم موجود مسبقا وغير جديد"
                };
            }
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.fullname = newFullName;
            user.new_user = "no";

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "تم تغيير المعلومات بنجاح.", Success = true };
        }

		public async Task<ServiceResponse<UserEntityChangeDetails>> ChangeUserName(UserEntityChangeDetails userEntityChange)
		{
			var response = await _context.usersentity
                .Where(u=>u.Email == userEntityChange.Email)
                .FirstOrDefaultAsync();
            if(response == null)
            {
                return new ServiceResponse<UserEntityChangeDetails>
                {
                    Data = null,
                    Success = false,
                    Message = "User Not Found"
                };
            }

            //response.EntityId = userEntityChange.EntityId;
            response.fullname = userEntityChange.fullname;
            //response.new_user = userEntityChange.state;
            await _context.SaveChangesAsync();
            return new ServiceResponse<UserEntityChangeDetails> { Data=userEntityChange, Success = true,Message="okay" };
		}
	}

}
