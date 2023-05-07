using core.Domain;

namespace core.Models;

public class DtoStudentGroup
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public List<Student> Students { get; set; }
}