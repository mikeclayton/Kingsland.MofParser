using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record NullLiteralToken : SyntaxToken
{

    public static readonly NullLiteralToken Null = new();

    #region Constructors

    [PublicAPI]
    public NullLiteralToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public NullLiteralToken(string? text)
        : this(
            text is null ? null : new SourceExtent(null, null, text)
        )
    {
    }

    [PublicAPI]
    public NullLiteralToken(SourcePosition? start, SourcePosition? end, string text)
        : this (new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public NullLiteralToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? Constants.NULL;
    }

    #endregion

}
