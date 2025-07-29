using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.5.2 Class declaration

    private static Class ConvertClassDeclarationAst(ClassDeclarationAst node)
    {
        return new Class(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.ClassName.Name,
            node.SuperClass?.Name,
            node.ClassFeatures.Select(
                classFeature => ModelConverter.ConvertClassFeatureAst(classFeature)
            )
        );
    }

    private static IClassFeature ConvertClassFeatureAst(IClassFeatureAst node)
    {
        return node switch
        {
            MethodDeclarationAst n => ModelConverter.ConvertMethodDeclarationAst(n),
            IStructureFeatureAst n => ModelConverter.ConvertStructureFeatureAst(n),
            _ => throw new NotImplementedException($"Unsupported class feature: {node.GetType().Name}"),
        };
    }

    #endregion


    #region 7.5.5 Property declaration

    private static Property ConvertPropertyDeclarationAst(PropertyDeclarationAst node)
    {
        return new(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.ReturnType.Name,
            node.ReturnTypeIsRef,
            node.PropertyName.Name,
            node.ReturnTypeIsArray,
            node.Initializer is null
                ? null
                : ModelConverter.ConvertPropertyValueAst(node.Initializer)
        );
    }

    #endregion

    #region 7.5.6 Method declaration

    private static Method ConvertMethodDeclarationAst(MethodDeclarationAst node)
    {
        return new(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.ReturnType.Name,
            node.ReturnTypeIsArray,
            node.Name.Name,
            node.Parameters.Select(ModelConverter.ConvertParameterDeclarationAst).ToList()
        );
    }

    #endregion

    #region 7.5.7 Parameter declaration

    private static Parameter ConvertParameterDeclarationAst(ParameterDeclarationAst node)
    {
        return new(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.ParameterType.Name,
            node.ParameterIsRef,
            node.ParameterName.Name,
            node.ParameterIsArray,
            node.DefaultValue is null
                ? null
                : ModelConverter.ConvertPropertyValueAst(node.DefaultValue)
        );
    }

    #endregion

}
