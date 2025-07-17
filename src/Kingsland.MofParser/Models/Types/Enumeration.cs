namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed class Enumeration
{

    [PublicAPI]
    public Enumeration(string name, Type underlyingType)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.UnderlyingType =
            new[] { typeof(string), typeof(int) }
                .Contains(underlyingType ?? throw new ArgumentNullException(nameof(underlyingType)))
            ? underlyingType
            : throw new ArgumentException(
                "underlying type must be string or int",
                nameof(underlyingType)
            );
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

    [PublicAPI]
    public Type UnderlyingType
    {
        get;
    }

}
