﻿using Kingsland.ParseFx.Syntax;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Kingsland.ParseFx.Parsing;

[PublicAPI]
public sealed class TokenStream
{

    #region Constructors

    [PublicAPI]
    public TokenStream(IEnumerable<SyntaxToken> source)
    {
        this.Source = (source ?? throw new ArgumentNullException(nameof(source)))
            .ToList();
        this.Position = 0;
    }

    #endregion

    #region Properties

    private List<SyntaxToken> Source
    {
        get;
    }

    [PublicAPI]
    public int Position
    {
        get;
        set;
    }

    [PublicAPI]
    public bool Eof =>
        (this.Source.Count == 0) ||
        (this.Position >= this.Source.Count);

    #endregion

    #region Peek Methods

    /// <summary>
    /// Retrieves, but does not consume, the next token in the stream.
    /// Throws an exception if the stream has no more tokens to read.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public SyntaxToken Peek()
    {
        if ((this.Source.Count == 0) ||
           (this.Position >= this.Source.Count))
        {
            throw new UnexpectedEndOfStreamException();
        }
        return this.Source[this.Position];
    }

    /// <summary>
    /// Retrieves, but does not consume, the next token in the stream.
    /// If the token is not of type T it is discarded and the function returns null instead.
    /// Throws an exception if the stream has no more tokens to read.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public T? Peek<T>() where T : SyntaxToken
    {
        if ((this.Source.Count == 0) ||
           (this.Position >= this.Source.Count))
        {
            throw new UnexpectedEndOfStreamException();
        }
        return (this.Source[this.Position] as T);
    }

    #endregion

    #region TryPeek Methods

    /// <summary>
    /// Retrieves, but does not consume, the next token in the stream.
    /// Returns true if the next token was of type T, otherwise false.
    /// Returns false if the stream has no more tokens to read.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public bool TryPeek<T>() where T : SyntaxToken
    {

        if ((this.Source.Count == 0) ||
           (this.Position >= this.Source.Count))
        {
            return false;
        }
        return (this.Source[this.Position] is T);
    }

    /// <summary>
    /// Retrieves, but does not consume, the next token in the stream.
    /// Returns true if the next token was of type T, otherwise false.
    /// Returns false if the stream has no more tokens to read.
    /// If the token is of type T it is assigned to the output variable.
    /// Throws an exception if the stream has no more tokens to read.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public bool TryPeek<T>(
        [NotNullWhen(true)] out T? result
    ) where T : SyntaxToken
    {
        if ((this.Source.Count == 0) ||
            (this.Position >= this.Source.Count))
        {
            result = null;
            return false;
        }
        var peek = this.Source[this.Position] as T;
        if (peek == null)
        {
            result = null;
            return false;
        }
        result = peek;
        return true;
    }

    [PublicAPI]
    public bool TryPeek<T>(Func<T, bool> predicate, out T? result) where T : SyntaxToken
    {

        if ((this.Source.Count == 0) ||
            (this.Position >= this.Source.Count))
        {
            throw new UnexpectedEndOfStreamException();
        }
        if ((this.Source[this.Position] is T peek) && predicate(peek))
        {
            result = peek;
            return true;
        }
        result = null;
        return false;
    }

    #endregion

    #region Read Methods

    /// <summary>
    /// Retrieves, and consumes, the next token in the stream.
    /// Throws an exception if the stream has no more tokens to read.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public SyntaxToken Read()
    {
        var value = this.Peek();
        this.Position += 1;
        return value;
    }

    /// <summary>
    /// Retrieves, and consumes, the next token in the stream.
    /// If the token is not of type T an exception is thrown.
    /// Throws an exception if the stream has no more tokens to read.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public T Read<T>() where T : SyntaxToken
    {
        var token = this.Peek();
        if (token is T cast)
        {
            this.Read();
            return cast;
        }
        throw new UnexpectedTokenException(token);
    }

    #endregion

    #region TryRead Methods

    [PublicAPI]
    public bool TryRead<T>(
        [NotNullWhen(true)] out T? result
    ) where T : SyntaxToken
    {
        if (this.TryPeek(out result))
        {
            this.Read();
            return true;
        }
        return false;
    }

    #endregion

    #region Backtrack Methods

    /// <summary>
    /// Moves the stream position back a token.
    /// </summary>
    [PublicAPI]
    public void Backtrack()
    {
        this.Backtrack(1);
    }

    /// <summary>
    /// Moves the stream position back the specified number of tokens.
    /// </summary>
    [PublicAPI]
    public void Backtrack(int count)
    {
        if (this.Position < count)
        {
            throw new InvalidOperationException();
        }
        this.Position -= count;
    }

    #endregion

    #region Object Interface

    public override string ToString()
    {
        var result = new StringBuilder();
        var count = 0;
        for (var i = Math.Max(0, this.Position - 5); i < Math.Min(this.Source.Count, this.Position + 5); i++)
        {
            if (count > 0)
            {
                result.Append(' ');
                count += 1;
            }
            if (i == this.Position)
            {
                result.Append(">>>");
                count += 3;
            }
            result.Append(this.Source[i]);
        }
        return $"Current = '{result}'";
    }

    #endregion

}
