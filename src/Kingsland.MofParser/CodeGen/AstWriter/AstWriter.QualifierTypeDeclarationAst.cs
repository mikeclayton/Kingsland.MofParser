using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.4 Qualifiers

    [PublicAPI]
    public void WriteAstNode(QualifierTypeDeclarationAst node)
    {
        if (!string.IsNullOrEmpty(node.QualifierName.Name))
        {
            this.WriteString(node.QualifierName.Name);
        }
        if (node.Flavors.Count > 0)
        {
            this.WriteString(": ");
            this.WriteString(string.Join(" ", node.Flavors));
        }
    }

    #endregion

}
