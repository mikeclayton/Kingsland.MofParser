using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.3 Compiler directives

    public static class CompilerDirectiveTests
    {

        [Test]
        public static void CompilerDirectiveShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                #pragma include (""GlobalStructs/GOLF_Address.mof"")
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // #pragma include ("GlobalStructs/GOLF_Address.mof")
                .PragmaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("include")
                .WhitespaceToken(" ")
                .ParenthesisOpenToken()
                .StringLiteralToken("GlobalStructs/GOLF_Address.mof")
                .ParenthesisCloseToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new CompilerDirectiveAst(
                    "pragma", "include",
                    "GlobalStructs/GOLF_Address.mof"
                )
            );
            var expectedModule = new Module(
                new Pragma(
                    "include",
                    "GlobalStructs/GOLF_Address.mof"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void CompilerDirectiveWithMultipleSingleStringsShouldRoundtrip() {
            var newline = Environment.NewLine;
            var sourceText = @"
                #pragma include (""GlobalStructs"" ""/"" ""GOLF_Address.mof"")
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // #pragma include ("GlobalStructs" "/" "GOLF_Address.mof")
                .PragmaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("include")
                .WhitespaceToken(" ")
                .ParenthesisOpenToken()
                .StringLiteralToken("GlobalStructs")
                .WhitespaceToken(" ")
                .StringLiteralToken("/")
                .WhitespaceToken(" ")
                .StringLiteralToken("GOLF_Address.mof")
                .ParenthesisCloseToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new CompilerDirectiveAst(
                    "pragma", "include",
                    [ "GlobalStructs", "/", "GOLF_Address.mof" ]
                )
            );
            var expectedModule = new Module(
                new Pragma(
                    "include",
                    "GlobalStructs/GOLF_Address.mof"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    #endregion

}
