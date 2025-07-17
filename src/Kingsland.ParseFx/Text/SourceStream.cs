using Kingsland.ParseFx.Lexing;

namespace Kingsland.ParseFx.Text;

public sealed class SourceStream
{

    #region Constructors

    private SourceStream(TextReader baseReader)
    {
        this.BaseReader = baseReader ?? throw new ArgumentNullException(nameof(baseReader));
        this.Buffer = [];
    }

    #endregion

    #region Properties

    [PublicAPI]
    private TextReader BaseReader
    {
        get;
    }

    [PublicAPI]
    private List<SourceChar> Buffer
    {
        get;
    }

    #endregion

    #region Methods

    [PublicAPI]
    public bool Eof(int index)
    {
        return !this.PopulateBufferToPosition(index);
    }

    /// <summary>
    /// Returns the character at the specified index of the input stream.
    /// </summary>
    /// <returns></returns>
    [PublicAPI]
    public SourceChar Read(int index)
    {
        if (!this.PopulateBufferToPosition(index))
        {
            throw new UnexpectedEndOfStreamException();
        }
        return this.Buffer[index];
    }

    [PublicAPI]
    private bool PopulateBufferToPosition(int position)
    {
        while (this.Buffer.Count <= position)
        {
            if (!this.PopulateBufferChar())
            {
                return false;
            }
        }
        return true;
    }

    [PublicAPI]
    private bool PopulateBufferChar()
    {
        // read the next char from the stream
        var streamRead = this.BaseReader.Read();
        if (streamRead == -1)
        {
            return false;
        }
        var streamChar = (char)streamRead;
        // append a char to the buffer
        var lastChar = (this.Buffer.Count == 0) ? null : this.Buffer[^1];
        var nextChar = new SourceChar(
            SourceStream.GetNextPosition(lastChar, streamChar),
            streamChar
        );
        this.Buffer.Add(nextChar);
        return true;
    }

    [PublicAPI]
    private static SourcePosition GetNextPosition(SourceChar? lastChar, char nextChar)
    {
        if (lastChar == null)
        {
            return SourceStream.StartOfStream();
        }
        var lastPosition = lastChar.Position;
        if (lastPosition == null)
        {
            return SourceStream.StartOfStream();
        }
        return lastChar.Value switch
        {
            '\r' => (nextChar == '\n')
                ? SourceStream.MoveToNext(lastPosition)
                : SourceStream.StartNewLine(lastPosition),
            '\n' => SourceStream.StartNewLine(lastPosition),
            _ => SourceStream.MoveToNext(lastPosition),
        };
    }

    [PublicAPI]
    private static SourcePosition StartOfStream()
    {
        return new SourcePosition(
            position: 0,
            lineNumber: 1,
            columnNumber: 1
        );
    }

    [PublicAPI]
    private static SourcePosition StartNewLine(SourcePosition lastPosition)
    {
        return new SourcePosition(
            position: lastPosition.Position + 1,
            lineNumber: lastPosition.LineNumber + 1,
            columnNumber: 1
        );
    }

    [PublicAPI]
    private static SourcePosition MoveToNext(SourcePosition lastPosition)
    {
        return new SourcePosition(
            position: lastPosition.Position + 1,
            lineNumber: lastPosition.LineNumber,
            columnNumber: lastPosition.ColumnNumber + 1
        );
    }

    #endregion

    #region Factory Methods

    [PublicAPI]
    public static SourceStream From(TextReader value)
    {
        return new SourceStream(value);
    }

    [PublicAPI]
    public static SourceStream From(string value)
    {
        return new SourceStream(
            new StringReader(
                value ?? throw new ArgumentNullException(nameof(value))
            )
        );
    }

    #endregion

}
