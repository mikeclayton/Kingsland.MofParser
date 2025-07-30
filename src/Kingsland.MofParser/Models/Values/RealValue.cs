using System.Globalization;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class RealValue : LiteralValue
{

    [PublicAPI]
    internal RealValue(double value)
    {
        this.Value = value;
    }

    [PublicAPI]
    internal double Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value.ToString(CultureInfo.InvariantCulture);
    }

}
