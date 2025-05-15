namespace GymManager.Models.DTOs.Member;

public class ReadTrainerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string PhotoPath { get; set; }
}