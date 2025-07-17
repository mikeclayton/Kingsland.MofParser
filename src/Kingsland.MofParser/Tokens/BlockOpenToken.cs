using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record BlockOpenToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public BlockOpenToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public BlockOpenToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public BlockOpenToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? "{";
    }

    #endregion

}
