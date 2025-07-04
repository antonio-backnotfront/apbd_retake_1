namespace RecordMania.Models;

public class Teacher
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public int AcademicTitleId { get; set; }
    public int Level { get; set; }
}