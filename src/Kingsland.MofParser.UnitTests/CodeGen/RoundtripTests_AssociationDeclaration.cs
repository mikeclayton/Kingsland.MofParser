using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.3 Association declaration

    public static class AssociationDeclarationTests
    {

        [Test]
        public static void EmptyAssociationDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                association GOLF_MemberLocker
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // association GOLF_MemberLocker
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new AssociationDeclarationAst(
                    "GOLF_MemberLocker"
                )
            );
            var expectedModule = new Module(
                new Association(
                    "GOLF_MemberLocker"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void AssociationDeclarationWithSuperAssociationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                association GOLF_MemberLocker : GOLF_Base
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // association GOLF_MemberLocker : GOLF_Base
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
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
                new AssociationDeclarationAst(
                    "GOLF_MemberLocker", "GOLF_Base"
                )
            );
            var expectedModule = new Module(
                new Association(
                    "GOLF_MemberLocker", "GOLF_Base"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void AssociationDeclarationWithQualifiersShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                [Abstract, OCL{""-- the key property cannot be NULL"", ""inv: InstanceId.size() = 10""}]
                association GOLF_MemberLocker
                {
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
                // association GOLF_MemberLocker
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new AssociationDeclarationAst(
                    [
                        new("Abstract"),
                        new("OCL", new QualifierValueArrayInitializerAst("-- the key property cannot be NULL", "inv: InstanceId.size() = 10"))
                    ],
                    "GOLF_MemberLocker"
                )
            );
            var expectedModule = new Module(
                new Association(
                    [
                        new("Abstract"),
                        new("OCL", new LiteralValueArray("-- the key property cannot be NULL", "inv: InstanceId.size() = 10"))
                    ],
                    "GOLF_MemberLocker"
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void AssociationDeclarationWithPropertyDeclarationsShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                association GOLF_MemberLocker
                {
                    GOLF_ClubMember REF Member;
                    GOLF_Locker REF Locker;
                    GOLF_Date AssignedOnDate = Null;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // association GOLF_MemberLocker
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_ClubMember REF Member;
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(" ")
                .IdentifierToken("REF")
                .WhitespaceToken(" ")
                .IdentifierToken("Member")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Locker REF Locker;
                .IdentifierToken("GOLF_Locker")
                .WhitespaceToken(" ")
                .IdentifierToken("REF")
                .WhitespaceToken(" ")
                .IdentifierToken("Locker")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Date AssignedOnDate = Null
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(" ")
                .IdentifierToken("AssignedOnDate")
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
                new AssociationDeclarationAst(
                    "GOLF_MemberLocker",
                    [
                        new PropertyDeclarationAst(
                            "GOLF_ClubMember", "REF", "Member"
                        ),
                        new PropertyDeclarationAst(
                            "GOLF_Locker", "REF", "Locker"
                        ),
                        new PropertyDeclarationAst(
                            "GOLF_Date", "AssignedOnDate", NullValueAst.Null
                        )
                    ]
                )
            );
            var expectedModule = new Module(
                new Association(
                    "GOLF_MemberLocker",
                    [
                        new Property(
                            "GOLF_ClubMember", true, "Member"
                        ),
                        new Property(
                            "GOLF_Locker", true, "Locker"
                        ),
                        new Property(
                            "GOLF_Date", "AssignedOnDate", NullValue.Null
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

    }

    #endregion

}
