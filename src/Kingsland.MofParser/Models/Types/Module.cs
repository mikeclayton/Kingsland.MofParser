using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Module
{

    internal Module(IEnumerable<IProduction>? productions = null)
    {
        this.Productions = (productions ?? []).ToList().AsReadOnly();
    }

    internal Module(params IProduction[] productions)
    {
        this.Productions = productions.ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<IProduction> Productions
    {
        get;
    }

    [PublicAPI]
    public IEnumerable<Association> GetAssociations()
    {
        return this.Productions
            .Where(production => production is Association)
            .Cast<Association>();
    }

    [PublicAPI]
    public IEnumerable<Class> GetClasses()
    {
        return this.Productions
            .Where(production => production is Class)
            .Cast<Class>();
    }

    [PublicAPI]
    public IEnumerable<Instance> GetInstances()
    {
        return this.Productions
            .Where(production => production is Instance)
            .Cast<Instance>();
    }

}
