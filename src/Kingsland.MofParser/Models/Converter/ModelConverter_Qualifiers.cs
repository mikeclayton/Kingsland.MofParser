using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Qualifiers;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.4 Qualifiers

    //private static void ConvertQualifierTypeDeclarationAst(QualifierTypeDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.4.1 QualifierList

    private static IEnumerable<Qualifier> ConvertQualifierListAst(QualifierListAst node)
    {
        foreach (var qualifierValueAst in node.QualifierValues)
        {
            yield return ModelConverter.ConvertQualifierValueAst(qualifierValueAst);
        }
    }

    private static Qualifier ConvertQualifierValueAst(QualifierValueAst node)
    {
        return new(
            node.QualifierName.Name,
            node.Initializer is null
                ? null
                : ModelConverter.ConvertIQualifierInitializerAst(node.Initializer),
            node.Flavors.Select(f => f.Name).ToList()
        );
    }

    private static IQualifierValue ConvertIQualifierInitializerAst(IQualifierInitializerAst node)
    {
        return node switch
        {
            QualifierValueInitializerAst n => ModelConverter.ConvertQualifierValueInitializerAst(n),
            QualifierValueArrayInitializerAst n => new LiteralValueArray(ModelConverter.ConvertQualifierValueArrayInitializerAst(n)),
            _ => throw new NotImplementedException($"Unknown {nameof(QualifierValueAst)} type: {node.GetType().Name}"),
        };
    }

    private static LiteralValue ConvertQualifierValueInitializerAst(QualifierValueInitializerAst node)
    {
        return ModelConverter.ConvertLiteralValueAst(node.Value);
    }

    private static IEnumerable<LiteralValue> ConvertQualifierValueArrayInitializerAst(QualifierValueArrayInitializerAst node)
    {
        foreach (var literalValue in node.Values)
        {
            yield return ModelConverter.ConvertLiteralValueAst(literalValue);
        }
    }

    #endregion

}
