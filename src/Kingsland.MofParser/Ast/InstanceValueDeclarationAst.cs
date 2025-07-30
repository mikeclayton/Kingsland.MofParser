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
///     instanceValueDeclaration = INSTANCE OF ( className / associationName )
///                                [ alias ]
///                                propertyValueList ";"
///
///     alias                    = AS aliasIdentifier
///
/// </remarks>
public sealed record InstanceValueDeclarationAst : MofProductionAst
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
        public IdentifierToken? Instance
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
        public InstanceValueDeclarationAst Build()
        {
            return new(
                instance: this.Instance ?? throw new InvalidOperationException(
                    $"{nameof(this.Instance)} property must be set before calling {nameof(Build)}."
                ),
                of: this.Of ?? throw new InvalidOperationException(
                    $"{nameof(this.Of)} property must be set before calling {nameof(Build)}."
                ),
                typeName: this.TypeName ?? throw new InvalidOperationException(
                    $"{nameof(this.TypeName)} property must be set before calling {nameof(Build)}."
                ),
                @as: this.As,
                alias: this.Alias,
                propertyValues: this.PropertyValues,
                statementEnd: this.StatementEnd ?? throw new InvalidOperationException(
                    $"{nameof(this.StatementEnd)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal InstanceValueDeclarationAst(
        IdentifierToken instance,
        IdentifierToken of,
        IdentifierToken typeName,
        StatementEndToken statementEnd
    ) : this(instance, of, typeName, null, null, new PropertyValueListAst(), statementEnd)
    {
    }

    internal InstanceValueDeclarationAst(
        IdentifierToken instance,
        IdentifierToken of,
        IdentifierToken typeName,
        IdentifierToken @as,
        AliasIdentifierToken alias,
        StatementEndToken statementEnd
    ) : this(instance, of, typeName, @as, alias, new PropertyValueListAst(), statementEnd)
    {
    }

    internal InstanceValueDeclarationAst(
        IdentifierToken instance,
        IdentifierToken of,
        IdentifierToken typeName,
        PropertyValueListAst propertyValues,
        StatementEndToken statementEnd
    ) : this(instance, of, typeName, null, null, propertyValues, statementEnd)
    {
    }

    internal InstanceValueDeclarationAst(
        IdentifierToken instance,
        IdentifierToken of,
        IdentifierToken typeName,
        PropertySlotAst[] propertyValues,
        StatementEndToken statementEnd
    ) : this(instance, of, typeName, null, null, new PropertyValueListAst(propertyValues), statementEnd)
    {
    }

    internal InstanceValueDeclarationAst(
        IdentifierToken instance,
        IdentifierToken of,
        IdentifierToken typeName,
        IdentifierToken @as,
        AliasIdentifierToken alias,
        PropertySlotAst[] propertyValues,
        StatementEndToken statementEnd
    ) : this(instance, of, typeName, @as, alias, new PropertyValueListAst(propertyValues), statementEnd)
    {
    }

    internal InstanceValueDeclarationAst(
        IdentifierToken instance,
        IdentifierToken of,
        IdentifierToken typeName,
        IdentifierToken? @as,
        AliasIdentifierToken? alias,
        PropertyValueListAst propertyValues,
        StatementEndToken statementEnd
    )
    {
        this.Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        this.Of = of ?? throw new ArgumentNullException(nameof(of));
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        if ((@as is not null) || (alias is not null))
        {
            this.As = @as ?? throw new ArgumentNullException(nameof(@as));
            this.Alias = alias ?? throw new ArgumentNullException(nameof(alias));
        }
        this.PropertyValues = propertyValues ?? throw new ArgumentNullException(nameof(propertyValues)); 
        this.StatementEnd = statementEnd ?? throw new ArgumentNullException(nameof(statementEnd));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public IdentifierToken Instance
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
