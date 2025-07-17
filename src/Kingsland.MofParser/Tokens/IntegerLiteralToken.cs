using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record IntegerLiteralToken : SyntaxToken
{

    #region Constructors

    [PublicAPI]
    public IntegerLiteralToken(IntegerKind kind, long value)
        : this((SourceExtent?)null, kind, value)
    {
    }

    [PublicAPI]
    public IntegerLiteralToken(string text, IntegerKind kind, long value)
        : this(null, null, text, kind, value)
    {
    }

    [PublicAPI]
    public IntegerLiteralToken(SourcePosition? start, SourcePosition? end, string text, IntegerKind kind, long value)
        : this(new SourceExtent(start, end, text), kind, value)
    {
    }

    [PublicAPI]
    public IntegerLiteralToken(SourceExtent? extent, IntegerKind kind, long value)
        : base(extent)
    {
        this.Kind = kind;
        this.Value = value;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public IntegerKind Kind
    {
        get;
    }

    [PublicAPI]
    public long Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    [PublicAPI]
    public override string GetSourceString()
    {
        return this.Text
            ?? this.Kind switch {
                IntegerKind.BinaryValue =>
                    $"{(this.Value < 0 ? "-" : "")}{Convert.ToString(Math.Abs(this.Value), 2)}b",
                IntegerKind.DecimalValue =>
                    Convert.ToString(this.Value, 10),
                IntegerKind.HexValue =>
                    $"{(this.Value < 0 ? "-" : "")}0x{Convert.ToString(Math.Abs(this.Value), 16).ToUpperInvariant()}",
                IntegerKind.OctalValue =>
                    $"{(this.Value < 0 ? "-" : "")}0{Convert.ToString(Math.Abs(this.Value), 8)}",
                _ => throw new InvalidOperationException()
            };
    }

    #endregion

    #region Converters

    public static implicit operator IntegerLiteralToken(long value)
    {
        return new IntegerLiteralToken(IntegerKind.DecimalValue, value);
    }

    #endregion

}
