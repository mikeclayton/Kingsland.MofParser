using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// 7.6.2 Complex type value
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
///     structureValueDeclaration = VALUE OF
///                                 ( className / associationName / structureName )
///                                 alias
///                                 propertyValueList ";"
///
///     alias                     = AS aliasIdentifier
///
/// </remarks>
public sealed record StructureValueDeclarationAst : MofProductionAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.PropertyValues = new();
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
        public IdentifierToken? As
        {
            get;
            set;
        }

        [PublicAPI]
        public AliasIdentifierToken? Alias
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
        public StatementEndToken? StatementEnd
        {
            get;
            set;
        }

        [PublicAPI]
        public StructureValueDeclarationAst Build()
        {
            return new(
                value: this.Value ?? throw new InvalidOperationException(
                    $"{nameof(this.Value)} property must be set before calling {nameof(Build)}."
                ),
                of: this.Of ?? throw new InvalidOperationException(
                    $"{nameof(this.Of)} property must be set before calling {nameof(Build)}."
                ),
                typeName: this.TypeName ?? throw new InvalidOperationException(
                    $"{nameof(this.TypeName)} property must be set before calling {nameof(Build)}."
                ),
                @as: this.As,
                alias: this.Alias,
                propertyValues: (this.PropertyValues ?? throw new InvalidOperationException(
                        $"{nameof(this.PropertyValues)} property must be set before calling {nameof(Build)}."
                    )
                ),
                statementEnd: this.StatementEnd ?? throw new InvalidOperationException(
                    $"{nameof(this.StatementEnd)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal StructureValueDeclarationAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        IdentifierToken @as,
        AliasIdentifierToken alias,
        StatementEndToken statementEnd
    ) : this(value, of, typeName, @as, alias, (PropertyValueListAst?)null, statementEnd)
    {
    }

    internal StructureValueDeclarationAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        IdentifierToken @as,
        AliasIdentifierToken alias,
        PropertySlotAst[] propertyValues,
        StatementEndToken statementEnd
    ) : this(value, of, typeName, @as, alias, new PropertyValueListAst(propertyValues), statementEnd)
    {
    }

    internal StructureValueDeclarationAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        IdentifierToken? @as,
        AliasIdentifierToken? alias,
        PropertyValueListAst? propertyValues,
        StatementEndToken statementEnd
    )
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
        this.Of = of ?? throw new ArgumentNullException(nameof(of));
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        if ((@as is not null) || (alias is not null))
        {
            this.As = @as ?? throw new ArgumentNullException(nameof(@as));
            this.Alias = alias ?? throw new ArgumentNullException(nameof(alias));
        }
        this.PropertyValues = propertyValues ?? new();
        this.StatementEnd = statementEnd ?? throw new ArgumentNullException(nameof(statementEnd));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public IdentifierToken Value
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken Of
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken TypeName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? As
    {
        get;
    }

    [PublicAPI]
    public AliasIdentifierToken? Alias
    {
        get;
    }

    [PublicAPI]
    public PropertyValueListAst PropertyValues
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
