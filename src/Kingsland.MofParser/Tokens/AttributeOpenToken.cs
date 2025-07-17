using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record AttributeOpenToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public AttributeOpenToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public AttributeOpenToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public AttributeOpenToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? "[";
    }

    #endregion

}
