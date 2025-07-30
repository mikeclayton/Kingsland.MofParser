using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast;

public interface IAstNode
{

    [PublicAPI]
    public string ToMofString()
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
    public string ToMofString(AstWriterOptions options)
    {
        var buffer = new StringWriter();
        var writer = new AstWriter(
            buffer, options
        );
        writer.WriteAstNode(this);
        writer.Flush();
        return buffer.ToString();

    }

}
