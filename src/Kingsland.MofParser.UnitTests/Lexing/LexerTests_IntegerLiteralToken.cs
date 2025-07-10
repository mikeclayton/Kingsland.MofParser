using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Lexing;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

[TestFixture]
public static partial class LexerTests
{

    [TestFixture]
    public static class ReadIntegerLiteralTokenMethod
    {

        // binaryValue

        [Test]
        public static void ShouldReadBinaryValue0b()
        {
            var sourceText = "0b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(1, 1, 2),
                    sourceText, IntegerKind.BinaryValue, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadBinaryValue1b()
        {
            var sourceText = "1b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(1, 1, 2),
                    sourceText, IntegerKind.BinaryValue, 1
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadBinaryValue0000b()
        {
            var sourceText = "0000b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                   sourceText, IntegerKind.BinaryValue, 0b0000
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadBinaryValue1000b()
        {
            var sourceText = "1000b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.BinaryValue, 0b1000
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadBinaryValuePlus1000b()
        {
            var sourceText = "+1000b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.BinaryValue, 0b1000
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadBinaryValueMinus1000b()
        {
            var sourceText = "-1000b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.BinaryValue, -0b1000
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadBinaryValue1111b()
        {
            var sourceText = "1111b";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.BinaryValue, 0b1111
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        // octalValue

        [Test]
        public static void ShouldReadOctalValue00()
        {
            var sourceText = "00";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(1, 1, 2),
                    sourceText, IntegerKind.OctalValue, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValue01()
        {
            var sourceText = "01";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(1, 1, 2),
                    sourceText, IntegerKind.OctalValue, 1
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValue00000()
        {
            var sourceText = "00000";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.OctalValue, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValue01000()
        {
            var sourceText = "01000";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.OctalValue, 512
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValuePlus01000()
        {
            var sourceText = "+01000";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.OctalValue, 512
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValueMinus01000()
        {
            var sourceText = "-01000";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.OctalValue, -512
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValue01111()
        {
            var sourceText = "01111";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.OctalValue, 585
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValue04444()
        {
            var sourceText = "04444";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.OctalValue, 2340
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadOctalValue07777()
        {
            var sourceText = "07777";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.OctalValue, 4095
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldThrowOctalValue09()
        {
            // make sure strings of more than one character that start with a leading "0" are *not* parsed as decimal values
            // (i.e. "09" is not a valid octal value because of the "9", but could potentially parse as 9 (decimal) if not handled correctly)
            var sourceText = "09";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    sourceText, IntegerKind.DecimalValue, 0
                )
                .ToList();
            var ex = Assert.Throws<UnexpectedCharacterException>(
                () => LexerTests.AssertLexerTest(sourceText, expectedTokens)
            ) ?? throw new InvalidOperationException();
            Assert.That(ex.Message, Is.EqualTo("Unexpected character '9' found at Position 1, Line Number 1, Column Number 2"));
        }

        // hexValue

        [Test]
        public static void ShouldReadHexValue0x0()
        {
            var sourceText = "0x0";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(2, 1, 3),
                    sourceText, IntegerKind.HexValue, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadHexValue0x0000()
        {
            var sourceText = "0x0000";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.HexValue, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadHexValue0x8888()
        {
            var sourceText = "0x8888";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.HexValue, 0x8888
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadHexValuePlus0x8888()
        {
            var sourceText = "+0x8888";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(6, 1, 7),
                    sourceText, IntegerKind.HexValue, 0x8888
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadHexValueMinus0x8888()
        {
            var sourceText = "-0x8888";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(6, 1, 7),
                    sourceText, IntegerKind.HexValue, -0x8888
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadHexValue0xabcd()
        {
            var sourceText = "0xabcd";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.HexValue, 0xabcd
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadHexValue0xABCD()
        {
            var sourceText = "0xABCD";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.HexValue, 0xABCD
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        // decimalValue

        [Test]
        public static void ShouldReadDecimalValue0()
        {
            var sourceText = "0";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    sourceText, IntegerKind.DecimalValue, 0
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadDecimalValue12345()
        {
            var sourceText = "12345";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    sourceText, IntegerKind.DecimalValue, 12345
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadDecimalValuePlus12345()
        {
            var sourceText = "+12345";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.DecimalValue, 12345
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadDecimalValueMinus12345()
        {
            var sourceText = "-12345";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(5, 1, 6),
                    sourceText, IntegerKind.DecimalValue, -12345
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadDecimalValue1234567890()
        {
            var sourceText = "1234567890";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(9, 1, 10),
                   sourceText, IntegerKind.DecimalValue, 1234567890
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

}
