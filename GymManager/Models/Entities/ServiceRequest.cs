﻿using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class ServiceRequest
{
    //zgloszenia bledow, napraw etc, taski do wykonania ktore admin moze komus zlecic
    //id, note, photo (can be null)
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string ServiceProblemTitle { get; set; }
    
    [Required]
    [StringLength(250)]
    public string ProblemNote {get; set;}
    
    public string? ImagePath { get; set; }
    
    public int? EquipmentId { get; set; }
    
    public DateTime RequestDate { get; set; }
    public Equipment? Equipment { get; set; }
}