using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record ParenthesisCloseToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public ParenthesisCloseToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public ParenthesisCloseToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public ParenthesisCloseToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? ")";
    }

    #endregion

}
