using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

[PublicAPI]
public sealed class TokenBuilder
{

    #region Constructors

    [PublicAPI]
    public TokenBuilder()
    {
        this.Tokens = [];
    }

    #endregion

    #region Properties

    private List<SyntaxToken> Tokens
    {
        get;
    }

    #endregion

    #region Methods

    [PublicAPI]
    public List<SyntaxToken> ToList()
    {
        // return a duplicate of the Tokens value so our
        // internal list isn't exposed to external code
        return [.. this.Tokens];
    }

    #endregion

    #region AliasIdentifierToken

    [PublicAPI]
    public TokenBuilder AliasIdentifierToken(string name)
    {
        this.Tokens.Add(new AliasIdentifierToken(name));
        return this;
    }

    [PublicAPI]
    public TokenBuilder AliasIdentifierToken(SourcePosition start, SourcePosition end, string text, string name)
    {
        this.Tokens.Add(new AliasIdentifierToken(start, end, text, name));
        return this;
    }

    [PublicAPI]
    public TokenBuilder AliasIdentifierToken(SourceExtent extent, string name)
    {
        this.Tokens.Add(new AliasIdentifierToken(extent, name));
        return this;
    }

    #endregion

    #region AttributeCloseToken

    [PublicAPI]
    public TokenBuilder AttributeCloseToken()
    {
        this.Tokens.Add(new AttributeCloseToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder AttributeCloseToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new AttributeCloseToken(start, end, text));
        return this;
    }

    #endregion

    #region AttributeOpenToken

    [PublicAPI]
    public TokenBuilder AttributeOpenToken()
    {
        this.Tokens.Add(new AttributeOpenToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder AttributeOpenToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new AttributeOpenToken(start, end, text));
        return this;
    }

    #endregion

    #region BlockCloseToken

    [PublicAPI]
    public TokenBuilder BlockCloseToken()
    {
        this.Tokens.Add(new BlockCloseToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder BlockCloseToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new BlockCloseToken(start, end, text));
        return this;
    }

    #endregion

    #region BlockOpenToken

    [PublicAPI]
    public TokenBuilder BlockOpenToken()
    {
        this.Tokens.Add(new BlockOpenToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder BlockOpenToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new BlockOpenToken(start, end, text));
        return this;
    }

    #endregion

    #region BooleanLiteralToken

    [PublicAPI]
    public TokenBuilder BooleanLiteralToken(bool value)
    {
        this.Tokens.Add(
            new BooleanLiteralToken(value)
        );
        return this;
    }

    [PublicAPI]
    public TokenBuilder BooleanLiteralToken(string text, bool value)
    {
        this.Tokens.Add(
            new BooleanLiteralToken(text, value)
        );
        return this;
    }

    [PublicAPI]
    public TokenBuilder BooleanLiteralToken(SourcePosition? start, SourcePosition? end, string text, bool value)
    {
        this.Tokens.Add(
            new BooleanLiteralToken(start, end, text, value)
        );
        return this;
    }

    #endregion

    #region ColonToken

    [PublicAPI]
    public TokenBuilder ColonToken()
    {
        this.Tokens.Add(new ColonToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder ColonToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new ColonToken(start, end, text));
        return this;
    }

    #endregion

    #region CommaToken

    [PublicAPI]
    public TokenBuilder CommaToken()
    {
        this.Tokens.Add(new CommaToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder CommaToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new CommaToken(start, end, text));
        return this;
    }

    #endregion

    #region CommentToken

    [PublicAPI]
    public TokenBuilder CommentToken(string value)
    {
        this.Tokens.Add(new CommentToken(value));
        return this;
    }

