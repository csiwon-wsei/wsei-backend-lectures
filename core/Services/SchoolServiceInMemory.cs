using core.Generators;
using core.Interfaces;
using core.Models;

namespace core.Services;

public class SchoolServiceInMemory: ISchoolService
{
    private IntGenerator _studentId = new IntGenerator();
    private IntGenerator _groupId = new IntGenerator();
    private Dictionary<int, Student> _students = new();
    private Dictionary<int, StudentGroup> _groups = new();
    
    public Task<List<Student>> FindAllAsync()
    {
        return Task.FromResult(_students.Values.ToList());
    }

    public Task<StudentGroup?> FindGroupByIdAsync(int id)
    {
        return Task.FromResult(_groups.TryGetValue(id, out var group)? group : null);
    }

    public Task<Student> FindStudentByIdAsync(int id)
    {
        return Task.FromResult(_students.TryGetValue(id, out var student)? student : null);
    }

    public void AssignStudentToGroup(int studentId, int groupId)
    {
        if (_students.TryGetValue(studentId, out var student) && _groups.TryGetValue(groupId, out var group))
        {
            var st = student.WithGroup(group);
            group.AddStudent(st);
            _students[st.Id] = st;
        }
    }

    public Student AddStudent(NewStudent newStudent)
    {
        _students[_studentId.Next] = new Student(
            id: _studentId.Current, 
            firstName: newStudent.FirstName,
            lastName: newStudent.LastName,
            birth: newStudent.Birth,
            phone: newStudent.Phone,
            studentGroup: null
            );
        return _students[_studentId.Current];
    }

    public bool UpdateStudentGroup(StudentGroup group)
    {
        if (_groups.TryGetValue(group.Id, out var gr))
        {
            _groups[gr.Id] = group;
            return true;
        }
        return false;
    }

    public StudentGroup AddGroup(NewStudentGroup group)
    {
        _groups[_groupId.Next] = new StudentGroup(id: _groupId.Current, name: group.Name);
        return _groups[_groupId.Current];
    }

    public void RemoveById(int id)
    {
        _students.Remove(id);
    }

    public void ReplaceStudentGroup(int studentId, int groupId)
    {
        throw new NotImplementedException();
    }
}