using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class Equipment
{
    //sprzet dostepny na silowni
    
    //id, name, description, notes, quantity
    
    public int Id { get; set; } // -> dto get: nie chcemy wyswietlac
    
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; } // -> dto get: chcemy

    [Required] [StringLength(50)] public string Name { get; set; } = String.Empty;  // -> dto get: chcemy
    
    [Required]
    [StringLength(300)]
    public string Description { get; set; } = String.Empty;  // -> dto get: chcemy
    
    [StringLength(300)]
    public string? Notes { get; set; }  // -> dto get: chcemy
    
    public string? PhotoPath { get; set; }

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>(); // -> chcemy wysweitlac adminiowi zeby mogl przydzielic?
}