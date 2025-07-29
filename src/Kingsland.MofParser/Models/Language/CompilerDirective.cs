namespace Kingsland.MofParser.Models.Language;

[PublicAPI]
public sealed class CompilerDirective : IProduction
{

    internal CompilerDirective(string name, string value)
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
