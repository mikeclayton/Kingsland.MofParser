using Kingsland.MofParser.Models.Qualifiers;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public abstract class LiteralValue : PrimitiveTypeValue, IQualifierValue
{

    internal LiteralValue()
    {
    }

    public static implicit operator LiteralValue(int value) => new IntegerValue(value);
    public static implicit operator LiteralValue(double value) => new RealValue(value);
    public static implicit operator LiteralValue(bool value) => new BooleanValue(value);
    public static implicit operator LiteralValue(string value) => new StringValue(value);

}
