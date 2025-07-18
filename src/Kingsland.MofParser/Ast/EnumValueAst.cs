﻿using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.3 Enum type value
///
///     enumValue   = [ enumName "." ] enumLiteral
///
///     enumLiteral = IDENTIFIER
///
/// 7.5.4 Enumeration declaration
///
///     enumName    = elementName
///
/// </remarks>
public sealed record EnumValueAst : EnumTypeValueAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public IdentifierToken? EnumName
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? EnumLiteral
        {
            get;
            set;
        }

        [PublicAPI]
        public EnumValueAst Build()
        {
            return new(
                this.EnumName,
                this.EnumLiteral ?? throw new InvalidOperationException(
                    $"{nameof(this.EnumLiteral)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumValueAst(
        IdentifierToken enumLiteral
    ) : this(null, enumLiteral)
    {
    }

    internal EnumValueAst(
        IdentifierToken? enumName,
        IdentifierToken enumLiteral
    )
    {
        this.EnumName = enumName;
        this.EnumLiteral = enumLiteral ?? throw new ArgumentNullException(nameof(enumLiteral));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public IdentifierToken? EnumName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken EnumLiteral
    {
        get;
    }

    #endregion


    #region Converters

    public static implicit operator EnumValueAst(string enumName)
    {
        return new EnumValueAst((IdentifierToken)enumName);
    }

    #endregion

}
