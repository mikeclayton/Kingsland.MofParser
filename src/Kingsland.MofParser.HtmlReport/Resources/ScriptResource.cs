using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.HtmlReport.Resources;

internal sealed class ScriptResource : DscResource
{

    internal ScriptResource(string filename, string computerName, InstanceValue instance)
        : base(filename, computerName, instance)
    {
    }

    internal string? GetScript =>
        this.GetStringProperty(nameof(this.GetScript));

    internal string? TestScript =>
        this.GetStringProperty(nameof(this.TestScript));

    internal string? SetScript =>
        this.GetStringProperty(nameof(this.SetScript));

}
