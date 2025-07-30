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
///     propertyValueList = "{" *propertySlot "}"
///
///     propertySlot      = propertyName "=" propertyValue ";"
///
///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
///
///     propertyName      = IDENTIFIER
///
/// </remarks>
public sealed record PropertyValueListAst : IAstNode
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.PropertySlots = [];
        }

        [PublicAPI]
        public List<PropertySlotAst> PropertySlots
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertyValueListAst Build()
        {
            return new(
                this.PropertySlots
            );
        }

    }

    #endregion

    #region Constructors

    internal PropertyValueListAst(params PropertySlotAst[] values)
        : this((IEnumerable<PropertySlotAst>)values)
    {
    }

    internal PropertyValueListAst(
        IEnumerable<PropertySlotAst> propertySlots
    )
    {
        this.PropertySlots = (propertySlots ?? throw new ArgumentNullException(nameof(propertySlots)))
            .ToList().AsReadOnly();
        this.PropertyValues = new(
            (propertySlots ?? throw new ArgumentNullException(nameof(propertySlots)))
                .ToDictionary(
                    slot => slot.PropertyName.Name,
                    slot => slot.PropertyValue
                )
        );
    }

    #endregion

    #region Properties

    [PublicAPI]
    public ReadOnlyCollection<PropertySlotAst> PropertySlots
    {
        get;
    }

    [PublicAPI]
    public Dictionary<string, PropertyValueAst> PropertyValues
    {
        get;
    }

    #endregion

}
