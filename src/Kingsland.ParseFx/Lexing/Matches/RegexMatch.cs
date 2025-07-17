using System.Text.RegularExpressions;

namespace Kingsland.ParseFx.Lexing.Matches;

[PublicAPI]
public sealed class RegexMatch : IMatch
{

    #region Constructors

    [PublicAPI]
    public RegexMatch(string pattern)
    {
        this.Pattern = pattern;
        this.Regex = new Regex(pattern, RegexOptions.Compiled);
    }

    #endregion

    #region Properties

    [PublicAPI]
    public string Pattern
    {
        get;
    }

    [PublicAPI]
    public Regex Regex
    {
        get;
    }

    #endregion

    #region LexerRule Members

    [PublicAPI]
    public bool Matches(char value)
    {
        return this.Regex.IsMatch(
            new string(value, 1)
        );
    }

    #endregion

}
