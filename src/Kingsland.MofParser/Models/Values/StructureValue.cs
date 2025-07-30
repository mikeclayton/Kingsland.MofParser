using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Parsing;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class StructureValue : Production
{

    internal StructureValue(string typeName, string alias, IEnumerable<KeyValuePair<string, PropertyValue>>? properties = null)
    {
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.Alias = alias ?? throw new ArgumentNullException(nameof(alias));
        this.Properties = (properties ?? []).ToDictionary().AsReadOnly();
    }

    [PublicAPI]
    public string TypeName
    {
        get;
    }

    [PublicAPI]
    public string Alias
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
        return $"{Constants.VALUE} {Constants.OF} {this.TypeName} {Constants.AS} {this.Alias}";
    }

}
