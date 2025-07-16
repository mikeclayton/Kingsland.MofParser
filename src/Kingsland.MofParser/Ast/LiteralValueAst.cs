using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1 Primitive type value
///
///     literalValue = integerValue /
///                    realValue /
///                    booleanValue /
///                    nullValue /
///                    stringValue
///                      ; NOTE stringValue covers octetStringValue and
///                      ; dateTimeValue
///
/// </remarks>
public abstract record LiteralValueAst : PrimitiveTypeValueAst
{

    #region Constructors

    internal LiteralValueAst()
    {
    }

    #endregion

    #region Converters

    public static implicit operator LiteralValueAst(int value)
    {
        return new IntegerValueAst(IntegerKind.DecimalValue, value);
    }

    public static implicit operator LiteralValueAst(double value)
    {
        return new RealValueAst(value);
    }

    public static implicit operator LiteralValueAst(string value)
    {
        return new StringValueAst(value);
    }

    #endregion

}
