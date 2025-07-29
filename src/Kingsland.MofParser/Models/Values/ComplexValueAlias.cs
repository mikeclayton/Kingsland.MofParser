namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class ComplexValueAlias : ComplexValueBase
{

    [PublicAPI]
    internal ComplexValueAlias(string name)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    [PublicAPI]
    public override string ToString()
    {
        return $"${this.Name}";
    }

}
