using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.5 Property declaration

    public static class PropertyDeclarationTests
    {

        [Test]
        public static void PropertyDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer Severity;
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
               //     Integer Severity;
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("Severity")
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
                            "Integer", "Severity"
                        )
                    ]
                )
            );
            var expectedModule = new Module(
                new Class(
                    "GOLF_Base",
                    [
                        new Property("Integer", "Severity")
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer Severity[];
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
               //     Integer Severity[];
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("Severity")
               .AttributeOpenToken()
               .AttributeCloseToken()
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
                            "Integer", "Severity", true
                        )
                    ]
                )
            );
            var expectedModule = new Module(
                new Class(
                    "GOLF_Base",
                    [
                        new Property("Integer", "Severity", true)
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer Severity = 0;
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
                //     Integer Severity = 0;
                .IdentifierToken("Integer")
                .WhitespaceToken(" ")
                .IdentifierToken("Severity")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 0)
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
                            "Integer", "Severity", 0
                        )
                    ]
                )
            );
            var expectedModule = new Module(
                new Class(
                    "GOLF_Base",
                    [
                        new Property("Integer", "Severity", 0)
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void PropertyDeclarationWithRefTypeShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer REF Severity = {1, 2, 3};
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
               //     Integer REF Severity = {1, 2, 3};
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("REF")
               .WhitespaceToken(" ")
               .IdentifierToken("Severity")
               .WhitespaceToken(" ")
               .EqualsOperatorToken()
               .WhitespaceToken(" ")
               .BlockOpenToken()
               .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
               .CommaToken()
               .WhitespaceToken(" ")
               .IntegerLiteralToken(IntegerKind.DecimalValue, 2)
               .CommaToken()
               .WhitespaceToken(" ")
               .IntegerLiteralToken(IntegerKind.DecimalValue, 3)
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
                            "Integer", "REF", "Severity", new LiteralValueArrayAst(1, 2, 3)
                        )
                    ]
                )
            );
            var expectedModule = new Module(
                new Class(
                    "GOLF_Base",
                    [
                        new Property("Integer", true, "Severity", new LiteralValueArray(1, 2, 3))
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
        public static void PropertyDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    uint8 SeverityUint8;
                    uint16 SeverityUint16;
                    uint32 SeverityUint32;
                    uint64 SeverityUint64;
                    sint8 SeveritySint8;
                    sint16 SeveritySint16;
                    sint32 SeveritySint32;
                    sint64 SeveritySint64;
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
                //     uint8 SeverityUint8;
                .IdentifierToken("uint8")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint8")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     uint16 SeverityUint16;
                .IdentifierToken("uint16")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint16")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     uint32 SeverityUint32;
                .IdentifierToken("uint32")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint32")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     uint64 SeverityUint64;
                .IdentifierToken("uint64")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint64")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint8 SeveritySint8;
                .IdentifierToken("sint8")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint8")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint16 SeveritySint16;
                .IdentifierToken("sint16")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint16")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint32 SeveritySint32;
                .IdentifierToken("sint32")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint32")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint64 SeveritySint64;
                .IdentifierToken("sint64")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint64")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            var expectedAst = new MofSpecificationAst(
                new ClassDeclarationAst(
                    "GOLF_Base",
                    new PropertyDeclarationAst[] {
                        new("uint8", "SeverityUint8"),
                        new("uint16", "SeverityUint16"),
                        new("uint32", "SeverityUint32"),
                        new("uint64", "SeverityUint64"),
                        new("sint8", "SeveritySint8"),
                        new("sint16", "SeveritySint16"),
                        new("sint32", "SeveritySint32"),
                        new("sint64", "SeveritySint64")
                    }
                )
            );
            var expectedModule = new Module(
                new Class(
                    "GOLF_Base",
                    [
                        new Property("uint8", "SeverityUint8"),
                        new Property("uint16", "SeverityUint16"),
                        new Property("uint32", "SeverityUint32"),
                        new Property("uint64", "SeverityUint64"),
                        new Property("sint8", "SeveritySint8"),
                        new Property("sint16", "SeveritySint16"),
                        new Property("sint32", "SeveritySint32"),
                        new Property("sint64", "SeveritySint64")
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    [Test]
    public static void PropertyDeclarationsWithQualifierListShouldRoundtrip()
    {
        var newline = Environment.NewLine;
        var indent = "    ";
        var sourceText = @"
                class GOLF_Base
                {
                    [Description(""an instance of a class that derives from the GOLF_Base class. ""), Key] string InstanceID;
                    [Description(""A short textual description (one- line string) of the""), MaxLen(64)] string Caption = Null;
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
        var expectedModule = new Module(
            new Class(
                "GOLF_Base",
                [
                    new Property(
                        [
                            new("Description", new StringValue("an instance of a class that derives from the GOLF_Base class. ")),
                            new("Key")
                        ],
                        "string", "InstanceID"
                    ),
                    new Property(
                        [
                            new("Description", new StringValue("A short textual description (one- line string) of the")),
                            new("MaxLen", new IntegerValue(64))
                        ],
                        "string", "Caption", NullValue.Null
                    )
                ]
            )
        );
        RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
    }

    #endregion

}
