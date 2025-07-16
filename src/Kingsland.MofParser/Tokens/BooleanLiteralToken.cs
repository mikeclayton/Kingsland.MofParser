using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record BooleanLiteralToken : SyntaxToken
{

    [PublicAPI]
    public static readonly BooleanLiteralToken True = new(true);

    [PublicAPI]
    public static readonly BooleanLiteralToken False = new(false);


    #region Constructors

    [PublicAPI]
    public BooleanLiteralToken(bool value)
        : this((SourceExtent?)null, value)
    {
    }

    [PublicAPI]
    public BooleanLiteralToken(string text, bool value)
        : this(null, null, text, value)
    {
    }

    [PublicAPI]
    public BooleanLiteralToken(SourcePosition? start, SourcePosition? end, string text, bool value)
        : this(new SourceExtent(start, end, text), value)
    {
    }

    [PublicAPI]
    public BooleanLiteralToken(SourceExtent? extent, bool value)
        : base(extent)
    {
        this.Value = value;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public bool Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? (this.Value ? Constants.TRUE : Constants.FALSE);
    }

    #endregion

}
