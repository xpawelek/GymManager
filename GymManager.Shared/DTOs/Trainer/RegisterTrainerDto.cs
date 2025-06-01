namespace GymManager.Shared.DTOs.Trainer;

public class RegisterTrainerDto
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    public String PhoneNumber { get; set; }
    public string Description { get; set; } //konieczne dodajac?
    public string PhotoPath { get; set; } //konieczne dodajac?

}