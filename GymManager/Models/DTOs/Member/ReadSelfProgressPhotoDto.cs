namespace GymManager.Models.DTOs.Member;

public class ReadSelfProgressPhotoDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    public string ImagePath { get; set; }
    public bool IsPublic { get; set; }
}