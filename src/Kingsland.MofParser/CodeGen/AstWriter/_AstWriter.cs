// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

[PublicAPI]
public sealed partial class AstWriter
{

    #region Constructors

    [PublicAPI]
    public AstWriter(TextWriter writer)
        : this(writer, AstWriterOptions.Default)
    {
    }

    [PublicAPI]
    public AstWriter(TextWriter writer, AstWriterOptions options)
    {
        this.TextWriter = writer ?? throw new ArgumentNullException(nameof(writer));
        this.Options = options ?? throw new ArgumentNullException(nameof(options));
        this.Depth = 0;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public TextWriter TextWriter
    {
        get;
    }

    [PublicAPI]
    public AstWriterOptions Options
    {
        get;
    }

    [PublicAPI]
    public int Depth
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    [PublicAPI]
    public void Indent()
    {
        this.Depth += 1;
    }

    [PublicAPI]
    public void Unindent()
    {
        if (this.Depth == 0)
        {
            throw new InvalidOperationException();
        }
        this.Depth -= 1;
    }

    [PublicAPI]
    public void WriteIndent()
    {
        for (var i = 0; i < this.Depth; i++)
        {
            this.TextWriter.Write(this.Options.IndentStep);
        }
    }

    [PublicAPI]
    public void WriteLine()
    {
        this.TextWriter.WriteLine();
    }

    [PublicAPI]
    public void WriteString(char value)
    {
        this.TextWriter.Write(value);
    }

    //[PublicAPI]
    //public void WriteIdentifier(IdentifierToken value) {
    //    this.TextWriter.Write(value.ToString());
    //}

    [PublicAPI]
    public void WriteString(string value)
    {
        this.TextWriter.Write(value);
    }

    [PublicAPI]
    public void WriteString(params string[] arg)
    {
        foreach (var value in arg)
        {
            this.TextWriter.Write(value);
        }
    }

    [PublicAPI]
    public void WriteDelimitedList<T>(IEnumerable<T> source, Action<T> writer, string separator)
    {
        var count = 0;
        foreach (var value in source)
        {
            if (count > 0)
            {
                this.WriteString(separator);
            }
            writer(value);
            count++;
        }
    }

    [PublicAPI]
    public void Flush()
    {
        this.TextWriter.Flush();
    }

    #endregion

}
