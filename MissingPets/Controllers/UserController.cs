using Microsoft.AspNetCore.Mvc;
using MissingPets.Interfaces;
using MissingPets.Models;
using MissingPets.Models.Dtos;
using MissingPets.Models.Response;
using MissingPets.Services;
using MissingPets.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;

        public UserController(IRepository<User> usersRepository, IUserService userService)
        {
            _userRepository = usersRepository;
            _userService = userService;
        }

        
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthUser model)
        {
            Response response = new Response();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                response.Exito = 0;
                response.Mensaje = "Usuario o contraseña incorrectos";
                return BadRequest(response);
            }
            response.Exito = 1;
            response.Mensaje = "Usuario autentificado";
            response.Data = userResponse;
            return Ok(response);
        }
    }
}
