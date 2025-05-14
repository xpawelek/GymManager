using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class Equipment
{
    //sprzet dostepny na silowni
    
    //id, name, description, notes, quantity
    
    public int Id { get; set; }
    
    public int Quantity { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string Description { get; set; }
    
    [StringLength(300)]
    public string? Notes { get; set; }
    
    public ICollection<ServiceRequest> ServiceRequests { get; set; }
}