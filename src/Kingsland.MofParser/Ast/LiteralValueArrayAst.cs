using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1 Primitive type value
///
///     literalValueArray = "{" [ literalValue *( "," literalValue ) ] "}"
///
/// </remarks>
public sealed record LiteralValueArrayAst : PrimitiveTypeValueAst
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
            set;
        }

        [PublicAPI]
        public LiteralValueArrayAst Build()
        {
            return new(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal LiteralValueArrayAst()
        : this((IEnumerable<LiteralValueAst>)[])
    {
    }

    internal LiteralValueArrayAst(params LiteralValueAst[] values)
        : this((IEnumerable<LiteralValueAst>)values)
    {
    }

    internal LiteralValueArrayAst(
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
