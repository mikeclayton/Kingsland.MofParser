﻿using Kingsland.MofParser.Ast;
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
                    null, new("GOLF_Base"), null,
                    null
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
                    null, new("GOLF_Base"), new("GOLF_Superclass"),
                    null
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
                    null, new("GOLF_Base"), null,
                    [
                        new PropertyDeclarationAst(
                            null, new("string"), null, new("InstanceID"), null, null
                        ),
                        new PropertyDeclarationAst(
                            null, new("string"), null, new("Caption"), null,
                            new NullValueAst(
                                new("Null")
                            )
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
                    null, new("GOLF_Base"), null,
                    [
                        new PropertyDeclarationAst(
                            new QualifierListAst(
                                new QualifierValueAst(
                                    new("Description"),
                                    new QualifierValueInitializerAst(
                                        new StringValueAst(
                                            new StringLiteralToken("an instance of a class that derives from the GOLF_Base class. "),
                                            "an instance of a class that derives from the GOLF_Base class. "
                                        )
                                    ),
                                    null
                                ),
                                new QualifierValueAst(
                                    new("Key"), null, null
                                )
                            ),
                            new("string"), null, new("InstanceID"), null, null
                        ),
                        new PropertyDeclarationAst(
                            new QualifierListAst(
                                new QualifierValueAst(
                                    new("Description"),
                                    new QualifierValueInitializerAst(
                                        new StringValueAst(
                                            new StringLiteralToken("A short textual description (one- line string) of the"),
                                            "an instance of a class that derives from the GOLF_Base class. "
                                        )
                                    ),
                                    null
                                ),
                                new QualifierValueAst(
                                    new IdentifierToken("MaxLen"),
                                    new QualifierValueInitializerAst(
                                        new IntegerValueAst(
                                            new IntegerLiteralToken(IntegerKind.DecimalValue, 64)
                                        )
                                    ),
                                    null
                                )
                            ),
                            new("string"), null, new("Caption"), null,
                            new NullValueAst(
                                new NullLiteralToken("Null")
                            )
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

    }

    #endregion

}
