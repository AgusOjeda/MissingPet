using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissingPets.Dtos;
using MissingPets.Interfaces;
using MissingPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IRepository<Pet> petsRepository;

        public PetsController(IRepository<Pet> petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetAsync()
        {
            var items = (await petsRepository.GetAllAsync()).Select(item => item.AsPetDto());
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetByIdAsync(Guid id)
        {
            var item = await petsRepository.GetAsync(id);

            if (item == null)
                return NotFound();

            return item.AsPetDto();
        }

        // POST /items
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreatePetDto createPetDto)
        {
            var pet = new Pet
            {
                Species = createPetDto.Species,
                Name = createPetDto.Name,
                Breed = createPetDto.Breed,
                Size = createPetDto.Size,
                Sex = createPetDto.Sex,
                Color = createPetDto.Color,
                Plate = createPetDto.Plate,
                Observations = createPetDto.Observations,
                Location = createPetDto.Location,
                Pictures = createPetDto.Pictures,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await petsRepository.CreateAsync(pet);

            return Ok(pet);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdatePetDto updatePetDto)
        {
            var existingPet = await petsRepository.GetAsync(id);

            if (existingPet == null)
            {
                return NotFound();
            }
            existingPet.Species = updatePetDto.Species;
            existingPet.Name = updatePetDto.Name;
            existingPet.Breed = updatePetDto.Breed;
            existingPet.Size = updatePetDto.Size;
            existingPet.Sex = updatePetDto.Sex;
            existingPet.Color = updatePetDto.Color;
            existingPet.Plate = updatePetDto.Plate;
            existingPet.Observations = updatePetDto.Observations;
            existingPet.Location = updatePetDto.Location;
            existingPet.Pictures = updatePetDto.Pictures;

            await petsRepository.UpdateAsync(existingPet);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var pet = await petsRepository.GetAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            await petsRepository.RemoveAsync(pet.Id);

            return NoContent();
        }
    }
}
