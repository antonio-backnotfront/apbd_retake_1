namespace RecordMania.Models;

public class Record
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int RecordTypeId { get; set; }
    public int TaskId { get; set; }
    public decimal ExecutionTime { get; set; }
    public DateTime CreatedAt { get; set; }
    
}