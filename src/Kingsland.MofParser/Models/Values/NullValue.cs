using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class NullValue : LiteralValue
{

    [PublicAPI]
    public static readonly NullValue Null = new();

    private NullValue()
    {
    }

    public override string ToString()
    {
        return Constants.NULL;
    }

}
