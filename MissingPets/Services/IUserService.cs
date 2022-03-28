using MissingPets.Models.Request;
using MissingPets.Models.Response;

namespace MissingPets.Services
{
    public interface IUserService
    {
        Response Auth(AuthRequest model);
    }
}
