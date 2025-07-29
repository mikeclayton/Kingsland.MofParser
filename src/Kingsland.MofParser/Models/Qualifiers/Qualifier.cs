using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Qualifiers;

[PublicAPI]
public sealed class Qualifier
{

    internal Qualifier(string name)
        : this(name, null, null)
    {
    }

    internal Qualifier(string name, string value)
        : this(name, new StringValue(value), null)
    {
    }

    internal Qualifier(string name, IQualifierValue? value = null, IEnumerable<string>? flavors = null)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Value = value;
        this.Flavors = (flavors ?? []).ToList().AsReadOnly();
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    [PublicAPI]
    public IQualifierValue? Value
    {
        get;
    }

    [PublicAPI]
    public IReadOnlyCollection<string> Flavors
    {
        get;
    }

    public override string ToString()
    {
        return $"{this.Name} = {this.Value}";
    }

}
