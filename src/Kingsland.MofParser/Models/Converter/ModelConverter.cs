using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.6.2 Complex type value

    private static InstanceValue ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node)
    {
        return new InstanceValue(
            node.TypeName.Name,
            node.Alias?.Name,
            ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)
        );
    }

    private static StructureValue ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node)
    {
        return new StructureValue(
            node.TypeName.Name,
            node.Alias.Name,
            ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)
        );
    }

    #endregion

}
