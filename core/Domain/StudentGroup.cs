namespace core.Models;

public class StudentGroup
{
    public int Id { get; init; }
    public string Name { get; init; }

    private ISet<Student> _students;

    public IEnumerable<Student> Students => _students.AsEnumerable();

    public StudentGroup(int id, string name)
    {
        Id = id;
        Name = name;
        _students = new HashSet<Student>();
    }

    public bool AddStudent(Student student)
    {
        return _students.Add(student);
    }

    public bool RemoveStudent(Student student)
    {
        return _students.Remove(student);
    }

    protected bool Equals(StudentGroup other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((StudentGroup)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public StudentGroup()
    {
    }
}