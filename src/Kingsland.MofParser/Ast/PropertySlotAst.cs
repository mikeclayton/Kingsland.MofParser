using Kingsland.MofParser.Attributes.StaticAnalysis;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.9 Complex type value
///
///     propertySlot      = propertyName "=" propertyValue ";"
///
///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
///
///     propertyName      = IDENTIFIER
///
/// </remarks>
public sealed record PropertySlotAst : AstNode
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public IdentifierToken? PropertyName
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertyValueAst? PropertyValue
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertySlotAst Build()
        {
            return new(
                this.PropertyName ?? throw new NullReferenceException(),
                this.PropertyValue ?? throw new NullReferenceException()
            );
        }

    }

    #endregion

    #region Constructors

    internal PropertySlotAst(
        IdentifierToken propertyName, bool value
    ) : this(propertyName, new BooleanValueAst(value))
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, int value
    ) : this(propertyName, new IntegerValueAst(value))
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, double value
    ) : this(propertyName, new RealValueAst(value))
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, string value
    ) : this(propertyName, new StringValueAst(value))
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, AliasIdentifierToken aliasIdentifier
    ) : this(propertyName, new ComplexValueAst(aliasIdentifier))
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, AliasIdentifierToken[] aliasIdentifiers
    ) : this(
        propertyName,
        new ComplexValueArrayAst(
            (aliasIdentifiers ?? throw new ArgumentNullException(nameof(aliasIdentifiers)))
                .Select(aliasIdentifier => new ComplexValueAst(aliasIdentifier))
        )
    )
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, EnumValueAst[] enumValues
    ) : this(propertyName, new EnumValueArrayAst(enumValues))
    {
    }

    internal PropertySlotAst(
        IdentifierToken propertyName, PropertyValueAst propertyValue
    )
    {
        this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        this.PropertyValue = propertyValue ?? throw new ArgumentNullException(nameof(propertyValue));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public IdentifierToken PropertyName
    {
        get;
    }

    [PublicAPI]
    public PropertyValueAst PropertyValue
    {
        get;
    }

    #endregion

}
