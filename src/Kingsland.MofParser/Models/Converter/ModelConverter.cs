using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.6.2 Complex type value

    private static Instance ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node)
    {
        return new Instance(
            node.TypeName.Name,
            node.Alias?.Name,
            ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)
        );
    }

    private static ComplexValueObject ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node)
    {
        return new ComplexValueObject(
            node.TypeName.Name,
            node.Alias?.Name,
            ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)
        );
    }

    #endregion

}
