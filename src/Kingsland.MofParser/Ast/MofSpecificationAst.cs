using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.2 MOF specification
///
///     mofSpecification = *mofProduction
///
/// </remarks>
public sealed record MofSpecificationAst : IAstNode
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.Productions = [];
        }

        [PublicAPI]
        public List<MofProductionAst> Productions
        {
            get;
            set;
        }

        [PublicAPI]
        public MofSpecificationAst Build()
        {
            return new(
                this.Productions
            );
        }

    }

    #endregion

    #region Constructors

    internal MofSpecificationAst()
        : this((IEnumerable<MofProductionAst>)[])
    {
    }

    internal MofSpecificationAst(params MofProductionAst[] values)
        : this((IEnumerable<MofProductionAst>)values)
    {
    }

    internal MofSpecificationAst(
        IEnumerable<MofProductionAst> productions
    )
    {
        this.Productions = (productions ?? throw new ArgumentNullException(nameof(productions)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public ReadOnlyCollection<MofProductionAst> Productions
    {
        get;
    }

    #endregion

}
