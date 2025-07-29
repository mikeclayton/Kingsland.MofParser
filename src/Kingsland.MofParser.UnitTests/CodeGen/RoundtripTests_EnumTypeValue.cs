using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.3 Enum type value

    public static class EnumTypeValueTests
    {

        [Test]
        public static void EnumTypeValueWithEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = July;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = July;
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("July")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueAst("July"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValue("July"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void EnumTypeValueWithSingleItemEnumValueArrayShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {June};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = {June};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("June")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueArrayAst("June"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValueArray("June"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void EnumTypeValueWithMultipleItemEnumValueArrayShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {June, July};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = {June};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("June")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("July")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueArrayAst("June", "July"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValueArray("June", "July"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    public static class EnumValueTests
    {

        [Test]
        public static void UnqualifiedEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = July;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = July;
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("July")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueAst("July"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValue("July"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    public static class EnumValueArrayTests
    {

        [Test]
        public static void EmptyEnumValueArrayShouldRoundtrip()
        {
            // note - this parses as an array of *literal* values rather than
            // an array of *enum* values as there's no schema available for the
            // class that tells us the return type of the "Month" property
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = {};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new LiteralValueArrayAst())
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new LiteralValueArray())
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void EnumValueArrayWithSingleEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {June};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = {June};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("June")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueArrayAst("June"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValueArray("June"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void EnumValueArrayWithMultipleEnumValuesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {January, February};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = {January, February};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("January")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("February")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueArrayAst("January", "February"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValueArray("January", "February"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
        public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksEnabledShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {MonthEnums.July};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Month = {MonthEnums.July};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("MonthEnums")
                .DotOperatorToken()
                .IdentifierToken("July")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_Date",
                    [
                        new("Month", new EnumValueArrayAst([new("MonthEnums", "July")]))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_Date",
                    [
                        new("Month", new EnumValueArray([new("MonthEnums", "July")]))
                    ]
                )
            );
            var quirks = ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames;
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule, quirks);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
        public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksDisabledShouldThrow()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                instance of GOLF_Date
                {
                    Month = {MonthEnums.July};
                };
            ".TrimIndent(newline).TrimString(newline);
            var errorline = 3;
            var expectedMessage = @$"
                Unexpected token found at Position {45 + (errorline - 1) * newline.Length}, Line Number {errorline}, Column Number 24.
                Token Type: '{nameof(DotOperatorToken)}'
                Token Text: '.'
            ".TrimIndent(newline).TrimString(newline);
            RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
        }

    }

    #endregion

}
