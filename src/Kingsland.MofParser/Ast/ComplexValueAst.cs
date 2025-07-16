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
///     complexValue = aliasIdentifier /
///                    ( VALUE OF
///                      ( structureName / className / associationName )
///                      propertyValueList )
///
/// </remarks>
public sealed record ComplexValueAst : ComplexTypeValueAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.PropertyValues = new([]);
        }

        [PublicAPI]
        public AliasIdentifierToken? Alias
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? Value
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? Of
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? TypeName
        {
            get;
            set;
        }

        [PublicAPI]
        public PropertyValueListAst PropertyValues
        {
            get;
            set;
        }

        [PublicAPI]
        public ComplexValueAst Build()
        {
            return (this.Alias == null)
                ? new(
                    this.Value ?? throw new InvalidOperationException(
                        $"{nameof(this.Value)} property must be set before calling {nameof(Build)}."
                    ),
                    this.Of ?? throw new InvalidOperationException(
                        $"{nameof(this.Of)} property must be set before calling {nameof(Build)}."
                    ),
                    this.TypeName ?? throw new InvalidOperationException(
                        $"{nameof(this.TypeName)} property must be set before calling {nameof(Build)}."
                    ),
                    this.PropertyValues
                )
                : new(
                    this.Alias
                );
        }

    }

    #endregion

    #region Constructors

    internal ComplexValueAst(
        AliasIdentifierToken alias
    )
    {
        this.Alias = alias ?? throw new ArgumentNullException(nameof(alias));
        this.Value = null;
        this.Of = null;
        this.TypeName = null;
        this.PropertyValues = new();
    }

    internal ComplexValueAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        PropertySlotAst[] propertyValues
    ) : this(value, of, typeName, new PropertyValueListAst(propertyValues))
    {
    }

    internal ComplexValueAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        PropertyValueListAst propertyValues
    )
    {
        this.Alias = null;
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
        this.Of = of ?? throw new ArgumentNullException(nameof(of));
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.PropertyValues = propertyValues ?? throw new ArgumentNullException(nameof(propertyValues));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public bool IsAlias =>
        this.Alias is not null;

    [PublicAPI]
    public bool IsValue =>
        this.Value is not null;

    [PublicAPI]
    public AliasIdentifierToken? Alias
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? Value
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? Of
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? TypeName
    {
        get;
    }

    [PublicAPI]
    public PropertyValueListAst PropertyValues
    {
        get;
    }

    #endregion

    #region Converters

    public static implicit operator ComplexValueAst(AliasIdentifierToken aliasIdentifier)
    {
        return new ComplexValueAst(aliasIdentifier);
    }

    #endregion

}
