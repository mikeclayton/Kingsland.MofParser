using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class BooleanValue : LiteralValue
{

    [PublicAPI]
    public static readonly BooleanValue True = new(true);

    [PublicAPI]
    public static readonly BooleanValue False = new(false);

    [PublicAPI]
    public BooleanValue(bool value)
    {
        this.Value = value;
    }

    [PublicAPI]
    public bool Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value
            ? Constants.TRUE
            : Constants.FALSE;
    }

}
