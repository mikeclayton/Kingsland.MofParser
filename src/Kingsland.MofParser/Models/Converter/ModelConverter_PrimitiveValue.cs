using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

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
        return new(
            node.StringLiteralValues.Select(s => s.Value).ToArray()
        );
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

}
