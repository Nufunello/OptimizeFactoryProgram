using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptimizeFactoryProgram.Context
{
    public class Material
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public Measures Measure { get; set; } = default!;
    }
}
