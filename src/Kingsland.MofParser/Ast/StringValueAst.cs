using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1.3 String values
///
///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
///
///     stringValue       = singleStringValue *( *WS singleStringValue )
///
/// </remarks>
public sealed record StringValueAst : LiteralValueAst, IEnumElementValueAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.StringLiteralValues = [];
        }

        public List<StringLiteralToken> StringLiteralValues
        {
            get;
            set;
        }

        public string? Value
        {
            get;
            set;
        }

        public StringValueAst Build()
        {
            return new(
                this.StringLiteralValues,
                this.Value ?? throw new InvalidOperationException(
                    $"{nameof(this.Value)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal StringValueAst(
        StringLiteralToken token
    ) : this([token], token.Value)
    {
    }

    internal StringValueAst(
        StringLiteralToken token,
        string value
    ) : this([token], value)
    {
    }

    internal StringValueAst(
        string value
    ) : this([value], value)
    {
    }

    internal StringValueAst(
        params string[] values
    ) : this(
        values.Select(s => new StringLiteralToken(s)),
        string.Join(null, values)
    )
    {
    }

    internal StringValueAst(
        params StringLiteralToken[] tokens
    ) : this(
        tokens,
        string.Join(null, tokens.Select(token => token.Value))
    )
    {
    }

    internal StringValueAst(
        IEnumerable<StringLiteralToken> stringLiteralValues,
        string value
    )
    {
        var values = (stringLiteralValues ?? throw new ArgumentNullException(nameof(stringLiteralValues)))
            .ToList();
        if (values.Count == 0)
        {
            throw new ArgumentException(null, nameof(stringLiteralValues));
        }
        this.StringLiteralValues = new(values);
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<StringLiteralToken> StringLiteralValues
    {
        get;
    }

    public string Value
    {
        get;
    }

    #endregion

    #region Converters

    public static implicit operator StringValueAst(string value)
    {
        return new StringValueAst(value);
    }

    #endregion

}
