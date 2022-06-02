using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptimizeFactoryProgram.Context
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = default!;
        public class Ingredient
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            [Required]
            public Guid MaterialId { get; set; }
            [ForeignKey("MaterialId")]
            [ValidateNever]
            public Material Material { get; set; } = default!;
            [Required]
            public double Count { get; set; }
        }
        [Required]
        public virtual ICollection<Ingredient> Ingridients { get; set; }
        [Required]
        public Measures Measure { get; set; } = default!;
    }
}
