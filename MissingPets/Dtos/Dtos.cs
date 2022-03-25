using System;
using System.ComponentModel.DataAnnotations;

namespace MissingPets.Dtos
{
    public record PetDto(
         Guid Id,
         string Species,
         string Name,
         string Breed,
         string Size,
         string Sex,
         string Color,
         string Plate,
         string Observations,
         string Location,
         string[] Pictures,
         DateTimeOffset CreatedDate
         );

    public record CreatePetDto(
        [Required] string Species,
        [Required] string Name,
        string Breed,
        string Size,
        [Required] string Sex,
        [Required] string Color,
        string Plate,
        string Observations,
        [Required] string Location,
        string[] Pictures);

    public record UpdatePetDto(
        [Required] string Species,
        [Required] string Name,
        string Breed,
        string Size,
        [Required] string Sex,
        [Required] string Color,
        string Plate,
        string Observations,
        [Required] string Location,
        string[] Pictures);

    public record UserDto(Guid Id,string Name, string Email);
    public record CreateUserDto(string Name, string Email);

    public record UpdateUserDto(string Name, string Email);
}
