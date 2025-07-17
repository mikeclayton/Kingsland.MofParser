using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.2 Class declaration
///
///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
///                        "{" *classFeature "}" ";"
///
///     className        = elementName
///
///     superClass       = ":" className
///
///     classFeature     = structureFeature /
///                        methodDeclaration
///
///     CLASS            = "class" ; keyword: case insensitive
///
/// </remarks>
public sealed record ClassDeclarationAst : MofProductionAst
{

    #region Builder

    [PublicAPI]
    public sealed class Builder
    {

        [PublicAPI]
        public Builder()
        {
            this.QualifierList = new();
            this.ClassFeatures = [];
        }

        [PublicAPI]
        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? ClassName
        {
            get;
            set;
        }

        [PublicAPI]
        public IdentifierToken? SuperClass
        {
            get;
            set;
        }

        [PublicAPI]
        public List<IClassFeatureAst> ClassFeatures
        {
            get;
            set;
        }

        [PublicAPI]
        public ClassDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.ClassName ?? throw new InvalidOperationException(
                    $"{nameof(this.ClassName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperClass,
                this.ClassFeatures
            );
        }

    }

    #endregion

    #region Constructors

    internal ClassDeclarationAst(
        IdentifierToken className,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    ) : this((QualifierListAst?)null, className, null, classFeatures)
    {
    }

    internal ClassDeclarationAst(
        IdentifierToken className,
        IdentifierToken? superClass,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    ) : this((QualifierListAst?)null, className, superClass, classFeatures)
    {
    }

    internal ClassDeclarationAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken className,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    ) : this(new QualifierListAst(qualifierList), className, null, classFeatures)
    {
    }

    internal ClassDeclarationAst(
        QualifierValueAst[] qualifierList,
        IdentifierToken className,
        IdentifierToken? superClass,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    ): this(new QualifierListAst(qualifierList), className, superClass, classFeatures)
    {
    }

    internal ClassDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken className,
        IdentifierToken? superClass,
        IEnumerable<IClassFeatureAst>? classFeatures = null
    )
    {
        this.QualifierList = qualifierList ?? new();
        this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
        this.SuperClass = superClass;
        this.ClassFeatures = (classFeatures ?? []).ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    [PublicAPI]
    public QualifierListAst QualifierList
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken ClassName
    {
        get;
    }

    [PublicAPI]
    public IdentifierToken? SuperClass
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
    {
        get;
    }

    #endregion

}
