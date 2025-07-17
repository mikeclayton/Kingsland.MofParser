using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record AliasIdentifierToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public AliasIdentifierToken(string name)
        : this((SourceExtent?)null, name)
    {
    }

    [PublicAPI]
    public AliasIdentifierToken(string? text, string name)
        : this(
              text is null ? null : new SourceExtent(null, null, text),
              name
        )
    {
    }

    [PublicAPI]
    public AliasIdentifierToken(SourcePosition? start, SourcePosition? end, string text, string name)
        : this(new SourceExtent(start, end, text), name)
    {
    }

    [PublicAPI]
    public AliasIdentifierToken(SourceExtent? extent, string name)
        : base(extent)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public string Name
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? $"${this.Name}";
    }

    #endregion

    #region Converters

    [PublicAPI]
    public static implicit operator AliasIdentifierToken(string name)
    {
        return new AliasIdentifierToken(name);
    }

    #endregion

}
