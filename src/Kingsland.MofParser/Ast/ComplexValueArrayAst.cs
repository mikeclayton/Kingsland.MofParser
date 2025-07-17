using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.9 Complex type value
///
///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
///
/// </remarks>
public sealed record ComplexValueArrayAst : ComplexTypeValueAst
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
        public List<ComplexValueAst> Values
        {
            get;
            private set;
        }

        [PublicAPI]
        public ComplexValueArrayAst Build()
        {
            return new(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal ComplexValueArrayAst(params ComplexValueAst[] values)
        : this((IEnumerable<ComplexValueAst>)values)
    {
    }

    internal ComplexValueArrayAst(
        IEnumerable<ComplexValueAst> values
    )
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public ReadOnlyCollection<ComplexValueAst> Values
    {
        get;
    }

    #endregion

}
