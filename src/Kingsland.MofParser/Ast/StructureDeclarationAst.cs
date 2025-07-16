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
/// 7.5.1 Structure declaration
///
///     structureDeclaration = [ qualifierList ] STRUCTURE structureName
///                            [ superStructure ]
///                            "{" *structureFeature "}" ";"
///
///     structureName        = elementName
///
///     superStructure       = ":" structureName
///
///     structureFeature     = structureDeclaration /   ; local structure
///                            enumerationDeclaration / ; local enumeration
///                            propertyDeclaration
///
///     STRUCTURE            = "structure" ; keyword: case insensitive
///
/// </remarks>
public sealed record StructureDeclarationAst : MofProductionAst, IStructureFeatureAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
            this.StructureFeatures = [];
        }

        [PublicAPI]
        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? Structure
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? StructureName
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? SuperStructure
        {
            get;
            set;
        }

        [PublicAPI]
        public List<IStructureFeatureAst> StructureFeatures
        {
            get;
            set;
        }

        [PublicAPI]
        public StatementEndToken? StatementEnd
        {
            get;
            set;
        }

        [PublicAPI]
        public StructureDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.Structure ?? "structure",
                this.StructureName ?? throw new InvalidOperationException(
                    $"{nameof(this.StructureName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperStructure,
                this.StructureFeatures,
                this.StatementEnd ?? ";"
            );
        }

    }

    #endregion

    #region Constructors

    internal StructureDeclarationAst(
        IdentifierToken structure,
        IdentifierToken structureName,
        StatementEndToken statementEnd
    ) : this(null, structure, structureName, null, null, statementEnd)
    {
    }

    internal StructureDeclarationAst(
        IdentifierToken structure,
        IdentifierToken structureName,
        IdentifierToken? superStructure,
        StatementEndToken statementEnd
    ) : this(null, structure, structureName, superStructure, null, statementEnd)
    {
    }

    internal StructureDeclarationAst(
        IdentifierToken structure,
        IdentifierToken structureName,
        IdentifierToken? superStructure,
        IEnumerable<IStructureFeatureAst>? structureFeatures,
        StatementEndToken statementEnd
    ) : this(null, structure, structureName, superStructure, structureFeatures, statementEnd)
    {
    }

    internal StructureDeclarationAst(
        IdentifierToken structure,
        IdentifierToken structureName,
        IStructureFeatureAst[] structureFeatures,
        StatementEndToken statementEnd
    ) : this(null, structure, structureName, null, structureFeatures, statementEnd)
    {
    }

    internal StructureDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken structure,
        IdentifierToken structureName,
        IdentifierToken? superStructure,
        IEnumerable<IStructureFeatureAst>? structureFeatures,
        StatementEndToken statementEnd
    )
    {
        this.QualifierList = qualifierList ?? new ();
        this.Structure = structure ?? throw new ArgumentNullException(nameof(structure));
        this.StructureName = structureName ?? throw new ArgumentNullException(nameof(structureName));
        this.SuperStructure = superStructure;
        this.StructureFeatures = (structureFeatures ?? []).ToList().AsReadOnly();
        this.StatementEnd = statementEnd ?? throw new ArgumentNullException(nameof(statementEnd));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken Structure
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken StructureName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? SuperStructure
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<IStructureFeatureAst> StructureFeatures
    {
        get;
    }

    [PublicAPI]
    public StatementEndToken StatementEnd
    {
        get;
    }

    #endregion

}
