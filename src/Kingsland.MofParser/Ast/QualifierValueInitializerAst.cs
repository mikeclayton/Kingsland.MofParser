﻿using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.4.1 QualifierList
///
///     qualifierValueInitializer     = "(" literalValue ")"
///
/// </remarks>
public sealed record QualifierValueInitializerAst : IQualifierInitializerAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public LiteralValueAst? Value
        {
            get;
            set;
        }

        [PublicAPI]
        public QualifierValueInitializerAst Build()
        {
            return new(
                this.Value ?? throw new InvalidOperationException(
                    $"{nameof(this.Value)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierValueInitializerAst(LiteralValueAst value)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    internal QualifierValueInitializerAst(IntegerKind kind, long value)
    {
        this.Value = new IntegerValueAst(
            new IntegerLiteralToken(kind, value)
        );
    }

    internal QualifierValueInitializerAst(params string[] values)
    {
        this.Value = new StringValueAst(values);
    }

    #endregion

    #region Properties

    [PublicAPI]
    public LiteralValueAst Value
    {
        get;
    }

    #endregion

}
