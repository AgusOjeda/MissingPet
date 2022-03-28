using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissingPets.Dtos;
using MissingPets.Interfaces;
using MissingPets.Models;
using MissingPets.Models.Request;
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

        public UserController(IRepository<User> usersRepository)
        {
            this._userRepository = usersRepository;
        }

        
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest user)
        {
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAsync()
        {
            var items = (await _userRepository.GetAllAsync()).Select(item => item.AsUserDto());
            return Ok(items);
        }

        // GET /api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var item = await _userRepository.GetAsync(id);

            if (item == null)
                return NotFound();

            return item.AsUserDto();
        }

        // POST /api/user
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password
            };

            await _userRepository.CreateAsync(user);

            return Ok(user);
        }
        // PUT /api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var existingUser = await _userRepository.GetAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = updateUserDto.Name;
            existingUser.Email = updateUserDto.Email;
            

            await _userRepository.UpdateAsync(existingUser);

            return NoContent();
        }

        // DELETE /api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.RemoveAsync(user.Id);

            return NoContent();
        }

    }
}
