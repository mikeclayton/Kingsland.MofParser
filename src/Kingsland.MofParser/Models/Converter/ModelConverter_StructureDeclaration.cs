using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.5.1 Structure declaration

    private static Structure ConvertStructureDeclarationAst(StructureDeclarationAst node)
    {
        return new(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.StructureName.Name,
            node.SuperStructure?.Name,
            node.StructureFeatures.Select(ModelConverter.ConvertStructureFeatureAst).ToList()
        );
    }

    private static IStructureFeature ConvertStructureFeatureAst(IStructureFeatureAst node)
    {
        return node switch
        {
            EnumerationDeclarationAst n => ModelConverter.ConvertEnumerationDeclarationAst(n),
            PropertyDeclarationAst n => ModelConverter.ConvertPropertyDeclarationAst(n),
            StructureDeclarationAst n => ModelConverter.ConvertStructureDeclarationAst(n),
            _ => throw new NotImplementedException($"Unsupported structure feature: {node.GetType().Name}"),
        };
    }

    #endregion

}
