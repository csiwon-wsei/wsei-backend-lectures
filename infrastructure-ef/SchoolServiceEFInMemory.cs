using core.Interfaces;
using core.Models;

namespace infrastructure_ef;

public class SchoolServiceEFInMemory: ISchoolService
{
    public Task<List<Student>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StudentGroup?> FindGroupByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> FindStudentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void AssignStudentToGroup(int studentId, int groupId)
    {
        throw new NotImplementedException();
    }

    public Student AddStudent(NewStudent student)
    {
        throw new NotImplementedException();
    }

    public bool UpdateStudentGroup(StudentGroup student)
    {
        throw new NotImplementedException();
    }

    public StudentGroup AddGroup(NewStudentGroup group)
    {
        throw new NotImplementedException();
    }

    public void RemoveById(int id)
    {
        throw new NotImplementedException();
    }
}