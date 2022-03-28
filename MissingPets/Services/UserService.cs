using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MissingPets.Interfaces;
using MissingPets.Models;
using MissingPets.Models.Dtos;
using MissingPets.Models.Response;
using MissingPets.Settings;
using MissingPets.Tools;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MissingPets.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly AppSettings _appSettings;
        public UserService(IRepository<User> userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthUser model)
        {
            UserResponse userResponse = new UserResponse();
            string sPassword = Encrypt.GetSHA256(model.Password);

            var usuario = _userRepository.GetAsync(x => x.Email == model.Email && x.Password == sPassword).Result;
            if (usuario == null) return null;
            userResponse.Email = usuario.Email;
            userResponse.Token = GetToken(usuario);
            return userResponse;
        }
        private string GetToken(User usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
