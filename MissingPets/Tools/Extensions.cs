using MissingPets.Models.Dtos;
using MissingPets.Models;

namespace MissingPets.Tools
{
    public static class Extensions
    {
        public static PetDto AsPetDto(this Pet pet)
        {
            return new PetDto(pet.Id, pet.Species, pet.Name, pet.Breed, pet.Size, pet.Sex, pet.Color, pet.Plate, pet.Observations, pet.Location, pet.Pictures, pet.CreatedDate);
        }

        public static UserDto AsUserDto(this User user)
        {
            return new UserDto(user.Id, user.Name, user.Email, user.Password);
        }
    }
}
