namespace core.Generators;

public class IntGenerator
{
    private int _value;

    public int Next => ++_value;

    public int Current => _value;
    
    public IntGenerator(int value = 0) => _value = value;
}