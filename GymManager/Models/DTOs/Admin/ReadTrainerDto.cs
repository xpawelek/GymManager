namespace GymManager.Models.DTOs.Admin;

public class ReadTrainerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string PhotoPath { get; set; }
}