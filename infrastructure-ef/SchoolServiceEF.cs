using System.Linq.Expressions;
using core.Interfaces;
using core.Mappers;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace infrastructure_ef;

public class SchoolServiceEF : ISchoolService
{
    private SchoolServiceContextDb _contextDb;

    public SchoolServiceEF(SchoolServiceContextDb contextDb)
    {
        _contextDb = contextDb;
    }

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

    public Student AddStudent(NewStudent newStudent)
    {
        var student = new Student()
        {
            FirstName = newStudent.FirstName,
            LastName = newStudent.LastName,
            Birth = newStudent.Birth,
            Phone = newStudent.Phone
        };

        _contextDb.Entry(student).State = EntityState.Added;

        _contextDb.Students.Add(student);

        _contextDb.SaveChanges();
        return student;
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

    }

    public void ReplaceStudentGroup(int studentId, int groupId)
    {
        using (var transaction = _contextDb.Database.BeginTransaction())
        {
            try
            {
                var student = _contextDb.Students.Find(studentId);
                student?.StudentGroup.RemoveStudent(student);
                var group = _contextDb.Groups.Find(groupId);
                group?.AddStudent(student);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback(); // nie jest konieczne wywołanie Rollback w tym miejscu!!!
            }
        }
    }

    public void AddStudentToNewGroup(NewStudent newStudent, string studentGroupName)
    {
        StudentGroup group = new StudentGroup()
        {
            Name = studentGroupName
        };
        var student = new Student()
        {
            FirstName = newStudent.FirstName,
            LastName = newStudent.LastName,
            Birth = newStudent.Birth,
            Phone = newStudent.Phone,
            StudentGroup = group
        };

        _contextDb.Students.Add(student);
        _contextDb.SaveChanges();
    }
}