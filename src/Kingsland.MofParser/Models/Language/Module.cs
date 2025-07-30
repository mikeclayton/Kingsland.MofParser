using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Language;

[PublicAPI]
public sealed class Module
{

    internal Module(IEnumerable<Production>? productions = null)
    {
        this.Productions = (productions ?? []).ToList().AsReadOnly();
    }

    internal Module(params Production[] productions)
    {
        this.Productions = productions.ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<Production> Productions
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
    public IEnumerable<InstanceValue> GetInstances()
    {
        return this.Productions
            .Where(production => production is InstanceValue)
            .Cast<InstanceValue>();
    }

}
