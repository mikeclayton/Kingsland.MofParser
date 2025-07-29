using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Sample;

internal static class DumpClasses
{

    internal static void Go()
    {

        const string sourceText = @"
            [ClassVersion(""1.0.0""), FriendlyName(""Environment"")]
            class MSFT_EnvironmentResource : OMI_BaseResource
            {
              [Key] string Name;
              [write] string Value;
              [Write,ValueMap{""Present"", ""Absent""},Values{""Present"", ""Absent""}] string Ensure;
              [Write] boolean Path;
            };
        ";

        // parse the mof file
        var module = Parser.ParseText(sourceText);

        // display the classes
        foreach (var @class in module.GetClasses())
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"name       = {@class.Name}");
            Console.WriteLine($"superclass = {@class.SuperClass}");
            Console.WriteLine("properties:");
            foreach (var property in @class.GetProperties())
            {
                Console.WriteLine("    {0} = {1}", property.Name.PadRight(13), property.ReturnType);
            }
            Console.WriteLine("----------------------------------");
        }

        // ----------------------------------
        // name       = MSFT_EnvironmentResource
        // superclass = OMI_BaseResource
        // properties:
        //     Name          = string
        //     Value         = string
        //     Ensure        = string
        //     Path          = boolean
        // ----------------------------------

    }

}
