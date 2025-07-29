using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Parsing;
using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class ComplexValueObject : ComplexValueBase, IProduction
{

    internal ComplexValueObject(string typeName, IEnumerable<KeyValuePair<string, PropertyValue>>? properties = null)
        : this(typeName, null, properties)
    {
    }

    internal ComplexValueObject(string typeName, string? alias, IEnumerable<KeyValuePair<string, PropertyValue>>? properties = null)
    {
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.Alias = alias;
        this.Properties = (properties ?? []).ToDictionary().AsReadOnly();
    }

    [PublicAPI]
    public string TypeName
    {
        get;
    }

    [PublicAPI]
    public string? Alias
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyDictionary<string, PropertyValue> Properties
    {
        get;
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        result.Append($"{Constants.VALUE} {Constants.OF} {this.TypeName}");
        return result.ToString();
    }

}
