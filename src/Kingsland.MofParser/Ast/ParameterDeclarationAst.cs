using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.7 Parameter declaration
///
///     parameterDeclaration      = [ qualifierList ] ( primitiveParamDeclaration /
///                                 complexParamDeclaration /
///                                 enumParamDeclaration /
///                                 referenceParamDeclaration )
///
///     primitiveParamDeclaration = primitiveType parameterName [ array ]
///                                 [ "=" primitiveTypeValue ]
///
///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
///
///     enumParamDeclaration      = enumName parameterName [ array ]
///                                 [ "=" enumValue ]
///
///     referenceParamDeclaration = classReference parameterName [ array ]
///                                 [ "=" referenceTypeValue ]
///
///     parameterName             = IDENTIFIER
///
/// 7.5.6 Method declaration
///
///     classReference            = DT_REFERENCE
///
/// 7.5.10 Reference type declaration
///
///     DT_REFERENCE              = className REF
///
/// </remarks>
public sealed record ParameterDeclarationAst : AstNode
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
        public IdentifierToken? ParameterType
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? ParameterRef
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? ParameterName
        {
            get;
            set;
        }

        [PublicAPI]
        public bool ParameterIsArray
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertyValueAst? DefaultValue
        {
            get;
            set;
        }

        [PublicAPI]
        public ParameterDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.ParameterType ?? throw new InvalidOperationException(
                    $"{nameof(this.ParameterType)} property must be set before calling {nameof(Build)}."
                ),
                this.ParameterRef,
                this.ParameterName ?? throw new InvalidOperationException(
                    $"{nameof(this.ParameterName)} property must be set before calling {nameof(Build)}."
                ),
                this.ParameterIsArray,
                this.DefaultValue
            );
        }

    }

    #endregion

    #region Constructors

    internal ParameterDeclarationAst(
        IdentifierToken parameterType,
        IdentifierToken parameterName
    ) : this(null, parameterType, null, parameterName, false, null)
    {
        // Integer MyMethod(GOLF_ClubMember lateMembers)
    }

    internal ParameterDeclarationAst(
        IdentifierToken parameterType,
        IdentifierToken parameterName,
        PropertyValueAst defaultValue
    ) : this(null, parameterType, null, parameterName, false, defaultValue)
    {
        // Integer MyMethod(GOLF_ClubMember REF lateMembers = 1)
    }

    internal ParameterDeclarationAst(
        IdentifierToken parameterType,
        IdentifierToken? parameterRef,
        IdentifierToken parameterName
    ) : this(null, parameterType, parameterRef, parameterName, false, null)
    {
        // Integer MyMethod(GOLF_ClubMember REF lateMembers)
    }

    internal ParameterDeclarationAst(
        IdentifierToken parameterType,
        IdentifierToken? parameterRef,
        IdentifierToken parameterName,
        PropertyValueAst? defaultValue
    ) : this(null, parameterType, parameterRef, parameterName, false, defaultValue)
    {
        // Integer MyMethod(GOLF_ClubMember REF lateMembers = 1)
    }

    internal ParameterDeclarationAst(
        IdentifierToken parameterType,
        IdentifierToken parameterName,
        bool parameterIsArray = false,
        PropertyValueAst? defaultValue = null
    ) : this(null, parameterType, null, parameterName, parameterIsArray , defaultValue)
    {
        // Integer MyMethod(GOLF_ClubMember lateMembers[] = 1)
    }

    internal ParameterDeclarationAst(
        IdentifierToken parameterType,
        IdentifierToken? parameterRef,
        IdentifierToken parameterName,
        bool parameterIsArray = false,
        PropertyValueAst? defaultValue = null
    ) : this(null, parameterType, parameterRef, parameterName, parameterIsArray, defaultValue)
    {
        // Integer MyMethod(GOLF_ClubMember REF lateMembers[] = 1)
    }

    internal ParameterDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken parameterType,
        IdentifierToken? parameterRef,
        IdentifierToken parameterName,
        bool parameterIsArray,
        PropertyValueAst? defaultValue = null
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.ParameterType = parameterType ?? throw new ArgumentNullException(nameof(parameterType));
        this.ParameterRef = parameterRef;
        this.ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        this.ParameterIsArray = parameterIsArray;
        this.DefaultValue = defaultValue;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken ParameterType
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken ParameterName
    {
        get;
    }

    [PublicAPI]
    public bool ParameterIsRef =>
        this.ParameterRef is not null;

    [PublicAPI]
    public IdentifierToken? ParameterRef
    {
        get;
    }

    [PublicAPI]
    public bool ParameterIsArray
    {
        get;
    }

    [PublicAPI]
    public PropertyValueAst? DefaultValue
    {
        get;
    }

    #endregion

}
