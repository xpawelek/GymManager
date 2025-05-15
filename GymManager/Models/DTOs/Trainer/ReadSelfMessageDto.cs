namespace GymManager.Models.DTOs.Trainer;

public class ReadSelfMessageDto
{
    public int Id { get; set; }
    public string MessageContent { get; set; }
    
    public DateTime Date { get; set; }

    public int MemberId { get; set; }
}