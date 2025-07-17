namespace Kingsland.ParseFx.Lexing.Matches;

[PublicAPI]
public sealed class CharArrayMatch : IMatch
{

    #region Constructors

    [PublicAPI]
    public CharArrayMatch(char[] values)
    {
        this.Values = values;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public char[] Values
    {
        get;
    }

    #endregion

    #region LexerRule Members

    [PublicAPI]
    public bool Matches(char value)
    {
        return this.Values.Contains(value);
    }

    #endregion

}
