using core.Domain;
using core.Dto;
using core.Models;

namespace core.Interfaces;

public interface ISchoolService
{
    Task<List<Student>> FindAllAStudentsAsync();

    Task<StudentGroup?> FindGroupByIdAsync(int id);

    Task<Student?> FindStudentByIdAsync(int id);

    void AssignStudentToGroup(int studentId, int groupId);

    Student AddStudent(NewStudent newStudent);

    StudentGroup AddGroup(NewStudentGroup group);
}