using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.3 Compiler directives

    private static Pragma ConvertCompilerDirectiveAst(CompilerDirectiveAst node)
    {
        return new Pragma(
            node.PragmaName.Name,
            node.PragmaParameter.Value
        );
    }

    #endregion

}
