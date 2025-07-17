using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1.5 Boolean value
///
///     booleanValue = TRUE / FALSE
///
///     FALSE        = "false" ; keyword: case insensitive
///     TRUE         = "true"  ; keyword: case insensitive
///
/// </remarks>
public sealed record BooleanValueAst : LiteralValueAst
{

    private static readonly BooleanValueAst True = new(BooleanLiteralToken.True);
    private static readonly BooleanValueAst False = new(BooleanLiteralToken.False);

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public BooleanLiteralToken? Token
        {
            get;
            set;
        }

        [PublicAPI]
        public BooleanValueAst Build()
        {
            return new(
                this.Token ?? throw new InvalidOperationException(
                    $"{nameof(this.Token)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal BooleanValueAst(bool value)
        : this(new BooleanLiteralToken(value))
    {

    }

    internal BooleanValueAst(BooleanLiteralToken token)
    {
        this.Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public BooleanLiteralToken Token
    {
        get;
    }

    [PublicAPI]
    public bool Value =>
        this.Token.Value;

    #endregion

    #region Converters

    public static implicit operator BooleanValueAst(bool value)
    {
        return value
            ? BooleanValueAst.True
            : BooleanValueAst.False;
    }

    #endregion

}
