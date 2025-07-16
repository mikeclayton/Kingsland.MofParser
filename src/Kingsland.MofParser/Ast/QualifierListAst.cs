using Kingsland.MofParser.Attributes.StaticAnalysis;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.4.1 QualifierList
///
///     qualifierList = "[" qualifierValue *( "," qualifierValue ) "]"
///
/// </remarks>
public sealed record QualifierListAst : AstNode
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierValues = [];
        }

        [PublicAPI]
        public List<QualifierValueAst> QualifierValues
        {
            get;
            set;
        }

        [PublicAPI]
        public QualifierListAst Build()
        {
            return new(
                this.QualifierValues
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierListAst()
        : this([])
    {
    }

    internal QualifierListAst(params QualifierValueAst[] qualifierValues)
        : this((IEnumerable<QualifierValueAst>) qualifierValues)
    {
    }

    internal QualifierListAst(
        IEnumerable<QualifierValueAst> qualifierValues
    )
    {
        this.QualifierValues = (qualifierValues ?? throw new ArgumentNullException(nameof(qualifierValues)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public ReadOnlyCollection<QualifierValueAst> QualifierValues
    {
        get;
    }

    #endregion

}
