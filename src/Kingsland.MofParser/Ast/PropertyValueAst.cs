using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.9 Complex type value
///
///     propertyValue  = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
///
/// </remarks>
public abstract record PropertyValueAst : IAstNode
{

    #region Constructors

    internal PropertyValueAst()
    {
    }

    #endregion

    #region Converters

    public static implicit operator PropertyValueAst(AliasIdentifierToken aliasIdentifier)
    {
        return new ComplexValueAst(aliasIdentifier);
    }

    public static implicit operator PropertyValueAst(bool value)
    {
        return new BooleanValueAst(value);
    }

    public static implicit operator PropertyValueAst(BooleanLiteralToken booleanLiteral)
    {
        return new BooleanValueAst(booleanLiteral);
    }

    public static implicit operator PropertyValueAst(long value)
    {
        return new IntegerValueAst(value);
    }

    public static implicit operator PropertyValueAst(IntegerLiteralToken integerLiteral)
    {
        return new IntegerValueAst(integerLiteral);
    }

    public static implicit operator PropertyValueAst(NullLiteralToken nullLiteral)
    {
        return new NullValueAst(nullLiteral);
    }

    public static implicit operator PropertyValueAst(StringLiteralToken stringLiteral)
    {
        return new StringValueAst(stringLiteral);
    }

    #endregion

}
