using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class Member
{
    //czlonek silowni - moze kupic trening/subskrypcje/wybrac trenera/
    //kontatkowac sie z nim - wysylac zdjecia progressu, pytania etc, moze zglaszac uszkodzenia sprzetu
    //id, firstname, surname, email, date of birth, some kind of idenetity checker (mayber gym card and every member has other)?
    
    public int Id { get; set; }  // -> dto get: nie chcemy
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = String.Empty;  // -> dto get: chcemy

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = String.Empty;  // -> dto get: chcemy
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;  // -> dto get: chcemy
    
    [Required]
    public DateTime DateOfBirth { get; set; } // -> dto get: raczej nie chcemy
    
    [Required]
    [StringLength(10)]
    public string MembershipCardNumber { get; set; } = String.Empty;  // -> dto get: chcemy
    
    [Required]
    [StringLength(15)]
    [Phone]
    public string PhoneNumber { get; set; } = String.Empty;  // -> dto get: chcemy
}