using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record StringLiteralToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public StringLiteralToken(string value)
        : this(null, value)
    {
    }

    [PublicAPI]
    public StringLiteralToken(SourcePosition? start, SourcePosition end, string text, string value)
        : this(new SourceExtent(start, end, text), value)
    {
    }

    [PublicAPI]
    public StringLiteralToken(SourceExtent? extent, string value)
        : base(extent)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public string Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? $"\"{StringLiteralToken.EscapeString(this.Value)}\"";
    }

    #endregion

    #region Helpers

    private static readonly Dictionary<char, string> EscapeMap = new()
        {
            { '\\', $"{Constants.BACKSLASH}{Constants.BACKSLASH}" },
            { '\"', $"{Constants.BACKSLASH}{Constants.DOUBLEQUOTE}" },
            { '\'', $"{Constants.BACKSLASH}{Constants.SINGLEQUOTE}" },
            { '\b', $"{Constants.BACKSLASH}{Constants.BACKSPACE_ESC}" },
            { '\t', $"{Constants.BACKSLASH}{Constants.TAB_ESC}" },
            { '\n', $"{Constants.BACKSLASH}{Constants.LINEFEED_ESC}" },
            { '\f', $"{Constants.BACKSLASH}{Constants.FORMFEED_ESC}" },
            { '\r', $"{Constants.BACKSLASH}{Constants.CARRIAGERETURN_ESC}" }
        };

    [PublicAPI]
    public static string EscapeString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        var escapedString = new StringBuilder();
        foreach (var @char in value.ToCharArray())
        {
            if (StringLiteralToken.EscapeMap.TryGetValue(@char, out var escapedChar))
            {
                // escape sequence
                escapedString.Append(escapedChar);
            }
            else if ((@char >= 32) && (@char <= 126))
            {
                // printable characters ' ' - '~'
                escapedString.Append(@char);
            }
            else
            {
                throw new InvalidOperationException(new string(new [] { @char }));
            }
        }
        return escapedString.ToString();
    }

    #endregion

    #region Converters

    public static implicit operator StringLiteralToken(string value)
    {
        return new StringLiteralToken(value);
    }

    #endregion

}
