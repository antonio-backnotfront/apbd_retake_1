namespace RecordMania.Models.DTO;

public class GetRecordDto
{
    public int Id { get; set; }
    public GetTeacherDto Teacher { get; set; }
    public string RecordType { get; set; }
    public GetTaskDto Task { get; set; }
    public decimal ExecutionTime { get; set; }
    public DateTime CreatedAt { get; set; }
    
}