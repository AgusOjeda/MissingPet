using MissingPets.Interfaces;
using System;

namespace MissingPets.Models
{
    public class Pet : IEntity
    {
        public Guid Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Size { get; set; }
        public string Sex { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public string Observations { get; set; }
        public string Location { get; set; }
        public string[] Pictures { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
