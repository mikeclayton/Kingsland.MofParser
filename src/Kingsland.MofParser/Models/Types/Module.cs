using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Module
{

    //[Obsolete("Use the constructor with IEnumerable<Instance> instead.")]
    //internal Module(params Instance[] instances)
    //    : this((IEnumerable<Instance>)instances)
    //{
    //}

    internal Module(IEnumerable<Instance> instances)
    {
        this.Instances = (instances ?? throw new ArgumentNullException(nameof(instances)))
            .ToList().AsReadOnly();
    }

    [PublicAPI]
    public ReadOnlyCollection<Instance> Instances
    {
        get;
    }

}
