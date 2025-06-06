using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class UpdateMessageDto
    {
        [StringLength(1000, ErrorMessage = "Message content must be at most 1000 characters.")]
        public string? MessageContent { get; set; }
    }
}
