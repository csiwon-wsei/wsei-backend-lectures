namespace architecture_problems.Dependency;

public class TestDateTimeProvider: IDateTimeProvider
{
    private readonly DateTime now;

    public TestDateTimeProvider(DateTime now)
    {
        this.now = now;
    }

    public DateTime Now => now;
}