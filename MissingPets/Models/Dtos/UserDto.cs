using System;
using System.ComponentModel.DataAnnotations;

namespace MissingPets.Models.Dtos
{
    public record AuthUser([Required] string Email, [Required] string Password);
    public record UserDto(Guid Id, string Name, [Required] string Email, [Required] string Password);
    public record CreateUserDto(string Name, [Required] string Email, [Required] string Password);

    public record UpdateUserDto(string Name, [Required] string Email, [Required] string Password);
}
