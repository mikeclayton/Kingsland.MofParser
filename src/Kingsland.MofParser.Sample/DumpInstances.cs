using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Sample;

internal static class DumpInstances
{

    internal static void Go()
    {

        const string sourceText = @"
            instance of MSFT_RoleResource as $MSFT_RoleResource1ref
            {
                ResourceID = ""[WindowsFeature]IIS"";
                Ensure = ""Present"";
                SourceInfo = ""D:\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature"";
                Name = ""Web-Server"";
                ModuleName = ""PSDesiredStateConfiguration"";
                ModuleVersion = ""1.0"";
            };
        ";

        // parse the mof file
        var module = Parser.ParseText(sourceText);

        // display the instances
        foreach (var instance in module.GetInstances())
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"typename = {instance.TypeName}");
            Console.WriteLine($"alias    = {instance.Alias}");
            Console.WriteLine("properties:");
            foreach (var property in instance.Properties)
            {
                Console.WriteLine("    {0} = {1}", property.Key.PadRight(13), property.Value);
            }
            Console.WriteLine("----------------------------------");
        }

        // ----------------------------------
        // typename = MSFT_RoleResource
        // alias    = MSFT_RoleResource1ref
        // properties:
        //     ResourceID    = [WindowsFeature]IIS
        //     Ensure        = Present
        //     SourceInfo    = D:\dsc\MyServerConfig.ps1::6::9::WindowsFeature
        //     Name          = Web-Server
        //     ModuleName    = PSDesiredStateConfiguration
        //     ModuleVersion = 1.0
        // ----------------------------------
        // }

    }

}
