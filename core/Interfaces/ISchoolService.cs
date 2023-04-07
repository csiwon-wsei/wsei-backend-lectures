using core.Models;

namespace core.Interfaces;

public interface ISchoolService
{
    Task<List<Student>> FindAllAsync();

    Task<StudentGroup?> FindGroupByIdAsync(int id);

    Task<Student> FindStudentByIdAsync(int id);

    void AssignStudentToGroup(int studentId, int groupId);

    Student AddStudent(NewStudent newStudent);

    bool UpdateStudentGroup(StudentGroup student);
    
    StudentGroup AddGroup(NewStudentGroup group);

    public void RemoveById(int id);
    public void ReplaceStudentGroup(int studentId, int groupId);
}