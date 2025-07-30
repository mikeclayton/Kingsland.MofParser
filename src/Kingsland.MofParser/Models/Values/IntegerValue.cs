using System.Globalization;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class IntegerValue : LiteralValue, IEnumElementValue
{

    [PublicAPI]
    internal IntegerValue(long value)
    {
        this.Value = value;
    }

    [PublicAPI]
    public long Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value.ToString(CultureInfo.InvariantCulture);
    }

    public static implicit operator IntegerValue(int value) => new(value);

}
