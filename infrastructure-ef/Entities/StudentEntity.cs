using core.Domain;

namespace infrastructure_ef.Entities;
/// <summary>
/// Klasa encji dla obiektu domenowego Student.
/// Dziedziczy po klasie BaseEntity, w której jest zdefiniowany klucz główny
/// </summary>
public class StudentEntity:BaseEntity
{
    public Address ContactAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public DateOnly Birth { get; set; }
    /// <summary>
    /// Pole nawigacyjne związku z klasą encji dla grupy studenckiej.
    /// Ładowanie pola odbywa się w sposób leniwy (słowo virtual) 
    /// </summary>
    public virtual StudentGroupEntity? StudentGroupEntity { get; set; }

    /// <summary>
    /// Metoda mapująca encję na obiekt domentowy, można zastosować gotowy mapper lub zdefiniować metody w osobnych klasach.
    /// </summary>
    /// <returns>klasa Student - obiekt domentowy</returns>
    public Student To()
    {
        return new Student()
        {
            Id = Id,
            Birth = Birth,
            LastName = LastName,
            FirstName = FirstName,
            Phone = Phone,
            StudentGroup = StudentGroupEntity?.ToWithoutStudents(),
        };
    }
    /// <summary>
    /// Ta metoda także mapuje na obiekt domenowy, ale bez pola nawigacyjnego z grupą studencką
    /// </summary>
    /// <returns></returns>
    public Student ToWithoutGroup()
    {
        return new Student()
        {
            Id = Id,
            Birth = Birth,
            LastName = LastName,
            FirstName = FirstName,
            Phone = Phone,
            StudentGroup = null
        };
    }
}