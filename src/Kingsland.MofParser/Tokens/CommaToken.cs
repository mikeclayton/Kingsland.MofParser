using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record CommaToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public CommaToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public CommaToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public CommaToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? ",";
    }

    #endregion

}
