using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record ColonToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public ColonToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public ColonToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public ColonToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? ":";
    }

    #endregion

}
