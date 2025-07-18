﻿using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Syntax;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class LexerAssert
{

    internal static void AreEqual(SyntaxToken? expectedToken, SyntaxToken? actualToken, bool ignoreExtent)
    {
        LexerAssert.AreEqualInternal(expectedToken, actualToken, ignoreExtent);
    }

    internal static void AreEqual(List<SyntaxToken>? expectedTokens, List<SyntaxToken>? actualTokens, bool ignoreExtent)
    {
        if ((expectedTokens == null) && (actualTokens == null))
        {
            return;
        }
        if (expectedTokens == null)
        {
            Assert.Fail("expected is null, but actual is not null");
            // nullable workaround until Assert.Fail gets annotated with [DoesNotReturn]
            throw new InvalidOperationException();
        }
        if (actualTokens == null)
        {
            Assert.Fail("expected is not null, but actual is null");
            // nullable workaround until Assert.Fail gets annotated with [DoesNotReturn]
            throw new InvalidOperationException();
        }
        Assert.Multiple(() =>
        {
            Assert.That(actualTokens.Count, Is.EqualTo(expectedTokens.Count), "expected and actual are different lengths");
            for (var i = 0; i < Math.Min(expectedTokens.Count, actualTokens.Count); i++)
            {
                LexerAssert.AreEqualInternal(expectedTokens[i], actualTokens[i], ignoreExtent, i);
            }
        });
    }

    private static void AreEqualInternal(SyntaxToken? expectedToken, SyntaxToken? actualToken, bool ignoreExtent, int index = -1)
    {
        if ((expectedToken == null) && (actualToken == null))
        {
            return;
        }
        if (expectedToken == null)
        {
            LexerAssert.Fail(
                "expected is null, but actual is not null", index
            );
        }
        if (actualToken == null)
        {
            LexerAssert.Fail(
                "expected is not null, but actual is null", index
            );
        }
        if (expectedToken.GetType() != actualToken.GetType())
        {
            LexerAssert.Fail(
                 "Actual token type does not match expected token type",
                $"{expectedToken.GetType().Name} (\"{LexerAssert.EscapeString(expectedToken.GetSourceString())}\")",
                $"{actualToken.GetType().Name} (\"{LexerAssert.EscapeString(actualToken.GetSourceString())}\")",
                index
            );
        }
        if (!ignoreExtent)
        {
            Assert.That(
                actualToken.Extent?.StartPosition?.Position,
                Is.EqualTo(
                    expectedToken.Extent?.StartPosition?.Position
                ),
                LexerAssert.GetFailMessage($"actual Start Position '{actualToken.Extent?.StartPosition?.Position}' does not match expected value '{expectedToken.Extent?.StartPosition?.Position}'", index)
            );
            Assert.That(
                actualToken.Extent?.StartPosition?.LineNumber,
                Is.EqualTo(
                    expectedToken.Extent?.StartPosition?.LineNumber
                ),
                LexerAssert.GetFailMessage($"actual Start Line '{actualToken.Extent?.StartPosition?.LineNumber}' does not match expected value '{expectedToken.Extent?.StartPosition?.LineNumber}'", index)
            );
            Assert.That(
                actualToken.Extent?.StartPosition?.ColumnNumber,
                Is.EqualTo(
                    expectedToken.Extent?.StartPosition?.ColumnNumber
                ),
                LexerAssert.GetFailMessage($"actual Start Column '{actualToken.Extent?.StartPosition?.ColumnNumber}' does not match expected value '{expectedToken.Extent?.StartPosition?.ColumnNumber}'", index)
            );
            Assert.That(
                actualToken.Extent?.EndPosition?.Position,
                Is.EqualTo(
                    expectedToken.Extent?.EndPosition?.Position
                ),
                LexerAssert.GetFailMessage($"actual End Position '{actualToken.Extent?.EndPosition?.Position}' does not match expected value '{expectedToken.Extent?.EndPosition?.Position}'", index)
            );
            Assert.That(
                actualToken.Extent?.EndPosition?.LineNumber,
                Is.EqualTo(
                    expectedToken.Extent?.EndPosition?.LineNumber
                ),
                LexerAssert.GetFailMessage($"actual End Line '{actualToken.Extent?.EndPosition?.LineNumber}' does not match expected value '{expectedToken.Extent?.EndPosition?.LineNumber}'", index)
            );
            Assert.That(
                actualToken.Extent?.EndPosition?.ColumnNumber,
                Is.EqualTo(
                    expectedToken.Extent?.EndPosition?.ColumnNumber
                ),
                LexerAssert.GetFailMessage($"actual End Column '{actualToken.Extent?.EndPosition?.ColumnNumber}' does not match expected value '{expectedToken.Extent?.EndPosition?.ColumnNumber}'", index)
            );
            Assert.That(
                    actualToken.Extent?.Text,
                Is.EqualTo(
                    expectedToken.Extent?.Text
                ),
                LexerAssert.GetFailMessage("actual Text does not match expected value", index)
            );
        }
        var tokensEqual = expectedToken switch
        {
            AliasIdentifierToken token =>
                TokenCompare.AreEqual(token, (AliasIdentifierToken?)actualToken, ignoreExtent),
            AttributeCloseToken token =>
                TokenCompare.AreEqual(token, (AttributeCloseToken?)actualToken, ignoreExtent),
            AttributeOpenToken token =>
                TokenCompare.AreEqual(token, (AttributeOpenToken?)actualToken, ignoreExtent),
            BlockCloseToken token =>
                TokenCompare.AreEqual(token, (BlockCloseToken?)actualToken, ignoreExtent),
            BlockOpenToken token =>
                TokenCompare.AreEqual(token, (BlockOpenToken?)actualToken, ignoreExtent),
            BooleanLiteralToken token =>
                TokenCompare.AreEqual(token, (BooleanLiteralToken?)actualToken, ignoreExtent),
            ColonToken token =>
                TokenCompare.AreEqual(token, (ColonToken?)actualToken, ignoreExtent),
            CommaToken token =>
                TokenCompare.AreEqual(token, (CommaToken?)actualToken, ignoreExtent),
            CommentToken token =>
                TokenCompare.AreEqual(token, (CommentToken?)actualToken, ignoreExtent),
            DotOperatorToken token =>
                TokenCompare.AreEqual(token, (DotOperatorToken?)actualToken, ignoreExtent),
            EqualsOperatorToken token =>
                TokenCompare.AreEqual(token, (EqualsOperatorToken?)actualToken, ignoreExtent),
            IdentifierToken token =>
                TokenCompare.AreEqual(token, (IdentifierToken?)actualToken, ignoreExtent),
            IntegerLiteralToken token =>
                TokenCompare.AreEqual(token, (IntegerLiteralToken?)actualToken, ignoreExtent),
            NullLiteralToken token =>
                TokenCompare.AreEqual(token, (NullLiteralToken?)actualToken, ignoreExtent),
            ParenthesisCloseToken token =>
                TokenCompare.AreEqual(token, (ParenthesisCloseToken?)actualToken, ignoreExtent),
            ParenthesisOpenToken token =>
                TokenCompare.AreEqual(token, (ParenthesisOpenToken?)actualToken, ignoreExtent),
            PragmaToken token =>
                TokenCompare.AreEqual(token, (PragmaToken?)actualToken, ignoreExtent),
            RealLiteralToken token =>
                TokenCompare.AreEqual(token, (RealLiteralToken?)actualToken, ignoreExtent),
            StatementEndToken token =>
                TokenCompare.AreEqual(token, (StatementEndToken?)actualToken, ignoreExtent),
            StringLiteralToken token =>
                TokenCompare.AreEqual(token, (StringLiteralToken?)actualToken, ignoreExtent),
            WhitespaceToken token =>
                TokenCompare.AreEqual(token, (WhitespaceToken?)actualToken, ignoreExtent),
            _ =>
                throw new NotImplementedException($"Cannot compare type '{expectedToken.GetType().Name}'")
        };
        if (!tokensEqual)
        {
            LexerAssert.Fail(
                 "Actual token does not match expected token ",
                $"{expectedToken.GetType().Name} (\"{LexerAssert.EscapeString(expectedToken.GetSourceString())}\")",
                $"{actualToken.GetType().Name} (\"{LexerAssert.EscapeString(actualToken.GetSourceString() ?? string.Empty )}\")",
                index
            );
        }
    }

    private static string EscapeString(string value)
    {
        var result = new StringBuilder();
        var mappings = new Dictionary<char, string> {
            {'\"', "\\\""},
            {'\\', "\\\\"},
            {'\a', "\\a"},
            {'\b', "\\b"},
            {'\f', "\\f"},
            {'\n', "\\n"},
            {'\r', "\\r"},
            {'\t', "\\t"},
            {'\v', "\\v"},
            {'\0', "\\0"},
        };
        foreach (var c in value.ToCharArray())
        {
            result.Append(
                mappings.TryGetValue(c, out var lookup)
                    ? lookup
                    : new([c])
            );
        }
        return result.ToString();
    }

    [DoesNotReturn]
    private static void Fail(string message, int? index)
    {
        LexerAssert.Fail(message, string.Empty, string.Empty, index);
    }

    [DoesNotReturn]
    private static void Fail(string message, string expected, string actual, int? index)
    {
        Assert.Fail(LexerAssert.GetFailMessage(message, expected, actual, index));
        // nullable workaround until Assert.Fail gets annotated with [DoesNotReturn]
        throw new InvalidOperationException();
    }

    private static string GetFailMessage(string message, int? index)
    {
        return LexerAssert.GetFailMessage(message, string.Empty, string.Empty, index);
    }

    private static string GetFailMessage(string message, string? expected, string? actual, int? index)
    {
        var error = new StringBuilder();
        error.Append(message);
        if (index.HasValue)
        {
            error.Append($" at index {index}");
        }
        error.AppendLine();
        if (expected is not null)
        {
            error.AppendLine($"Expected: {expected}");
        }
        if (actual is not null)
        {
            error.Append($"But was:  {actual}");
        }
        return error.ToString();
    }

}
