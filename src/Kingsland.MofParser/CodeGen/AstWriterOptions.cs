namespace Kingsland.MofParser.CodeGen;

public sealed class AstWriterOptions
{

    #region Fields

    [PublicAPI]
    public static readonly AstWriterOptions Default = new(
        newLine: Environment.NewLine,
        indentStep: "\t",
        quirks: MofQuirks.None
    );

    #endregion

    #region Constructors

    [PublicAPI]
    public AstWriterOptions(
        string newLine, string indentStep, MofQuirks quirks = MofQuirks.None
    )
    {
        this.NewLine = newLine ?? throw new ArgumentNullException(nameof(newLine));
        this.IndentStep = indentStep ?? throw new ArgumentNullException(nameof(indentStep));
        this.Quirks = quirks;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public string NewLine
    {
        get;
    }

    [PublicAPI]
    public string IndentStep
    {
        get;
    }

    [PublicAPI]
    public MofQuirks Quirks
    {
        get;
    }

    #endregion

    #region Methods

    [PublicAPI]
    public static AstWriterOptions Create(
        string? newLine = null, string? indentStep = null, MofQuirks? quirks = null
    )
    {
        return new AstWriterOptions(
            newLine ?? AstWriterOptions.Default.NewLine,
            indentStep ?? AstWriterOptions.Default.IndentStep,
            quirks ?? AstWriterOptions.Default.Quirks
        );
    }

    #endregion

}
