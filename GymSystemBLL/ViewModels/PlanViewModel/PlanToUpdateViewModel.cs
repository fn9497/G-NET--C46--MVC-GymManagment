using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.PlanViewModel
{
    public class PlanToUpdateViewModel
    {
        [Required(ErrorMessage = "Plan name is required.")]
        [StringLength(100, MinimumLength = 3,
           ErrorMessage = "Plan name must be between 3 and 100 characters.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500,
            ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(typeof(decimal), "0.01", "1000000",
            ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 3650,
            ErrorMessage = "Duration must be at least 1 day.")]
        public int DurationDays { get; set; }

        public bool IsActive { get; set; }
    }
}
