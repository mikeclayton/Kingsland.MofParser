using Kingsland.ParseFx.Text;

namespace Kingsland.ParseFx.Syntax;

[PublicAPI]
public abstract record SyntaxToken
{

    #region Constructors

    protected SyntaxToken(SourceExtent? extent)
    {
        this.Extent = extent;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public SourceExtent? Extent
    {
        get;
    }

    [PublicAPI]
    public string? Text =>
        this.Extent?.Text;

    #endregion

    #region Methods

    [PublicAPI]
    public virtual string GetDebugString()
    {
        return $"{this.GetType().Name} (\"{this.Extent?.Text}\")";
    }

    [PublicAPI]
    public virtual string GetSourceString()
    {
        return $"{this.Extent?.Text}";
    }

    #endregion

    #region Object Interface

    public override string ToString()
    {
        return this.GetDebugString();
    }

    #endregion

}
