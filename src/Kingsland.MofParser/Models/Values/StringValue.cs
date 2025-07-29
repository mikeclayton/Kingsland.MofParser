using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class StringValue : LiteralValue, IEnumElementValue
{

    [PublicAPI]
    internal StringValue(params string[] values)
    {
        this.Values = values.ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<string> Values
    {
        get;
    }

    public string Value =>
        string.Join(string.Empty, this.Values);

    public override string ToString()
    {
        return string.Join(
            string.Empty,
            this.Values.Select(s => $"\"{StringLiteralToken.EscapeString(s)}\"")
        );
    }

    public static implicit operator StringValue(string value) => new(value);

}
