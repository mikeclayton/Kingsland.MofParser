using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class EnumValueArray : EnumTypeValue
{

    [PublicAPI]
    internal EnumValueArray(params EnumValue[] values)
        : this((IEnumerable<EnumValue>)values)
    {
    }

    [PublicAPI]
    internal EnumValueArray(IEnumerable<EnumValue> values)
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<EnumValue> Values
    {
        get;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append('{');
        for (var i = 0; i < this.Values.Count; i++)
        {
            if (i > 0)
            {
                sb.Append(", ");
            }
            sb.Append(this.Values[i]);
        }
        sb.Append('}');
        return sb.ToString();
    }

}
