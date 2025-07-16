using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.5 Property declaration
///
///     propertyDeclaration          = [ qualifierList ] ( primitivePropertyDeclaration /
///                                    complexPropertyDeclaration /
///                                    enumPropertyDeclaration /
///                                    referencePropertyDeclaration) ";"
///
///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
///                                    [ "=" primitiveTypeValue]
///
///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
///
///     enumPropertyDeclaration      = enumName propertyName [ array ]
///                                    [ "=" enumTypeValue]
///
///     referencePropertyDeclaration = classReference propertyName [ array ]
///                                    [ "=" referenceTypeValue ]
///
///     array                        = "[" "]"
///     propertyName                 = IDENTIFIER
///     structureOrClassName         = IDENTIFIER
///
///     classReference               = DT_REFERENCE
///     DT_REFERENCE                 = className REF
///     REF                          = "ref" ; keyword: case insensitive
///
/// </remarks>
public sealed record PropertyDeclarationAst : AstNode, IStructureFeatureAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
        }

        [PublicAPI]
        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? ReturnType
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? ReturnTypeRef
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken?PropertyName
        {
            get;
            set;
        }

        [PublicAPI]
        public bool ReturnTypeIsArray
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertyValueAst? Initializer
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertyDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.ReturnType ?? throw new InvalidOperationException(
                    $"{nameof(this.ReturnType)} property must be set before calling {nameof(Build)}."
                ),
                this.ReturnTypeRef,
                this.PropertyName ?? throw new InvalidOperationException(
                    $"{nameof(this.PropertyName)} property must be set before calling {nameof(Build)}."
                ),
                this.ReturnTypeIsArray,
                this.Initializer
            );
        }

    }

    #endregion

    #region Constructors

    internal PropertyDeclarationAst(
        IdentifierToken returnType,
        IdentifierToken propertyName,
        PropertyValueAst? initializer = null
    ) : this(null, returnType, null, propertyName, null, initializer)
    {
    }

    internal PropertyDeclarationAst(
        IdentifierToken returnType,
        IdentifierToken returnTypeRef,
        IdentifierToken propertyName
    ) : this(null, returnType, returnTypeRef, propertyName, null, null)
    {
    }

    internal PropertyDeclarationAst(
        IdentifierToken returnType,
        IdentifierToken propertyName,
        bool returnTypeIsArray
    ) : this(null, returnType, null, propertyName, returnTypeIsArray, null)
    {
    }

    internal PropertyDeclarationAst(
        IdentifierToken returnType,
        IdentifierToken returnTypeRef,
        IdentifierToken propertyName,
        PropertyValueAst initializer
    ) : this(null, returnType, returnTypeRef, propertyName, null, initializer)
    {
    }

    internal PropertyDeclarationAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken returnType,
        IdentifierToken propertyName
    ) : this(new(qualifierList), returnType, null, propertyName, null, null)
    {
    }

    internal PropertyDeclarationAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken returnType,
        IdentifierToken propertyName,
        bool returnTypeIsArray
    ) : this(new(qualifierList), returnType, null, propertyName, returnTypeIsArray, null)
    {
    }

    internal PropertyDeclarationAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken returnType,
        IdentifierToken propertyName,
        PropertyValueAst initializer
    ) : this(new(qualifierList), returnType, null, propertyName, null, initializer)
    {
    }

    internal PropertyDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken returnType,
        IdentifierToken? returnTypeRef,
        IdentifierToken propertyName,
        bool? returnTypeIsArray,
        PropertyValueAst? initializer = null
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        this.ReturnTypeRef = returnTypeRef;
        this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        this.ReturnTypeIsArray = returnTypeIsArray ?? false;
        this.Initializer = initializer;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken ReturnType
    {
        get;
    }

    [PublicAPI]
    public bool ReturnTypeIsRef =>
        this.ReturnTypeRef is not null;

    [PublicAPI]
    public IdentifierToken? ReturnTypeRef
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken PropertyName
    {
        get;
    }

    [PublicAPI]
    public bool ReturnTypeIsArray
    {
        get;
    }

    [PublicAPI]
    public PropertyValueAst? Initializer
    {
        get;
    }

    #endregion

}
