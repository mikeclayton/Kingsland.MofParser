using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Association : IProduction
{

    internal Association(Qualifier[] qualifiers, string name)
        : this((IEnumerable<Qualifier>?)qualifiers, name, null, null)
    {
    }

    internal Association(Qualifier[] qualifiers, string name, string? superAssociation)
        : this((IEnumerable<Qualifier>?)qualifiers, name, superAssociation, null)
    {
    }

    internal Association(string name)
        : this(null, name, null, null)
    {
    }

    internal Association(string name, string? superAssociation)
        : this(null, name, superAssociation, null)
    {
    }

    internal Association(string name, IEnumerable<IClassFeature>? features)
        : this(null, name, null, features)
    {
    }

    internal Association(string name, string? superAssociation, IEnumerable<IClassFeature>? features)
        : this(null, name, superAssociation, features)
    {
    }

    internal Association(IEnumerable<Qualifier>? qualifiers, string name, string? superAssociation, IEnumerable<IClassFeature>? features)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.SuperAssociation = superAssociation;
        this.Features = (features ?? []).ToList().AsReadOnly();
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
    public string? SuperAssociation
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<IClassFeature> Features
    {
        get;
    }

}
