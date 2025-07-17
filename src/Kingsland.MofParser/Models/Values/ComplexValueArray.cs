using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class ComplexValueArray : ComplexTypeValue
{

    [PublicAPI]
    public ComplexValueArray(params ComplexValueBase[] values)
        : this((IEnumerable<ComplexValueBase>)values)
    {
    }

    [PublicAPI]
    public ComplexValueArray(IEnumerable<ComplexValueBase> values)
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<ComplexValueBase> Values
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
