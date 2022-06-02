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
            public Material Material { get; set; } = default!;
            [Required]
            public double Count { get; set; } = default!;
        }
        [Required]
        public virtual ICollection<Ingredient> Ingridients { get; set; }
        [Required]
        public Measures Measure { get; set; } = default!;
    }
}
