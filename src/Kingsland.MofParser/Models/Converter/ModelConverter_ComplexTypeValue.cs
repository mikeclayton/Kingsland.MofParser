using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.5.9 Complex type value

    private static PropertyValue ConvertComplexTypeValueAst(ComplexTypeValueAst node)
    {
        return node switch
        {
            ComplexValueArrayAst n => ModelConverter.ConvertComplexValueArrayAst(n),
            ComplexValueAst n => ModelConverter.ConvertComplexValueAst(n),
            _ => throw new NotImplementedException(),
        };
    }

    private static ComplexValueArray ConvertComplexValueArrayAst(ComplexValueArrayAst node)
    {
        return new(
            node.Values.Select(ModelConverter.ConvertComplexValueAst)
        );
    }

    private static ComplexValue ConvertComplexValueAst(ComplexValueAst node)
    {
        // section 7.5.9 of the MOF spec says:
        //
        // complexValue = aliasIdentifier /
        //                ( VALUE OF
        //                  ( structureName / className / associationName )
        //                  propertyValueList )
        //
        // but the text says:
        //
        //     The keyword "value" corresponds to the StructureValue CIM metaelement.
        //
        // although it mustn't have an "alias" like a StructureValueDeclaration does
        // so we've introduced a "ComplexObjectValue" to represent the "VALUE OF ..."
        // portion of the definition.
        //
        if (node.IsAlias)
        {
            return new AliasValue(
                (node.Alias ?? throw new InvalidOperationException()).Name
            );
        }
        else if (node.IsValue)
        {
            return new ComplexObjectValue(
                (node.TypeName ?? throw new InvalidOperationException()).Name,
                ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)
            );
        }
        else
        {
            throw new InvalidOperationException($"{nameof(ComplexValueAst)} must be an alias or a value.");
        }
    }

    private static IDictionary<string, PropertyValue> ConvertPropertyValueListAst(PropertyValueListAst node)
    {
        return node.PropertyValues.ToDictionary(
            kvp => kvp.Key,
            kvp => ModelConverter.ConvertPropertyValueAst(kvp.Value)
        );
    }

    private static PropertyValue ConvertPropertyValueAst(PropertyValueAst node)
    {
        return node switch
        {
            PrimitiveTypeValueAst n => ModelConverter.ConvertPrimitiveTypeValueAst(n),
            ComplexTypeValueAst n => ModelConverter.ConvertComplexTypeValueAst(n),
            //ReferenceTypeValueAst n => ModelConverter.FromReferenceTypeValueAst(n),
            EnumTypeValueAst n => ModelConverter.ConvertEnumTypeValueAst(n),
            _ => throw new NotImplementedException()
        };
    }

    #endregion

}
