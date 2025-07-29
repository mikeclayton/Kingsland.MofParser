using Kingsland.MofParser.Models.Qualifiers;
using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class LiteralValueArray : PrimitiveTypeValue, IQualifierValue
{

    [PublicAPI]
    internal LiteralValueArray(params LiteralValue[] values)
        : this((IEnumerable<LiteralValue>)values)
    {
    }

    [PublicAPI]
    internal LiteralValueArray(IEnumerable<LiteralValue> values)
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<LiteralValue> Values
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
