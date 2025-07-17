namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Class
{

    internal Class(string className, string superClass)
    {
        this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
        this.SuperClass = superClass ?? throw new ArgumentNullException(nameof(superClass));
    }

    [PublicAPI]
    public string ClassName
    {
        get;
    }

    [PublicAPI]
    public string SuperClass
    {
        get;
    }

}
