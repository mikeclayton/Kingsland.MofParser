using Kingsland.MofParser.Attributes.StaticAnalysis;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.3 Enum type value
///
///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
///
/// </remarks>
public sealed record EnumValueArrayAst : EnumTypeValueAst
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
        public List<EnumValueAst> Values
        {
            get;
            private set;
        }

        [PublicAPI]
        public EnumValueArrayAst Build()
        {
            return new(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumValueArrayAst()
        : this((IEnumerable<EnumValueAst>)[])
    {
    }

    internal EnumValueArrayAst(params EnumValueAst[] values)
        : this((IEnumerable<EnumValueAst>)values)
    {
    }

    internal EnumValueArrayAst(
        IEnumerable<EnumValueAst> values
    )
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public ReadOnlyCollection<EnumValueAst> Values
    {
        get;
    }

    #endregion

}
