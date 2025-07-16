using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record DotOperatorToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public DotOperatorToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public DotOperatorToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public DotOperatorToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? ".";
    }

    #endregion

}
