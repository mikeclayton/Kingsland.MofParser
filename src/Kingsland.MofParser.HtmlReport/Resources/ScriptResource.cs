using Kingsland.MofParser.Models.Types;

namespace Kingsland.MofParser.HtmlReport.Resources;

internal sealed class ScriptResource : DscResource
{

    internal ScriptResource(string filename, string computerName, Instance instance)
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
