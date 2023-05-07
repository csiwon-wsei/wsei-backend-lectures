using core.Domain;

namespace infrastructure_ef.Entities;
/// <summary>
/// Klasa encji dla grupy studenckiej, także dziedzicyz po klasie bazowej encji
/// </summary>
public class StudentGroupEntity: BaseEntity
{
    public string Name { get; set; }
    /// <summary>
    /// Pole nawigacyjne z członkami grupy studenckiej.
    /// Pole także jest ładowane leniwie (słowo virtual).
    /// </summary>
    public virtual ISet<StudentEntity> Students { get; set; }

    public StudentGroupEntity()
    {
        Students = new HashSet<StudentEntity>();
    }
    /// <summary>
    /// Metoda mapuje encję na klasę domenową. Mapuje także każdą encję studenta na klasę domenową
    /// </summary>
    /// <returns></returns>
    public StudentGroup To()
    {
        return new StudentGroup(this.Id, this.Name, this.Students.Select(s => s.To()).ToHashSet());
    }
    /// <summary>
    /// Metoda mapuje encję grupy na klasę domenową, ale każdy ze studentów - członków grupy, mapowany jest na obiekt z grupą równą null. 
    /// </summary>
    /// <returns></returns>
    public StudentGroup ToWithoutStudents()
    {
        return new StudentGroup(this.Id, this.Name, this.Students.Select(s => s.ToWithoutGroup()).ToHashSet());
    }
}