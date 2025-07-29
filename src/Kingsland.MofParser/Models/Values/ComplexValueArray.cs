using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class ComplexValueArray : ComplexTypeValue
{

    [PublicAPI]
    internal ComplexValueArray(params ComplexValue[] values)
        : this((IEnumerable<ComplexValue>)values)
    {
    }

    [PublicAPI]
    internal ComplexValueArray(IEnumerable<ComplexValue> values)
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<ComplexValue> Values
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
