using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Lexing;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

[TestFixture]
public static partial class LexerTests
{

    [TestFixture]
    public static class ReadRealLiteralTokenMethod
    {

        [Test]
        public static void ShouldReadRealValue0_0()
        {
            var sourceText = "0.0";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(2, 1, 3),
                    sourceText, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValue000_000()
        {
            // realValues allow multiple leading zeros
            var sourceText = "000.000";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(6, 1, 7),
                    sourceText, 0.0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValue123_45()
        {
            var sourceText = "123.45";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, 123.45
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValuePlus123_45()
        {
            var sourceText = "+123.45";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(6, 1, 7),
                    sourceText, 123.45
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueMinus123_45()
        {
            var sourceText = "-123.45";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(6, 1, 7),
                    sourceText, -123.45
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValue1234567890_00()
        {
            var sourceText = "1234567890.00";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(12, 1, 13),
                    sourceText, 1234567890.00
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValue_45()
        {
            var sourceText = ".45";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(2, 1, 3),
                    sourceText, 0.45
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValuePlus_45()
        {
            var sourceText = "+.45";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(3, 1, 4),
                    sourceText, 0.45
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueMinus_45()
        {
            var sourceText = "-.45";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(3, 1, 4),
                    sourceText, -0.45
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueExponent_9_236_E_000()
        {
            var sourceText = "9.236E000";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(8, 1, 9),
                    sourceText, 9.236 * Math.Pow(10, 0)
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueExponent_9_236_E_005()
        {
            var sourceText = "9.236E005";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(8, 1, 9),
                    sourceText, 9.236 * Math.Pow(10, 5)
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueExponent_9_236_E_Plus_005()
        {
            var sourceText = "9.236E+005";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(9, 1, 10),
                    sourceText, 9.236 * Math.Pow(10, 5)
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueExponent_9236_E_Minus_005()
        {
            var sourceText = "923600.0E-005";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(12, 1, 13),
                    sourceText, 923600 * Math.Pow(10, -5)
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadRealValueExponent_GithubIssue85()
        {
            // see https://github.com/mikeclayton/Kingsland.MofParser/issues/85
            var sourceText = "9.2360000000000007000000000000000000000000000000000000E+000";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(58, 1, 59),
                    sourceText, 9.2360000000000007
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldThrowRealValueExponentNoDecimalPoint()
        {
            // real values *must* have a decimal point, so the following should throw an exception
            // because "92360" is a *decimal* value, not a real value, but there's also an exponent
            // which is only valid for real values
            var sourceText = "9.236E+005";
            var expectedTokens = new TokenBuilder()
                .RealLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(9, 1, 10),
                    sourceText, 9.236 * Math.Pow(10, 5)
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

}
