using Kingsland.MofParser.Parsing;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class ComplexObjectValue : ComplexValue
{

    internal ComplexObjectValue(string typeName, IEnumerable<KeyValuePair<string, PropertyValue>>? properties = null)
    {
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.Properties = (properties ?? []).ToDictionary().AsReadOnly();
    }

    [PublicAPI]
    public string TypeName
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
        return $"{Constants.VALUE} {Constants.OF} {this.TypeName}";
    }

}