    [PublicAPI]
    public TokenBuilder CommentToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new CommentToken(start, end, text));
        return this;
    }

    #endregion

    #region DotOperatorToken

    [PublicAPI]
    public TokenBuilder DotOperatorToken()
    {
        this.Tokens.Add(new DotOperatorToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder DotOperatorToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new DotOperatorToken(start, end, text));
        return this;
    }

    #endregion

    #region EqualsOperatorToken

    [PublicAPI]
    public TokenBuilder EqualsOperatorToken()
    {
        this.Tokens.Add(new EqualsOperatorToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder EqualsOperatorToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new EqualsOperatorToken(start, end, text));
        return this;
    }

    #endregion

    #region IdentifierToken

    [PublicAPI]
    public TokenBuilder IdentifierToken(string name)
    {
        this.Tokens.Add(new IdentifierToken(name));
        return this;
    }

    [PublicAPI]
    public TokenBuilder IdentifierToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new IdentifierToken(start, end, text));
        return this;
    }

    #endregion

    #region IntegerLiteralToken

    [PublicAPI]
    public TokenBuilder IntegerLiteralToken(IntegerKind kind, long value)
    {
        this.Tokens.Add(new IntegerLiteralToken(kind, value));
        return this;
    }

    [PublicAPI]
    public TokenBuilder IntegerLiteralToken(string text, IntegerKind kind, long value)
    {
        this.Tokens.Add(new IntegerLiteralToken(text, kind, value));
        return this;
    }

    [PublicAPI]
    public TokenBuilder IntegerLiteralToken(SourcePosition start, SourcePosition end, string text, IntegerKind kind, long value)
    {
        this.Tokens.Add(new IntegerLiteralToken(start, end, text, kind, value));
        return this;
    }

    #endregion

    #region NullLiteralToken

    [PublicAPI]
    public TokenBuilder NullLiteralToken()
    {
        this.Tokens.Add(new NullLiteralToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder NullLiteralToken(string text)
    {
        this.Tokens.Add(new NullLiteralToken(null, null, text));
        return this;
    }

    [PublicAPI]
    public TokenBuilder NullLiteralToken(SourcePosition? start, SourcePosition? end, string text)
    {
        this.Tokens.Add(new NullLiteralToken(start, end, text));
        return this;
    }

    #endregion

    #region ParenthesisCloseToken

    [PublicAPI]
    public TokenBuilder ParenthesisCloseToken()
    {
        this.Tokens.Add(new ParenthesisCloseToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder ParenthesisCloseToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new ParenthesisCloseToken(start, end, text));
        return this;
    }

    #endregion

    #region ParenthesisOpenToken

    [PublicAPI]
    public TokenBuilder ParenthesisOpenToken()
    {
        this.Tokens.Add(new ParenthesisOpenToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder ParenthesisOpenToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new ParenthesisOpenToken(start, end, text));
        return this;
    }

    #endregion

    #region PragmaToken

    [PublicAPI]
    public TokenBuilder PragmaToken()
    {
        this.Tokens.Add(new PragmaToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder PragmaToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new PragmaToken(start, end, text));
        return this;
    }

    #endregion

    #region RealLiteralToken

    [PublicAPI]
    public TokenBuilder RealLiteralToken(double value)
    {
        this.Tokens.Add(
            new RealLiteralToken(value)
        );
        return this;
    }

    [PublicAPI]
    public TokenBuilder RealLiteralToken(string text, double value)
    {
        this.Tokens.Add(new RealLiteralToken(null, null, text, value));
        return this;
    }

    [PublicAPI]
    public TokenBuilder RealLiteralToken(SourcePosition? start, SourcePosition? end, string text, double value)
    {
        this.Tokens.Add(new RealLiteralToken(start, end, text, value));
        return this;
    }

    #endregion

    #region StatementEndToken

    [PublicAPI]
    public TokenBuilder StatementEndToken()
    {
        this.Tokens.Add(new StatementEndToken());
        return this;
    }

    [PublicAPI]
    public TokenBuilder StatementEndToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new StatementEndToken(start, end, text));
        return this;
    }

    #endregion

    #region StringLiteralToken

    [PublicAPI]
    public TokenBuilder StringLiteralToken(string value)
    {
        this.Tokens.Add(new StringLiteralToken(value));
        return this;
    }

    [PublicAPI]
    public TokenBuilder StringLiteralToken(SourcePosition start, SourcePosition end, string text, string value)
    {
        this.Tokens.Add(new StringLiteralToken(start, end, text, value));
        return this;
    }

    #endregion

    #region WhitespaceToken

    [PublicAPI]
    public TokenBuilder WhitespaceToken(string value)
    {
        this.Tokens.Add(new WhitespaceToken(value));
        return this;
    }

    [PublicAPI]
    public TokenBuilder WhitespaceToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new WhitespaceToken(start, end, text));
        return this;
    }

    #endregion

}
