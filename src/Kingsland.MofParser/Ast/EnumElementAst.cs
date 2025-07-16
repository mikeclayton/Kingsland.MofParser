using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.4 Enumeration declaration
///
///     integerEnumElement     = [ qualifierList ] enumLiteral "=" integerValue
///     stringEnumElement      = [ qualifierList ] enumLiteral [ "=" stringValue ]
///
///     enumLiteral            = IDENTIFIER
///
/// </remarks>
public sealed record EnumElementAst : AstNode
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.QualifierList = new();
        }

        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        public IdentifierToken? EnumElementName
        {
            get;
            set;
        }

        public IEnumElementValueAst? EnumElementValue
        {
            get;
            set;
        }

        public EnumElementAst Build()
        {
            return new(
                this.QualifierList,
                this.EnumElementName ?? throw new InvalidOperationException(
                    $"{nameof(this.EnumElementName)} property must be set before calling {nameof(Build)}."
                ),
                this.EnumElementValue
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumElementAst(
        IdentifierToken enumElementName,
        string? enumElementValue
    ) : this(
        new QualifierListAst(),
        enumElementName,
        (enumElementValue is null) ? null : new StringValueAst(enumElementValue)
    )
    {
    }

    internal EnumElementAst(
        IdentifierToken enumElementName,
        long? enumElementValue
    ) : this(
        new QualifierListAst(),
        enumElementName,
        (enumElementValue is null) ? null : new IntegerValueAst(enumElementValue)
    )
    {
    }

    internal EnumElementAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken enumElementName,
        string? enumElementValue
    ) : this(
        new QualifierListAst(qualifierList),
        enumElementName,
        (enumElementValue is null) ? null : new StringValueAst(enumElementValue)
    )
    {
    }

    internal EnumElementAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken enumElementName,
        long? enumElementValue
    ) : this(
        new QualifierListAst(qualifierList),
        enumElementName,
        (enumElementValue is null) ? null : new IntegerValueAst(enumElementValue)
    )
    {
    }

    internal EnumElementAst(
        QualifierListAst? qualifierList,
        IdentifierToken enumElementName,
        IEnumElementValueAst? enumElementValue
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.EnumElementName = enumElementName ?? throw new ArgumentNullException(nameof(enumElementName));
        this.EnumElementValue = enumElementValue;
    }

    #endregion

    #region Properties

    public QualifierListAst? QualifierList
    {
        get;
    }

    public IdentifierToken EnumElementName
    {
        get;
    }

    public IEnumElementValueAst? EnumElementValue
    {
        get;
    }

    #endregion

}
