using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Class : IProduction
{

    internal Class(Qualifier[] qualifiers, string name)
        : this((IEnumerable<Qualifier>?)qualifiers, name, null, null)
    {
    }

    internal Class(string name)
        : this(null, name, null, null)
    {
    }

    internal Class(Qualifier[] qualifiers, string name, string? superClass)
        : this((IEnumerable<Qualifier>?)qualifiers, name, superClass, null)
    {
    }

    internal Class(string name, string? superClass)
        : this(null, name, superClass, null)
    {
    }

    internal Class(string name, IEnumerable<IClassFeature>? features)
        : this(null, name, null, features)
    {
    }

    internal Class(string name, string? superClass, IEnumerable<IClassFeature>? features)
        : this(null, name, superClass, features)
    {
    }

    internal Class(IEnumerable<Qualifier>? qualifiers, string name, string? superClass, IEnumerable<IClassFeature>? features)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.SuperClass = superClass;
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
    public string? SuperClass
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<IClassFeature> Features
    {
        get;
    }

}
