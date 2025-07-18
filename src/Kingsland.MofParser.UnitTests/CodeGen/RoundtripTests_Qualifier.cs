﻿using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.4 Qualifiers

    public static class QualifierTests
    {

        [Test]

        public static void QualifierShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                [Description(""Instances of this class represent golf clubs. A golf club is "" ""an organization that provides member services to golf players "" ""both amateur and professional."")]
                class GOLF_Club : GOLF_Base
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // [Description("Instances of this class represent golf clubs. A golf club is " "an organization that provides member services to golf players " "both amateur and professional.")]
                .AttributeOpenToken()
                .IdentifierToken("Description")
                .ParenthesisOpenToken()
                .StringLiteralToken("Instances of this class represent golf clubs. A golf club is ")
                .WhitespaceToken(" ")
                .StringLiteralToken("an organization that provides member services to golf players ")
                .WhitespaceToken(" ")
                .StringLiteralToken("both amateur and professional.")
                .ParenthesisCloseToken()
                .AttributeCloseToken()
                .WhitespaceToken(newline)
                // class GOLF_Club : GOLF_Base\r\n
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Club")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new ClassDeclarationAst(
                    [
                        new("Description",
                            new QualifierValueInitializerAst(
                                "Instances of this class represent golf clubs. A golf club is ",
                                "an organization that provides member services to golf players ",
                                "both amateur and professional."
                            )
                        )
                    ],
                    "GOLF_Club", "GOLF_Base"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

    }

    #endregion

}
