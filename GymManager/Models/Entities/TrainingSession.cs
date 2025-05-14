using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class TrainingSession
{
    //sesje treninegowe grupowe oraz indywidualne
    //zeby miec mozliwosc kontroli, ktory trener jest w danym momencie dostepny
    //czas sesji treningowych jest ustalony przez admina, natomiast sesje indywidualne ustala trener z klientem
    //i to trener zaznacza ze wtedy jest niedostpeny od danej godziny do danej godziny oraz co wtedy robi
    //id, date, hour, trainer, description, type of training (group/individual)
    public int Id { get; set; }
    
    [Required]
    public int TrainerId { get; set; }
    
    [Required]
    public Trainer Trainer { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public TimeSpan Duration { get; set; }
    
    [Required]
    public bool IsGroupSession { get; set; }
    
    public int? MemberId {get; set;}
    public Member? Member { get; set; }
    public int? WorkoutNoteId { get; set; }
    public WorkoutNote? WorkoutNote { get; set; }
    
}