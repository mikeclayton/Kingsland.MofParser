﻿using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.9 Complex type value

    public static class ComplexValueTests
    {

        [Test]
        public static void ComplexValuePropertyShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = $MyAliasIdentifier;
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
                //     LastPaymentDate = $MyAliasIdentifier;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .AliasIdentifierToken("MyAliasIdentifier")
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
                        new("LastPaymentDate", new AliasIdentifierToken("MyAliasIdentifier"))
                    ],
                    ";"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void ComplexValuePropertyWithValueOfShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = value of GOLF_Date
                    {
                        Month = July;
                    };
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
                //     LastPaymentDate = value of GOLF_Date
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("value")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(newline + indent)
                //     {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent + indent)
                //         Month = July;
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("July")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     };
                .BlockCloseToken()
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
                        new(
                            "LastPaymentDate",
                            new ComplexValueAst(
                                "value", "of", "GOLF_Date",
                                [
                                    new("Month", new EnumValueAst("July"))
                                ]
                            )
                        )
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                [
                    new Instance(
                        "GOLF_ClubMember",
                        [
                            new Property(
                                "LastPaymentDate",
                                new ComplexValueObject(
                                    "GOLF_Date",
                                    [
                                        new("Month", new EnumValue("July"))
                                    ]
                                )
                            )
                        ]
                    )
                ]
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    #endregion

}
