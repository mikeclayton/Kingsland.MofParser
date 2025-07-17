using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record ParenthesisOpenToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public ParenthesisOpenToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public ParenthesisOpenToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public ParenthesisOpenToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? "(";
    }

    #endregion

}
