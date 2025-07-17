using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Property
{

    internal Property(string name, PropertyValue value)
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
    public PropertyValue Value
    {
        get;
    }

    public override string ToString()
    {
        return $"{this.Name} = {this.Value}";
    }

}
