using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Models.Qualifiers;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed class Class : IProduction
{

    internal Class(Qualifier[] qualifiers, string name)
        : this(qualifiers, name, null, null)
    {
    }

    internal Class(string name)
        : this(null, name, null, null)
    {
    }

    internal Class(Qualifier[] qualifiers, string name, string? superClass)
        : this(qualifiers, name, superClass, null)
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

    public IEnumerable<Property> GetProperties()
    {
        foreach (var feature in this.Features)
        {
            if (feature is Property property)
            {
                yield return property;
            }
        }
    }

}
