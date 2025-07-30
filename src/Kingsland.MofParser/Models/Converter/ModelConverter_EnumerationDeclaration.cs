using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.5.4 Enumeration declaration

    private static Enumeration ConvertEnumerationDeclarationAst(EnumerationDeclarationAst node)
    {
        return new Enumeration(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.EnumName.Name,
            node.EnumType.Name,
            node.EnumElements.Select(ModelConverter.ConvertEnumElementAst).ToList()
        );
    }

    private static EnumElement ConvertEnumElementAst(EnumElementAst node)
    {
        return new(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.EnumElementName.Name,
            node.EnumElementValue is null
                ? null
                : ModelConverter.ConvertIEnumElementValueAst(node.EnumElementValue)
        );
    }

    private static IEnumElementValue ConvertIEnumElementValueAst(IEnumElementValueAst node)
    {
        return node switch
        {
            IntegerValueAst n => new IntegerValue(n.Value),
            StringValueAst n => new StringValue(n.Value),
            _ => throw new NotImplementedException($"Unsupported enum element value type: {node.GetType().Name}"),
        };
    }

    #endregion

    #region 7.6.3 Enum type value

    private static EnumTypeValue ConvertEnumTypeValueAst(EnumTypeValueAst node)
    {
        return node switch
        {
            EnumValueAst n => ModelConverter.ConvertEnumValueAst(n),
            EnumValueArrayAst n => ModelConverter.ConvertEnumValueArrayAst(n),
            _ => throw new NotImplementedException()
        };
    }

    private static EnumValue ConvertEnumValueAst(EnumValueAst node)
    {
        return new(
            node.EnumName?.Name,
            node.EnumLiteral.Name
        );
    }

    private static EnumValueArray ConvertEnumValueArrayAst(EnumValueArrayAst node)
    {
        return new(
            node.Values.Select(ModelConverter.ConvertEnumValueAst)
        );
    }

    #endregion

}
