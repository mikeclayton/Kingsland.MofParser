namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Pragma : IProduction
{

    internal Pragma(string name, string value)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    [PublicAPI]
    public string Value
    {
        get;
    }

}
