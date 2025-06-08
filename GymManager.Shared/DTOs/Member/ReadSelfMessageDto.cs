using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class ReadSelfMessageDto
{
    public int Id { get; set; }

    public string MessageContent { get; set; }
    
    public DateTime Date { get; set; }

    public int TrainerId { get; set; }

    public bool SentByMember { get; set; }
}