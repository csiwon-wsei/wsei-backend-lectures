using System.Xml.Serialization;
using Newtonsoft.Json;

namespace core.Models;
[XmlRoot]
public class Student
{
    public Student(int id, string firstName, string lastName, string phone, StudentGroup studentGroup, DateOnly birth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        StudentGroup = studentGroup;
        Birth = birth;
    }

    public Student()
    {
    }

    public int Id { get; init; }
   
    public string FirstName { get; init; }
    
    public string LastName { get; init; }

    public string Phone { get; init; }
    
    public StudentGroup StudentGroup { get; init; }
    
    public DateOnly Birth { get; init; }

    public string GetFullName()
    {
        return $"{FirstName}, {LastName}";
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Phone)}: {Phone}, {nameof(Birth)}: {Birth}";
    }

    public Student WithGroup(StudentGroup group)
    {
        return new Student(id: Id, firstName: FirstName, lastName: LastName, studentGroup: group, phone: Phone, birth: Birth);
    }

    protected bool Equals(Student other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Student)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

