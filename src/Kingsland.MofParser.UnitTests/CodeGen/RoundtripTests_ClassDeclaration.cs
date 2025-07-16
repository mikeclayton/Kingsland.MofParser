using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.2 Class declaration

    public static class ClassDeclarationTests
    {

        [Test]
        public static void EmptyClassDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                class GOLF_Base
                {
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
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new ClassDeclarationAst(
                    "GOLF_Base"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void ClassDeclarationWithSuperclassShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                class GOLF_Base : GOLF_Superclass
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class GOLF_Base : GOLF_Superclass
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Superclass")
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
                    "GOLF_Base", "GOLF_Superclass"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void ClassDeclarationWithClassFeaturesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    string InstanceID;
                    string Caption = Null;
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
                //     string InstanceID;
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("InstanceID")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     string Caption = Null;
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .NullLiteralToken("Null")
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
                            "string", "InstanceID"
                        ),
                        new PropertyDeclarationAst(
                            "string", "Caption",
                            NullValueAst.Null
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void ClassDeclarationsWithQualifierListShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                [Abstract, OCL{""-- the key property cannot be NULL"", ""inv: InstanceId.size() = 10""}]
                class GOLF_Base
                {
                    [Description(""an instance of a class that derives from the GOLF_Base class. ""), Key] string InstanceID;
                    [Description(""A short textual description (one- line string) of the""), MaxLen(64)] string Caption = Null;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // [Abstract, OCL{"-- the key property cannot be NULL", "inv: InstanceId.size() = 10"}]
                .AttributeOpenToken()
                .IdentifierToken("Abstract")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("OCL")
                .BlockOpenToken()
                .StringLiteralToken("-- the key property cannot be NULL")
                .CommaToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("inv: InstanceId.size() = 10")
                .BlockCloseToken()
                .AttributeCloseToken()
                .WhitespaceToken(newline)
                // class GOLF_Base
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     [Description("an instance of a class that derives from the GOLF_Base class. "), Key] string InstanceID;;
                .AttributeOpenToken()
                .IdentifierToken("Description")
                .ParenthesisOpenToken()
                .StringLiteralToken("an instance of a class that derives from the GOLF_Base class. ")
                .ParenthesisCloseToken()
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("Key")
                .AttributeCloseToken()
                .WhitespaceToken(" ")
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("InstanceID")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     [Description("A short textual description (one- line string) of the"), MaxLen(64)] string Caption = Null;
                .AttributeOpenToken()
                .IdentifierToken("Description")
                .ParenthesisOpenToken()
                .StringLiteralToken("A short textual description (one- line string) of the")
                .ParenthesisCloseToken()
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("MaxLen")
                .ParenthesisOpenToken()
                .IntegerLiteralToken(IntegerKind.DecimalValue, 64)
                .ParenthesisCloseToken()
                .AttributeCloseToken()
                .WhitespaceToken(" ")
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .NullLiteralToken("Null")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new ClassDeclarationAst(
                    [
                        new("Abstract"),
                        new("OCL", new QualifierValueArrayInitializerAst("-- the key property cannot be NULL", "inv: InstanceId.size() = 10"))
                    ],
                    "GOLF_Base",
                    [
                        new PropertyDeclarationAst(
                            [
                                new("Description", "an instance of a class that derives from the GOLF_Base class. "),
                                new("Key")
                            ],
                            "string", "InstanceID"
                        ),
                        new PropertyDeclarationAst(
                            [
                                new("Description", "A short textual description (one- line string) of the"),
                                new("MaxLen", IntegerKind.DecimalValue, 64)
                            ],
                            "string", "Caption",
                            NullValueAst.Null
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

    }

    #endregion

}
