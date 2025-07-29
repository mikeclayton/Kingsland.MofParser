using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Models.Qualifiers;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed class Enumeration : IProduction, IStructureFeature
{

    [PublicAPI]
    internal Enumeration(string name, string underlyingType)
        : this(null, name, underlyingType, null)
    {
    }

    [PublicAPI]
    internal Enumeration(Qualifier[] qualifiers, string name, string underlyingType)
        : this(qualifiers, name, underlyingType, null)
    {
    }

    [PublicAPI]
    internal Enumeration(string name, string underlyingType, IEnumerable<EnumElement>? elements)
        : this([], name, underlyingType, elements)
    {
    }

    [PublicAPI]
    internal Enumeration(IEnumerable<Qualifier>? qualifiers, string name, string underlyingType, IEnumerable<EnumElement>? elements)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.UnderlyingType = underlyingType ?? throw new ArgumentNullException(nameof(underlyingType));
        this.Elements = (elements ?? []).ToList().AsReadOnly();
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
    public string UnderlyingType
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<EnumElement> Elements
    {
        get;
    }

}
