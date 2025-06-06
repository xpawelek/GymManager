using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class CreateEquipmentDto
    {
        [Required(ErrorMessage = "Equipment name is required.")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(300, ErrorMessage = "Description must be at most 300 characters.")]
        public string Description { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Notes must be at most 300 characters.")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Photo path is required.")]
        public string PhotoPath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or more.")]
        public int Quantity { get; set; }
    }
}
