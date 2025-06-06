using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class CreateMessageDto
    {
        [Required(ErrorMessage = "Message content is required.")]
        [StringLength(1000, ErrorMessage = "Message content must be at most 1000 characters.")]
        public string MessageContent { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Trainer ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Trainer ID must be a positive number.")]
        public int TrainerId { get; set; }
    }
}
