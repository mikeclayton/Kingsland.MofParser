﻿using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.4.1 QualifierList
///
///     qualifierValue                = qualifierName [ qualifierValueInitializer /
///                                     qualiferValueArrayInitializer ]
///
///     qualifierValueInitializer     = "(" literalValue ")"
///
///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
///
/// </remarks>
public sealed record QualifierValueAst : AstNode
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Flavors = [];
        }

        public IdentifierToken? QualifierName
        {
            get;
            set;
        }

        public IQualifierInitializerAst? Initializer
        {
            get;
            set;
        }

        public List<IdentifierToken> Flavors
        {
            get;
            set;
        }

        public QualifierValueAst Build()
        {
            return new(
                this.QualifierName ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierName)} property must be set before calling {nameof(Build)}."
                ),
                this.Initializer,
                this.Flavors
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierValueAst(
        IdentifierToken qualifierName,
        IQualifierInitializerAst? initializer,
        IEnumerable<IdentifierToken>? flavors)
    {
        this.QualifierName = qualifierName ?? throw new ArgumentNullException(nameof(qualifierName));
        this.Initializer = initializer;
        this.Flavors = (flavors ?? Enumerable.Empty<IdentifierToken>())
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    public IdentifierToken QualifierName
    {
        get;
    }

    public IQualifierInitializerAst? Initializer
    {
        get;
    }

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// 7.4 Qualifiers
    ///
    /// NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
    /// MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
    ///
    /// These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
    ///
    /// </remarks>
    public ReadOnlyCollection<IdentifierToken> Flavors
    {
        get;
    }

    #endregion

}
