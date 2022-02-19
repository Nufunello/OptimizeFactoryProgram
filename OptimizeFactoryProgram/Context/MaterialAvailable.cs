using System.ComponentModel.DataAnnotations.Schema;

namespace OptimizeFactoryProgram.Context
{
    public class MaterialAvailable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Material Material { get; set; } = default!;
        public double Count { get; set; } = default!;
    }
}
