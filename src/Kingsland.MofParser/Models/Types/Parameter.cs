using Kingsland.MofParser.Models.Values;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Parameter
{

    internal Parameter(Qualifier[] qualifiers, string type, string name)
        : this((IEnumerable<Qualifier>?) qualifiers, type, false, name, false, null)
    {
    }

    internal Parameter(Qualifier[] qualifiers, string type, string name, PropertyValue? defaultValue)
        : this((IEnumerable<Qualifier>?)qualifiers, type, false, name, false, defaultValue)
    {
    }

    internal Parameter(string type, string name)
        : this(null, type, false, name, false, null)
    {
    }

    internal Parameter(string type, string name, PropertyValue? defaultValue)
        : this(null, type, false, name, false, defaultValue)
    {
    }

    internal Parameter(string type, bool isRef, string name)
        : this(null, type, isRef, name, false, null)
    {
    }

    internal Parameter(string type, bool isRef, string name, PropertyValue? defaultValue)
        : this(null, type, isRef, name, false, defaultValue)
    {
    }

    internal Parameter(string type, string name, bool isArray)
        : this(null, type, false, name, isArray, null)
    {
    }

    internal Parameter(IEnumerable<Qualifier>? qualifiers, string type, bool isRef, string name, bool isArray, PropertyValue? defaultValue)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.Type = type ?? throw new ArgumentNullException(nameof(type));
        this.IsRef = isRef;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.IsArray = isArray;
        this.DefaultValue = defaultValue;
    }

    [PublicAPI]
    public ReadOnlyCollection<Qualifier> Qualifiers
    {
        get;
    }

    [PublicAPI]
    public string Type
    {
        get;
    }

    [PublicAPI]
    public bool IsRef
    {
        get;
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    [PublicAPI]
    public bool IsArray
    {
        get;
    }

    [PublicAPI]
    public PropertyValue? DefaultValue
    {
        get;
    }

    public override string ToString()
    {
        return $"{this.Name} = {this.DefaultValue}";
    }

}
