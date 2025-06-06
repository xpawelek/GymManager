using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class ServiceRequest
{
    //zgloszenia bledow, napraw etc, taski do wykonania ktore admin moze komus zlecic
    //id, note
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string ServiceProblemTitle { get; set; }

    [Required]
    [StringLength(250)]
    public string ProblemNote { get; set; }

    public DateTime RequestDate { get; set; } = DateTime.Now;

    public bool IsResolved { get; set; } = false;
}