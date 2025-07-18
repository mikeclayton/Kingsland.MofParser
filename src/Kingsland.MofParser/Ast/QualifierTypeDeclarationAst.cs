﻿using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <returns>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.4 Qualifiers
///
///     qualifierTypeDeclaration = [ qualifierList ] QUALIFIER qualifierName ":"
///                                qualifierType qualifierScope
///                                [ qualifierPolicy ] ";"
///
///     qualifierName            = elementName
///
///     qualifierType            = primitiveQualifierType / enumQualiferType
///
///     primitiveQualifierType   = primitiveType [ array ]
///                                [ "=" primitiveTypeValue ] ";"
///
///     enumQualiferType         = enumName [ array ] "=" enumTypeValue ";"
///
///     qualifierScope           = SCOPE "(" ANY / scopeKindList ")"
///
/// </returns>
public sealed record QualifierTypeDeclarationAst : MofProductionAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
            this.Flavors = [];
        }

        [PublicAPI]
        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? QualifierKeyword
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? QualifierName
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? QualifierType
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? QualifierScope
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? QualifierPolicy
        {
            get;
            set;
        }

        [PublicAPI]
        public List<string> Flavors
        {
            get;
            set;
        }

        [PublicAPI]
        public QualifierTypeDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.QualifierKeyword ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierKeyword)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierName ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierName)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierType ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierType)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierScope ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierScope)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierPolicy ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierPolicy)} property must be set before calling {nameof(Build)}."
                ),
                this.Flavors
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierTypeDeclarationAst(
        QualifierListAst qualifierList,
        IdentifierToken qualifierKeyword,
        IdentifierToken qualifierName,
        IdentifierToken qualifierType,
        IdentifierToken qualifierScope,
        IdentifierToken qualifierPolicy,
        IEnumerable<string> flavors
    )
    {
        this.QualifierList = qualifierList ?? throw new ArgumentNullException(nameof(qualifierList));
        this.QualifierKeyword = qualifierKeyword ?? throw new ArgumentNullException(nameof(qualifierKeyword));
        this.QualifierName = qualifierName ?? throw new ArgumentNullException(nameof(qualifierName));
        this.QualifierType = qualifierType ?? throw new ArgumentNullException(nameof(qualifierType));
        this.QualifierScope = qualifierScope ?? throw new ArgumentNullException(nameof(qualifierScope));
        this.QualifierPolicy = qualifierPolicy ?? throw new ArgumentNullException(nameof(qualifierPolicy));
        this.Flavors = (flavors ?? throw new ArgumentNullException(nameof(flavors)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken QualifierKeyword
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken QualifierName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken QualifierType
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken QualifierScope
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken QualifierPolicy
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<string> Flavors
    {
        get;
    }

    #endregion

}
