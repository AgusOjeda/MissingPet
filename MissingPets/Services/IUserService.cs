using MissingPets.Models.Dtos;
using MissingPets.Models.Response;

namespace MissingPets.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthUser model);
    }
}
