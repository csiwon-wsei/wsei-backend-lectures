namespace architecture_problems.Record;

public record User(int Id, string Username, string Password)
{
    public virtual bool Equals(User? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}
