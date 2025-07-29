using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.6 Method declaration
///
///     methodDeclaration = [ qualifierList ]
///                         ( ( returnDataType [ array ] ) / VOID ) methodName
///                         "(" [ parameterList ] ")" ";"
///
///     returnDataType    = primitiveType /
///                         structureOrClassName /
///                         enumName /
///                         classReference
///
///     methodName        = IDENTIFIER
///     classReference    = DT_REFERENCE
///     DT_REFERENCE      = className REF
///     VOID              = "void" ; keyword: case insensitive
///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
///
/// 7.5.5 Property declaration
///
///    array             = "[" "]"
///
/// </remarks>
public sealed record MethodDeclarationAst : IClassFeatureAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
            this.Parameters = [];
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
        public bool ReturnTypeIsArray
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? MethodName
        {
            get;
            set;
        }

        [PublicAPI]
        public List<ParameterDeclarationAst> Parameters
        {
            get;
            set;
        }

        [PublicAPI]
        public MethodDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.ReturnType ?? throw new InvalidOperationException(
                    $"{nameof(this.ReturnType)} property must be set before calling {nameof(Build)}."
                ),
                this.ReturnTypeRef,
                this.ReturnTypeIsArray,
                this.MethodName ?? throw new InvalidOperationException(
                    $"{nameof(this.MethodName)} property must be set before calling {nameof(Build)}."
                ),
                this.Parameters
            );
        }

    }

    #endregion

    #region Constructors

    internal MethodDeclarationAst(
        IdentifierToken returnType,
        IdentifierToken methodName,
        IEnumerable<ParameterDeclarationAst>? parameters = null
    ) : this(null, returnType, null, false, methodName, parameters)
    {
        // Integer GetMembersWithOutstandingFees();
        // Integer GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
    }

    internal MethodDeclarationAst(
        IdentifierToken returnType,
        bool returnTypeIsArray,
        IdentifierToken methodName,
        IEnumerable<ParameterDeclarationAst>? parameters = null
    ) : this(null, returnType, null, returnTypeIsArray, methodName, parameters)
    {
        // Integer GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
        // Integer[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
    }

    internal MethodDeclarationAst(
        IdentifierToken returnType,
        IdentifierToken returnTypeRef,
        IdentifierToken methodName,
        IEnumerable<ParameterDeclarationAst>? parameters = null
    ) : this(null, returnType, returnTypeRef, false, methodName, parameters)
    {
        // MyClass REF GetMembersWithOutstandingFees();
        // MyClass REF GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
    }

    internal MethodDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken returnType,
        IdentifierToken? returnTypeRef,
        bool returnTypeIsArray,
        IdentifierToken methodName,
        IEnumerable<ParameterDeclarationAst>? parameters = null
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        this.ReturnTypeRef = returnTypeRef;
        this.ReturnTypeIsArray = returnTypeIsArray;
        this.Name = methodName ?? throw new ArgumentNullException(nameof(methodName));
        this.Parameters = (parameters ?? []).ToList().AsReadOnly();
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
    public bool ReturnTypeIsArray
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken Name
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<ParameterDeclarationAst> Parameters
    {
        get;
    }

    #endregion

}
