using System.ComponentModel.DataAnnotations.Schema;

namespace OptimizeFactoryProgram.Context
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public class Ingredient
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public Material Material { get; set; } = default!;
            public double Count { get; set; } = default!;
        }
        public IEnumerable<Ingredient> Receipt { get; set; } = default!;
        public Measures Measure { get; set; } = default!;
    }
}
