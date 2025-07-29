using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.1.3 String values

    public static class StringValueTests
    {

        [Test]
        public static void SingleStringValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    Caption = ""Instance of John Doe"";
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Caption = "Instance of John Doe";
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("Instance of John Doe")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_ClubMember",
                    [
                        new("Caption", "Instance of John Doe")
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_ClubMember",
                    [
                        new("Caption", "Instance of John Doe")
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void MultistringValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    Caption = ""Instance"" ""of"" ""John"" ""Doe"";
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Caption = "Instance" "of" "John" "Doe";
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("Instance")
                .WhitespaceToken(" ")
                .StringLiteralToken("of")
                .WhitespaceToken(" ")
                .StringLiteralToken("John")
                .WhitespaceToken(" ")
                .StringLiteralToken("Doe")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_ClubMember",
                    [
                        new("Caption", new StringValueAst("Instance", "of", "John", "Doe"))
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_ClubMember",
                    [
                        new("Caption", new StringValue("Instance", "of", "John", "Doe"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/20")]
        public static void StringValueWithSingleQuoteShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    Caption = ""Instance of John Doe\'s GOLF_ClubMember object"";
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Caption = "Instance of John Doe's GOLF_ClubMember object";
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("Instance of John Doe's GOLF_ClubMember object")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new InstanceValueDeclarationAst(
                    "instance", "of", "GOLF_ClubMember",
                    [
                        new("Caption", "Instance of John Doe's GOLF_ClubMember object")
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_ClubMember",
                    [
                        new("Caption", "Instance of John Doe's GOLF_ClubMember object")
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    #endregion

}
