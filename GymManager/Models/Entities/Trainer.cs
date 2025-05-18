using System.ComponentModel.DataAnnotations;
using GymManager.Models.Identity;

namespace GymManager.Models.Entities;

public class Trainer
{
    //moze zostac przypisany do jakiegos treningu grupowego przez admina, jesli nie jest wtedy zajety,
    //kontaktuje sie z podopoeiecznymi ktorzy go wybrali, moze zglaszac uszkodzenia
    //id, firstname, surname, description, photo
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    
    [Required]
    public string PhotoPath { get; set; }
    
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}