using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.3 Compiler directives

    //private static void ConvertCompilerDirectiveAst(CompilerDirectiveAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.4 Qualifiers

    //private static void ConvertQualifierTypeDeclarationAst(QualifierTypeDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.4.1 QualifierList

    //private static void ConvertQualifierListAst(QualifierListAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertQualifierValueAst(QualifierValueAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertIQualifierInitializerAst(IQualifierInitializerAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertQualifierValueInitializerAst(QualifierValueInitializerAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertQualifierValueArrayInitializerAst(QualifierValueArrayInitializerAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.1 Structure declaration

    //private static void ConvertStructureDeclarationAst(StructureDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertStructureFeatureAst(IStructureFeatureAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.2 Class declaration

    //private static void ConvertClassDeclarationAst(ClassDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertClassFeatureAst(IClassFeatureAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.3 Association declaration

    //private static void ConvertAssociationDeclarationAst(AssociationDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.4 Enumeration declaration

    //private static Enumeration ConvertEnumerationDeclarationAst(EnumerationDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertEnumElementAst(EnumElementAst node)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void ConvertIEnumElementValueAst(IEnumElementValueAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.5 Property declaration

    //private static void ConvertPropertyDeclarationAst(PropertyDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.6 Method declaration

    //private static void ConvertMethodDeclarationAst(MethodDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.5.7 Parameter declaration

    //private static void ConvertParameterDeclarationAst(ParameterDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion

    #region 7.6.1 Primitive type value

    private static PrimitiveTypeValue ConvertPrimitiveTypeValueAst(PrimitiveTypeValueAst node)
    {
        return node switch
        {
            LiteralValueAst n => ModelConverter.ConvertLiteralValueAst(n),
            LiteralValueArrayAst n => ModelConverter.ConvertLiteralValueArrayAst(n),
            _ => throw new NotImplementedException(),
        };
    }

    private static LiteralValue ConvertLiteralValueAst(LiteralValueAst node)
    {
        return node switch
        {
            IntegerValueAst n => ModelConverter.ConvertIntegerValueAst(n),
            RealValueAst n => ModelConverter.ConvertRealValueAst(n),
            BooleanValueAst n => ModelConverter.ConvertBooleanValueAst(n),
            NullValueAst n => ModelConverter.ConvertNullValueAst(n),
            StringValueAst n => ModelConverter.ConvertStringValueAst(n),
            _ => throw new NotImplementedException(),
        };
    }

    private static LiteralValueArray ConvertLiteralValueArrayAst(LiteralValueArrayAst node)
    {
        return new(
            node.Values.Select(ModelConverter.ConvertLiteralValueAst)
        );
    }

    #endregion

    #region 7.6.1.1 Integer value

    private static IntegerValue ConvertIntegerValueAst(IntegerValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.2 Real value

    private static RealValue ConvertRealValueAst(RealValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.3 String values

    private static StringValue ConvertStringValueAst(StringValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.5 Boolean value

    private static BooleanValue ConvertBooleanValueAst(BooleanValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.6 Null value

    private static NullValue ConvertNullValueAst(NullValueAst node)
    {
        return NullValue.Null;
    }

    #endregion

    #region 7.6.2 Complex type value

    private static Instance ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node)
    {
        return new Instance(
            typeName: node.TypeName.Name,
            alias: node.Alias?.Name,
            properties: [.. ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)]
        );
    }

    //private static void ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node)
    //{
    //    throw new NotImplementedException();
    //}

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
