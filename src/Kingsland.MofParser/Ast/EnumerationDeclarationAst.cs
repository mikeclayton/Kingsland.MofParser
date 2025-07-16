using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.4 Enumeration declaration
///
///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
///
///     enumTypeHeader         = [ qualifierList ] ENUMERATION
///
///     enumName               = elementName
///
///     enumTypeDeclaration    = ( DT_INTEGER / integerEnumName ) integerEnumDeclaration /
///                              ( DT_STRING / stringEnumName ) stringEnumDeclaration
///
///     integerEnumName        = enumName
///     stringEnumName         = enumName
///
///     integerEnumDeclaration = "{" [ integerEnumElement *( "," integerEnumElement ) ] "}"
///     stringEnumDeclaration  = "{" [ stringEnumElement *( "," stringEnumElement ) ] "}"
///
///     integerEnumElement     = [ qualifierList ] enumLiteral "=" integerValue
///     stringEnumElement      = [ qualifierList ] enumLiteral [ "=" stringValue ]
///
///     enumLiteral            = IDENTIFIER
///
///     ENUMERATION            = "enumeration" ; keyword: case insensitive
///
/// </remarks>
public sealed record EnumerationDeclarationAst : MofProductionAst, IStructureFeatureAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
            this.EnumElements = [];
        }

        [PublicAPI]
        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? EnumName
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? EnumType
        {
            get;
            set;
        }

        [PublicAPI]
        public List<EnumElementAst> EnumElements
        {
            get;
            set;
        }

        [PublicAPI]
        public EnumerationDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.EnumName ?? throw new InvalidOperationException(
                    $"{nameof(this.EnumName)} property must be set before calling {nameof(Build)}."
                ),
                this.EnumType ?? throw new InvalidOperationException(
                    $"{nameof(this.EnumType)} property must be set before calling {nameof(Build)}."
                ),
                this.EnumElements
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumerationDeclarationAst(
        IdentifierToken enumName,
        IdentifierToken enumType,
        IEnumerable<string>? enumElements
    ) : this(
        null,
        enumName,
        enumType,
        (enumElements ?? throw new ArgumentNullException(nameof(enumElements)))
            .Select(enumElement => new EnumElementAst(enumElement, (string?)null))
    )
    {
    }

    internal EnumerationDeclarationAst(
        IdentifierToken enumName,
        IdentifierToken enumType,
        IEnumerable<EnumElementAst>? enumElements = null
    ) : this(null, enumName, enumType, enumElements)
    {
    }

    internal EnumerationDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken enumName,
        IdentifierToken enumType,
        IEnumerable<EnumElementAst>? enumElements = null
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.EnumName = enumName ?? throw new ArgumentNullException(nameof(enumName));
        this.EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
        this.EnumElements = (enumElements ?? []).ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken EnumName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken EnumType
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<EnumElementAst> EnumElements
    {
        get;
    }

    #endregion

}
