﻿using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class TokenCompare
{

    #region Token Comparison Methods

    internal static bool AreEqual(AliasIdentifierToken? expected, AliasIdentifierToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                   (expected.Name == actual.Name);
        }
    }

    internal static bool AreEqual(AttributeCloseToken? expected, AttributeCloseToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(AttributeOpenToken? expected, AttributeOpenToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(BlockCloseToken? expected, BlockCloseToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(BlockOpenToken? expected, BlockOpenToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(BooleanLiteralToken? expected, BooleanLiteralToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                   (expected.Value == actual.Value);
        }
    }

    internal static bool AreEqual(ColonToken? expected, ColonToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(CommaToken? expected, CommaToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(CommentToken? expected, CommentToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(DotOperatorToken? expected, DotOperatorToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(EqualsOperatorToken? expected, EqualsOperatorToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(IdentifierToken? expected, IdentifierToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                   (expected.Name == actual.Name);
        }
    }

    internal static bool AreEqual(IntegerLiteralToken? expected, IntegerLiteralToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                (expected.Kind == actual.Kind) &&
                (expected.Value == actual.Value);
        }
    }

    internal static bool AreEqual(NullLiteralToken? expected, NullLiteralToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(ParenthesisCloseToken? expected, ParenthesisCloseToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(ParenthesisOpenToken? expected, ParenthesisOpenToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(PragmaToken? expected, PragmaToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(RealLiteralToken? expected, RealLiteralToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                (expected.Value == actual.Value);
        }
    }

    internal static bool AreEqual(StatementEndToken? expected, StatementEndToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent));
        }
    }

    internal static bool AreEqual(StringLiteralToken? expected, StringLiteralToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                (expected.Value == actual.Value);
        }
    }

    internal static bool AreEqual(WhitespaceToken? expected, WhitespaceToken? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (ignoreExtent || TokenCompare.AreEqual(expected.Extent, actual.Extent)) &&
                (expected.Value == actual.Value);
        }
    }

    #endregion

    #region Helper Methods

    private static bool AreEqual(SourceExtent? expected, SourceExtent? actual)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return TokenCompare.AreEqual(expected.StartPosition, actual.StartPosition) &&
                TokenCompare.AreEqual(expected.EndPosition, actual.EndPosition) &&
                (expected.Text == actual.Text);
        }
    }

    private static bool AreEqual(SourcePosition? expected, SourcePosition? actual)
    {
        if ((expected == null) && (actual == null))
        {
            return true;
        }
        else if ((expected == null) || (actual == null))
        {
            return false;
        }
        else
        {
            return (expected.Position == actual.Position) &&
                (expected.LineNumber == actual.LineNumber) &&
                (expected.ColumnNumber == actual.ColumnNumber);
        }
    }

    #endregion

}
