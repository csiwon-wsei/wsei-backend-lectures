namespace architecture_problems.Dependency;

public class DefaultDateTimeProvider: IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}