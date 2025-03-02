using core.Domain;
using core.Dto;
using core.Interfaces;
using core.Models;
using infrastructure_ef.Entities;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace infrastructure_ef.Services;
/// <summary>
///
/// Implementacja interfejsu serwisu domenowego z użyciem klas encji.
/// Metody serwisu zwracają klasy domentowe
/// </summary>
public class SchoolServiceEF : ISchoolService
{
    private readonly SchoolServiceContextDb _contextDb;

    public SchoolServiceEF(SchoolServiceContextDb contextDb)
    {
        _contextDb = contextDb;
    }

    public Task<List<Student>> FindAllAStudentsAsync()
    {
        return _contextDb.Students.AsNoTracking().Include(s => s.StudentGroupEntity).Select(s => s.To()).ToListAsync();
    }

    public Task<StudentGroup?> FindGroupByIdAsync(int id)
    {
        var findAsync = _contextDb.Groups.AsNoTracking().Where(g => g.Id == id).Select(g => g.To());
        return Task.FromResult(findAsync.FirstOrDefault());
    }

    public Task<Student?> FindStudentByIdAsync(int id)
    {
        return Task.FromResult(_contextDb.Students.AsNoTracking().FirstOrDefault(e => e.Id == id)?.To());
    }

    public void AssignStudentToGroup(int studentId, int groupId)
    {
        var student = new StudentEntity() {Id = studentId};
        var group = new StudentGroupEntity() {Id = groupId};
        if (_contextDb.Students.Local.All(x => x.Id != studentId))
        {
            _contextDb.Entry(student).State = EntityState.Unchanged;
        }
        else
        {
            student = _contextDb.Students.Local.First(s => s.Id == studentId);
        }

        if (_contextDb.Groups.Local.All(x => x.Id != groupId))
        {
            _contextDb.Entry(group).State = EntityState.Unchanged;
        }
        else
        {
            group = _contextDb.Groups.Local.First(g => g.Id == groupId);
        }
        
        group.Students.Add(student);
        _contextDb.SaveChanges();
    }

    public Student AddStudent(NewStudent newStudent)
    {
        var student = new StudentEntity()
        {
            FirstName = newStudent.FirstName,
            LastName = newStudent.LastName,
            Birth = newStudent.Birth,
            Phone = newStudent.Phone
        };
        _contextDb.Entry(student).Property<DateTime?>("Created").CurrentValue = DateTime.Now; 
        _contextDb.Entry(student).State = EntityState.Added;
        _contextDb.SaveChanges();
        return student.To();
    }

    public bool UpdateStudentGroup(StudentGroup group)
    {
        try
        {
            _contextDb.Entry(group).State = EntityState.Modified;
            _contextDb.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public StudentGroup AddGroup(NewStudentGroup group)
    {
        var entityEntry = _contextDb.Groups.Add(
            new StudentGroupEntity()
            {
                Name = group.Name
            }
        );
        _contextDb.SaveChanges();
        return entityEntry.Entity.To();
    }

    public void RemoveStudentById(int id)
    {
        var student = new StudentEntity {Id = id};
        _contextDb.Entry(student).State = EntityState.Unchanged;
        _contextDb.Students.Remove(student);
        _contextDb.SaveChanges();
    }
}