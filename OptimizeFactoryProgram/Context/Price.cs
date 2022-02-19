using System.ComponentModel.DataAnnotations.Schema;

namespace OptimizeFactoryProgram.Context
{
    public class Price<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public T PriceOf { get; set; } = default!;
        public double Count { get; set; } = default!;
        public double Cost { get; set; } = default!;
        public DateTime ActualDate { get; set; } = default!;
    }
}
