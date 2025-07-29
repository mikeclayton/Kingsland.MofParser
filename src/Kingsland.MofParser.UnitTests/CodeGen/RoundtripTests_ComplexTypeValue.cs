using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Language;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.9 Complex type value

    public static class ComplexTypeValueTests
    {

        [Test]
        public static void ComplexTypeValueWithComplexValuePropertyShouldRoundtrip()
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
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", new AliasValue("MyAliasIdentifier"))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void ComplexTypeValueWithComplexValueArrayPropertyShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {$MyAliasIdentifier};
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
                .BlockOpenToken()
                .AliasIdentifierToken("MyAliasIdentifier")
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
                        new("LastPaymentDate", new AliasIdentifierToken[] { "MyAliasIdentifier" })
                    ],
                    ";"
                )
            );
            var expectedModule = new Module(
                new InstanceValue(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", new ComplexValueArray(
                            new AliasValue("MyAliasIdentifier")
                        ))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void ComplexTypeValueAsDefaultValueOfPropertyShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    GOLF_Id InstanceID = value of GOLF_Id
                    {
                        Id = 12345;
                    };
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class GOLF_Base
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Id InstanceID = value of GOLF_Id
                .IdentifierToken("GOLF_Id")
                .WhitespaceToken(" ")
                .IdentifierToken("InstanceID")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("value")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Id")
                .WhitespaceToken(newline + indent)
                //     {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent + indent)
                //         Id = 12345;
                .IdentifierToken("Id")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 12345)
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
                new ClassDeclarationAst(
                    "GOLF_Base",
                    [
                        new PropertyDeclarationAst(
                            "GOLF_Id", "InstanceID",
                            new ComplexValueAst(
                                "value", "of", "GOLF_Id",
                                [
                                    new("Id", 12345)
                                ]
                            )
                        )
                    ]
                )
            );
            var expectedModule = new Module(
                new Class(
                    "GOLF_Base",
                    [
                        new Property(
                            "GOLF_Id", "InstanceID",
                            new StructureValue(
                                "GOLF_Id",
                                [
                                    new("Id", new IntegerValue(12345))
                                ]
                            )
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    #endregion

}
