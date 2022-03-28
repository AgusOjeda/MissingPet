using MissingPets.Interfaces;
using MissingPets.Models;
using MissingPets.Models.Dtos;
using MissingPets.Models.Response;
using MissingPets.Tools;

namespace MissingPets.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async UserResponse Auth(AuthUser model)
        {
            UserResponse userResponse = null;
            string sPassword = Encrypt.GetSHA256(model.Password);

            var usuario = await _userRepository.GetAsync(x => x.Email == model.Email && x.Password == sPassword);
            if (usuario == null)
            {
                return null;
            }
            userResponse.Email = usuario.Email;
            return userResponse;
        }
    }
}
