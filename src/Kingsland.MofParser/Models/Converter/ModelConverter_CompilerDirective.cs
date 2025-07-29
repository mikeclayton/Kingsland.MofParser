using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Language;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.3 Compiler directives

    private static CompilerDirective ConvertCompilerDirectiveAst(CompilerDirectiveAst node)
    {
        return new CompilerDirective(
            node.PragmaName.Name,
            node.PragmaParameter.Value
        );
    }

    #endregion

}
