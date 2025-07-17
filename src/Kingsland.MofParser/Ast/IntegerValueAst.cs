using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1.1 Integer value
///
///     integerValue = binaryValue / octalValue / hexValue / decimalValue
///
/// </remarks>
public sealed record IntegerValueAst : LiteralValueAst, IEnumElementValueAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public IntegerLiteralToken? IntegerLiteralToken
        {
            get;
            set;
        }

        [PublicAPI]
        public IntegerValueAst Build()
        {
            return new(
                this.IntegerLiteralToken ?? throw new InvalidOperationException(
                    $"{nameof(this.IntegerLiteralToken)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal IntegerValueAst(
        IntegerKind integerKind,
        long value
    ) : this(new IntegerLiteralToken(integerKind, value))
    {
    }

    internal IntegerValueAst(
        IntegerLiteralToken integerLiteralToken
    )
    {
        this.IntegerLiteralToken = integerLiteralToken ?? throw new ArgumentNullException(nameof(integerLiteralToken));
        this.Kind = integerLiteralToken.Kind;
        this.Value = integerLiteralToken.Value;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public IntegerLiteralToken IntegerLiteralToken
    {
        get;
    }

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

    #region Converters

    public static implicit operator IntegerValueAst(int value)
    {
        return new IntegerValueAst(IntegerKind.DecimalValue, value);
    }

    #endregion

}
