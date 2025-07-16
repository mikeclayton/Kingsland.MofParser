using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record PragmaToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public PragmaToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public PragmaToken(string? text)
        : this(
            text is null ? null : new SourceExtent(null, null, text)
        )
    {
    }

    [PublicAPI]
    public PragmaToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public PragmaToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? Constants.PRAGMA;
    }

    #endregion

    #region Converters

    public static implicit operator PragmaToken(string text)
    {
        return new PragmaToken(text);
    }

    #endregion

}
