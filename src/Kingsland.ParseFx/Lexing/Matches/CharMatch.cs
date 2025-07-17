namespace Kingsland.ParseFx.Lexing.Matches;

[PublicAPI]
public sealed class CharMatch : IMatch
{

    #region Constructors

    [PublicAPI]
    public CharMatch(char value)
    {
        this.Value = value;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public char Value
    {
        get;
    }

    #endregion

    #region LexerRule Members

    [PublicAPI]
    public bool Matches(char value)
    {
        return (value == this.Value);
    }

    #endregion

}
