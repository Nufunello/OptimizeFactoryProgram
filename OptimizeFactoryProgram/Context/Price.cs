using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptimizeFactoryProgram.Context
{
    public class Price<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid PriceOfId { get; set; } = default!;
        [ForeignKey("PriceOfId")]
        [ValidateNever]
        public virtual T PriceOf { get; set; } = default!;
        public double Count { get; set; } = 1;
        [Required]
        public double Cost { get; set; } = default!;
        public DateTime ActualDate { get; set; } = DateTime.Now;
    }
}
