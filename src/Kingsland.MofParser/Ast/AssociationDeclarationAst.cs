using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.3 Association declaration
///
///     associationDeclaration = [ qualifierList ] ASSOCIATION associationName
///                              [ superAssociation ]
///                              "{" *classFeature "}" ";"
///
///     associationName        = elementName
///
///     superAssociation       = ":" elementName
///
///     ASSOCIATION            = "association" ; keyword: case insensitive
///
/// </remarks>
public sealed record AssociationDeclarationAst : MofProductionAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
            this.ClassFeatures = [];
        }

        [PublicAPI]
        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? AssociationName
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? SuperAssociation
        {
            get;
            set;
        }

        [PublicAPI]
        public List<IClassFeatureAst> ClassFeatures
        {
            get;
            set;
        }

        [PublicAPI]
        public AssociationDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.AssociationName ?? throw new InvalidOperationException(
                    $"{nameof(this.AssociationName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperAssociation,
                this.ClassFeatures
            );
        }

    }

    #endregion

    #region Constructors

    internal AssociationDeclarationAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken associationName
    ) : this(new(qualifierList), associationName, null, null)
    {
    }

    internal AssociationDeclarationAst(
        IdentifierToken associationName,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    ) : this(null, associationName, null, classFeatures)
    {
    }

    internal AssociationDeclarationAst(
        IdentifierToken associationName,
        IdentifierToken? superAssociation,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    ) : this(null, associationName, superAssociation, classFeatures)
    {
    }

    internal AssociationDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken associationName,
        IdentifierToken? superAssociation = null,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.AssociationName = associationName ?? throw new ArgumentNullException(nameof(associationName));
        this.SuperAssociation = superAssociation;
        this.ClassFeatures = (classFeatures ?? []).ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken AssociationName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? SuperAssociation
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
    {
        get;
    }

    #endregion

}
