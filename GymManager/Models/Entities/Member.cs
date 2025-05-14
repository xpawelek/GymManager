using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class Member
{
    //czlonek silowni - moze kupic trening/subskrypcje/wybrac trenera/
    //kontatkowac sie z nim - wysylac zdjecia progressu, pytania etc, moze zglaszac uszkodzenia sprzetu
    //id, firstname, surname, email, date of birth, some kind of idenetity checker (mayber gym card and every member has other)?
    
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public string MembershipCardNumber { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
}