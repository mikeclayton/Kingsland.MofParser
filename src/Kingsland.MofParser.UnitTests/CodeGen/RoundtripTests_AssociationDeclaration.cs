﻿using Kingsland.MofParser.Ast;
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
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void AssociationDeclarationWithSuperAssociationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                association GOLF_MemberLocker : GOLF_Base
                {
                    GOLF_ClubMember REF Member;
                    GOLF_Locker REF Locker;
                    GOLF_Date AssignedOnDate;
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
                //     GOLF_Date AssignedOnDate;
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(" ")
                .IdentifierToken("AssignedOnDate")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst(
                new AssociationDeclarationAst(
                    "GOLF_MemberLocker", "GOLF_Base",
                    [
                        new PropertyDeclarationAst(
                            "GOLF_ClubMember", "REF", "Member"
                        ),
                        new PropertyDeclarationAst(
                            "GOLF_Locker", "REF", "Locker"
                        ),
                        new PropertyDeclarationAst(
                            "GOLF_Date", "AssignedOnDate"
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void AssociationDeclarationWithClassFeaturesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                association GOLF_MemberLocker
                {
                    GOLF_ClubMember REF Member;
                    GOLF_Locker REF Locker;
                    GOLF_Date AssignedOnDate;
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
                //     GOLF_Date AssignedOnDate
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(" ")
                .IdentifierToken("AssignedOnDate")
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
                            "GOLF_Date", "AssignedOnDate"
                        )
                    ]
                )
            );
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

    }

    #endregion

}
