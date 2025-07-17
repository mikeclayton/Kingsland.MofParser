using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast;

public abstract record AstNode : ParseFx.Parsing.AstNode
{

    #region Object Overrides

    [PublicAPI]
    public sealed override string ToString()
    {
        var buffer = new StringWriter();
        var writer = new AstWriter(
            buffer
        );
        writer.WriteAstNode(this);
        writer.Flush();
        return buffer.ToString();

    }

    [PublicAPI]
    public string ToString(AstWriterOptions options)
    {
        var buffer = new StringWriter();
        var writer = new AstWriter(
            buffer, options
        );
        writer.WriteAstNode(this);
        writer.Flush();
        return buffer.ToString();

    }

    #endregion

}
