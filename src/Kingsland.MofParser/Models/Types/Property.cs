using Kingsland.MofParser.Models.Values;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Property : IStructureFeature
{

    internal Property(Qualifier[] qualifiers, string returnType, string name)
        : this((IEnumerable<Qualifier>?) qualifiers, returnType, false, name, false, null)
    {
    }

    internal Property(Qualifier[] qualifiers, string returnType, string name, PropertyValue? defaultValue)
        : this((IEnumerable<Qualifier>?)qualifiers, returnType, false, name, false, defaultValue)
    {
    }

    internal Property(string returnType, string name)
        : this(null, returnType, false, name, false, null)
    {
    }

    internal Property(string returnType, string name, PropertyValue? defaultValue)
        : this(null, returnType, false, name, false, defaultValue)
    {
    }

    internal Property(string returnType, bool isRef, string name)
        : this(null, returnType, isRef, name, false, null)
    {
    }

    internal Property(string returnType, bool isRef, string name, PropertyValue? defaultValue)
        : this(null, returnType, isRef, name, false, defaultValue)
    {
    }

    internal Property(Qualifier[] qualifiers, string returnType, string name, bool isArray)
        : this((IEnumerable<Qualifier>?)qualifiers, returnType, false, name, isArray, null)
    {
    }

    internal Property(string returnType, string name, bool isArray)
        : this(null, returnType, false, name, isArray, null)
    {
    }

    internal Property(IEnumerable<Qualifier>? qualifiers, string returnType, bool isRef, string name, bool isArray, PropertyValue? defaultValue)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
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
    public string ReturnType
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
