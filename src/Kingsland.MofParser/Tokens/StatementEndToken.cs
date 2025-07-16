using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record StatementEndToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public StatementEndToken()
        : this((SourceExtent?)null)
    {
    }

    [PublicAPI]
    public StatementEndToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    [PublicAPI]
    public StatementEndToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? ";";
    }

    #endregion

    #region Converters

    [PublicAPI]
    public static implicit operator StatementEndToken(string text)
    {
        if (text is not ";")
        {
            throw new ArgumentException(null, nameof(text));
        }
        return new StatementEndToken();
    }

    #endregion

}
