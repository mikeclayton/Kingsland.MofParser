using Kingsland.MofParser.Models.Qualifiers;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed class Method : IStructureFeature
{

    internal Method(string returnType, string name)
        : this(null, returnType, false, name, null)
    {
    }

    internal Method(Qualifier[] qualifiers, string returnType, string name)
        : this((IEnumerable<Qualifier>?) qualifiers, returnType, false, name, null)
    {
    }

    internal Method(string returnType, string name, IEnumerable<Parameter>? parameters)
        : this(null, returnType, false, name, parameters)
    {
    }

    internal Method(string returnType, bool isArray, string name, IEnumerable<Parameter>? parameters)
        : this(null, returnType, isArray, name, parameters)
    {
    }

    internal Method(IEnumerable<Qualifier>? qualifiers, string returnType, bool isArray, string name, IEnumerable<Parameter>? parameters)
    {
        this.Qualifiers = (qualifiers ?? []).ToList().AsReadOnly();
        this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        this.IsArray = isArray;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Parameters = (parameters ?? []).ToList().AsReadOnly();
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
    public bool IsArray
    {
        get;
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    public override string ToString()
    {
        return this.Name;
    }

    [PublicAPI]
    public ReadOnlyCollection<Parameter> Parameters
    {
        get;
    }

}
