using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record CommentToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public CommentToken(string value)
        : this(null, value)
    {
    }

    [PublicAPI]
    public CommentToken(SourcePosition? start, SourcePosition? end, string text)
         : this(new SourceExtent(start, end, text), text)
    {
    }

    [PublicAPI]
    public CommentToken(SourceExtent? extent, string value)
        : base(extent)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public string Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? this.Value;
    }

    #endregion

}
