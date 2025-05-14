namespace GymManager.Models.Entities;

public class WeeklySchedule
{
    //plan np na przyszly tydzien/dwa, po uplywie czasu np ląduje w archiwum or sth
    //list of training group sessions
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}