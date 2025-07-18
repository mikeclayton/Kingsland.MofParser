﻿using Kingsland.ParseFx.Syntax;

namespace Kingsland.ParseFx.Parsing;

[PublicAPI]
public sealed class UnexpectedTokenException : Exception
{

    #region Constructors

    internal UnexpectedTokenException()
    {
    }

    [PublicAPI]
    public UnexpectedTokenException(SyntaxToken foundToken)
    {
        this.FoundToken = foundToken
            ?? throw new ArgumentNullException(nameof(foundToken));
    }

    #endregion

    #region Properties

    [PublicAPI]
    public SyntaxToken? FoundToken
    {
        get;
    }

    public override String Message
    {
        get
        {
            if (this.FoundToken == null)
            {
                var message = "Unexpected token found.";
                return message;
            }
            else
            {
                var newline = Environment.NewLine;
                var token = this.FoundToken;
                var extent = this.FoundToken.Extent;
                var startPosition = extent?.StartPosition;
                return $"Unexpected token found at Position {startPosition?.Position}, Line Number {startPosition?.LineNumber}, Column Number {startPosition?.ColumnNumber}.{newline}" +
                       $"Token Type: '{token.GetType().Name}'{newline}" +
                       $"Token Text: '{token.Extent?.Text}'";
            }
        }
    }

    #endregion

}
