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
///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
///
/// </remarks>

public sealed record QualifierValueArrayInitializerAst : IQualifierInitializerAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.Values = [];
        }

        [PublicAPI]
        public List<LiteralValueAst> Values
        {
            get;
            private set;
        }

        [PublicAPI]
        public QualifierValueArrayInitializerAst Build()
        {
            return new(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierValueArrayInitializerAst()
        : this([])
    {
    }

    internal QualifierValueArrayInitializerAst(params LiteralValueAst[] values)
        : this((IEnumerable<LiteralValueAst>)values)
    {
    }

    internal QualifierValueArrayInitializerAst(
        IEnumerable<LiteralValueAst> values
    )
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public ReadOnlyCollection<LiteralValueAst> Values
    {
        get;
    }

    #endregion

}
