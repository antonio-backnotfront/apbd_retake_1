using System.ComponentModel.DataAnnotations;

namespace RecordMania.Models.DTO;

public class CreateTaskDto
{
    [Required] 
    public required int Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? RequiredLevel { get; set; }
    public decimal? MinRequiredTime { get; set; }
    
}