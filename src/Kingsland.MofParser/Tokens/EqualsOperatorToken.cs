using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record EqualsOperatorToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public EqualsOperatorToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public EqualsOperatorToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public EqualsOperatorToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? "=";
    }

    #endregion

}
