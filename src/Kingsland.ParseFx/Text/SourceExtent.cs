using Kingsland.ParseFx.Attributes.StaticAnalysis;

namespace Kingsland.ParseFx.Text;

/// <summary>
/// Denotes the start position, end position and text of a section of source code.
/// </summary>
public sealed class SourceExtent
{

    #region Constructors

    public SourceExtent(string? text)
        : this(null, null, text)
    {
    }

    public SourceExtent(SourcePosition? startPosition, SourcePosition? endPosition, string? text)
    {
        this.StartPosition = startPosition;
        this.EndPosition = endPosition;
        this.Text = text;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public SourcePosition? StartPosition
    {
        get;
    }

    [PublicAPI]
    public SourcePosition? EndPosition
    {
        get;
    }

    [PublicAPI]
    public string? Text
    {
        get;
    }

    #endregion

    #region Factory Methods

    [PublicAPI]
    public static SourceExtent From(SourceChar @char)
    {
        return (@char is null )
            ? throw new ArgumentNullException(nameof(@char))
            : new SourceExtent(
                startPosition: @char.Position,
                endPosition: @char.Position,
                text: new string(@char.Value, 1)
            );
    }

    [PublicAPI]
    public static SourceExtent? From(IList<SourceChar> chars)
    {
        var text = SourceExtent.ConvertToString(chars);
        return (text.Length > 0)
            ? new SourceExtent(
                startPosition: chars.First().Position,
                endPosition: chars.Last().Position,
                text: text
            )
            : null;
    }

    [PublicAPI]
    public static string ConvertToString(IList<SourceChar> chars) {
        return (chars is null)
            ? throw new ArgumentNullException(nameof(chars))
            : new string(chars.Select(n => n.Value).ToArray());
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        var start = this.StartPosition;
        var end = this.EndPosition;
        return $"StartPosition=[{start?.Position},{start?.LineNumber},{start?.ColumnNumber}]," +
               $"EndPosition=[{end?.Position},{end?.LineNumber},{end?.ColumnNumber}]," +
               $"Text=\"{this.Text}\"";
    }

    #endregion

}
