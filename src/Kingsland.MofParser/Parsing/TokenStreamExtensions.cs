using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static class TokenStreamExtensions
{

    #region PeekIdentifierToken Methods

    internal static bool TryPeekIdentifierToken(this TokenStream stream, string name, out IdentifierToken? result)
    {
        return stream.TryPeek(
            t => t.IsKeyword(name),
            out result
        );
    }

    #endregion

    #region ReadIdentifierToken Methods

    internal static IdentifierToken ReadIdentifierToken(this TokenStream stream, string name)
    {
        var token = stream.Read<IdentifierToken>();
        if (!token.IsKeyword(name))
        {
            throw new UnexpectedTokenException(token);
        }
        return token;
    }

    internal static IdentifierToken ReadIdentifierToken(this TokenStream stream, Func<IdentifierToken, bool> predicate)
    {
        var token = stream.Read<IdentifierToken>();
        return predicate(token) ? token : throw new UnexpectedTokenException(token);
    }

    internal static bool TryReadIdentifierToken(this TokenStream stream, string name, out IdentifierToken? result)
    {
        if (stream.TryPeekIdentifierToken(name, out result))
        {
            stream.Read();
            return true;
        }
        return false;
    }

    #endregion

}

