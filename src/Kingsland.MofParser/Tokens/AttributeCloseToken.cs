using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record AttributeCloseToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public AttributeCloseToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public AttributeCloseToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public AttributeCloseToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? "]";
    }

    #endregion

}
