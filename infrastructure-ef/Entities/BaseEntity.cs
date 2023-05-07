namespace infrastructure_ef;
/// <summary>
/// Klasa bazowa encji, posiada klucz główny typu int
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
}