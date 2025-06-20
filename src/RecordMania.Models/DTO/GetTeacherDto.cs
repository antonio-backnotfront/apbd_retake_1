namespace RecordMania.Models.DTO;

public class GetTeacherDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public string AcademicTitle { get; set; }
    public int Level { get; set; }
}