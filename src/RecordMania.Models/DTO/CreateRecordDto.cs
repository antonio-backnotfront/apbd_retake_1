using System.ComponentModel.DataAnnotations;

namespace RecordMania.Models.DTO;

public class CreateRecordDto
{
    public int? Id { get; set; }
    [Required]
    public required int TeacherId { get; set; }
    
    [Required]
    public CreateTaskDto Task { get; set; }
    
    [Required]
    public required int RecordTypeId { get; set; }
    
    [Required]
    public required decimal ExecutionTime { get; set; }
    
    
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    
}