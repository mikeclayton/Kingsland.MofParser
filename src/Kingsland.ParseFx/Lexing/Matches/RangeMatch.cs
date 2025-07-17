namespace Kingsland.ParseFx.Lexing.Matches;

[PublicAPI]
public sealed class RangeMatch : IMatch
{

    #region Constructors

    [PublicAPI]
    public RangeMatch(char fromValue, char toValue)
    {
        if (fromValue > toValue)
        {
            throw new ArgumentException($"{nameof(fromValue)} must be less than {nameof(toValue)}.");
        }
        this.FromValue = fromValue;
        this.ToValue = toValue;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public char FromValue
    {
        get;
    }

    [PublicAPI]
    public char ToValue
    {
        get;
    }

    #endregion

    #region LexerRule Members

    [PublicAPI]
    public bool Matches(char value)
    {
        return (value >= this.FromValue) && (value <= this.ToValue);
    }

    #endregion

}
