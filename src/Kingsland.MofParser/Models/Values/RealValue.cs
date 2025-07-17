using System.Globalization;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class RealValue : LiteralValue
{

    [PublicAPI]
    public RealValue(double value)
    {
        this.Value = value;
    }

    [PublicAPI]
    public double Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value.ToString(CultureInfo.InvariantCulture);
    }

}
