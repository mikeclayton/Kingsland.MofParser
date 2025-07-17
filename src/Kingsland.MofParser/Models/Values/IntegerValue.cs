using System.Globalization;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class IntegerValue : LiteralValue
{

    [PublicAPI]
    public IntegerValue(long value)
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

}
