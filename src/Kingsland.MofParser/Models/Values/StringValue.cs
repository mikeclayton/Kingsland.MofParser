using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class StringValue : LiteralValue
{

    [PublicAPI]
    public StringValue(string value)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    [PublicAPI]
    public string Value
    {
        get;
    }

    public override string ToString()
    {
        return $"\"{StringLiteralToken.EscapeString(this.Value)}\"";
    }

}
