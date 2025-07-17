using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record IdentifierToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public IdentifierToken(string name)
        : this(null, name)
    {
    }

    [PublicAPI]
    public IdentifierToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text), text)
    {
    }

    [PublicAPI]
    public IdentifierToken(SourceExtent? extent, string name)
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
            ?? this.Name;
    }

    #endregion

    #region Helpers

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [PublicAPI]
    public bool IsKeyword(string value)
    {
        return this.Name.Equals(
            value,
            StringComparison.InvariantCultureIgnoreCase
        );
    }

    [PublicAPI]
    public bool IsKeyword(IEnumerable<string> values)
    {
        return values.Any(
            value =>
                this.Name.Equals(
                    value,
                    StringComparison.InvariantCultureIgnoreCase
                )
        );
    }

    #endregion

    #region Converters

    public static implicit operator IdentifierToken(string name)
    {
        return new IdentifierToken(name);
    }

    #endregion

}
