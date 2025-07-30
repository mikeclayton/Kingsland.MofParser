using Kingsland.MofParser.Models.Qualifiers;
using Kingsland.MofParser.Models.Values;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed class EnumElement
{

    internal EnumElement(string name)
        : this(null, name, (IEnumElementValue?)null)
    {
    }

    internal EnumElement(string name, IntegerValue value)
        : this(null, name, (IEnumElementValue?)value)
    {
    }

    internal EnumElement(IEnumerable<Qualifier>? qualifiers, string name, IntegerValue value)
        : this(qualifiers, name, (IEnumElementValue?)value)
    {
    }

    internal EnumElement(string name, StringValue value)
        : this(null, name, (IEnumElementValue?)value)
    {
    }

    internal EnumElement(IEnumerable<Qualifier>? qualifiers, string name, StringValue value)
        : this(qualifiers, name, (IEnumElementValue?)value)
    {
    }

    internal EnumElement(string name, IEnumElementValue? value)
        : this(null, name, value)
    {
    }

    internal EnumElement(IEnumerable<Qualifier>? qualifiers, string name, IEnumElementValue? value)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Value = value switch
        {
            null =>
                // value can be null if the enum's underlying type is "string",
                // in which case the value is the same as the name
                null,
            IntegerValue => value,
            StringValue => value,
            _ => throw new ArgumentException(
                $"Unsupported value type: {value.GetType().Name}. Expected {nameof(IntegerValue)} or {nameof(StringValue)}.", nameof(value)
            )
        };
    }

    [PublicAPI]
    public ReadOnlyCollection<Qualifier> Qualifiers
    {
        get;
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    [PublicAPI]
    public IEnumElementValue? Value
    {
        get;
    }

}
