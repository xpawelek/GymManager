using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class UpdateEquipmentDto
    {
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters.")]
        public string? Name { get; set; }

        [StringLength(300, ErrorMessage = "Description must be at most 300 characters.")]
        public string? Description { get; set; }

        [StringLength(300, ErrorMessage = "Notes must be at most 300 characters.")]
        public string? Notes { get; set; }

        public string? PhotoPath { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater.")]
        public int? Quantity { get; set; }
    }
}