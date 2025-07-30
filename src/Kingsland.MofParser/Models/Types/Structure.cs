using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Models.Qualifiers;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed class Structure : Production, IStructureFeature
{

    internal Structure(string name)
        : this(null, name, null, null)
    {
    }

    internal Structure(string name, string? superStructure)
        : this(null, name, superStructure, null)
    {
    }

    internal Structure(string name, IEnumerable<IStructureFeature>? features)
        : this(null, name, null, features)
    {
    }

    internal Structure(string name, string? superStructure, IEnumerable<IStructureFeature>? features)
        : this(null, name, superStructure, features)
    {
    }

    [PublicAPI]
    public Structure(IEnumerable<Qualifier>? qualifiers, string name, string? superStructure, IEnumerable<IStructureFeature>? features)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.SuperStructure = superStructure;
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
    public string? SuperStructure
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<IStructureFeature> Features
    {
        get;
    }

}
