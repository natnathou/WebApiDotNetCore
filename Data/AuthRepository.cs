using System;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using dotnet_rpg.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            System.Console.WriteLine(user);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User dosen't exist!";
                return response;
            }


            if (VerifyPasspordHash(password, user.PasswordHash, user.Salt) && user.ToString() != "")
            {
                response.Data = user.Id.ToString();
                response.Success = true;
                response.Message = "Username was found!";
            }
            else
            {
                response.Success = false;
                response.Message = "Password wrong!";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExist(user.Username))
            {
                response.Success = false;
                response.Message = "User Already exist!";

            }
            else
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.Salt = passwordSalt;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                response.Data = user.Id;
                response.Success = true;
                response.Message = "User has created!";
            }

            return response;

        }

        public async Task<bool> UserExist(string username)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasspordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }


    }
}