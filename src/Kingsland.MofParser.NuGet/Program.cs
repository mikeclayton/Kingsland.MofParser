namespace Kingsland.MofParser.NuGet;

static class Program
{

    static void Main()
    {

        const string filename = "dsc\\MyServer.mof";

        // parse the mof file
        var instances = PowerShellDscHelper.ParseMofFileInstances(filename);

        // display the instances
        foreach (var instance in instances)
        {
            Console.WriteLine("--------------------------");
            Console.Write($"instance of {instance.TypeName}");
            if (!string.IsNullOrEmpty(instance.Alias))
            {
                Console.Write($" as ${instance.Alias}");
            }
            Console.WriteLine();
            foreach (var property in instance.Properties)
            {
                Console.WriteLine($"    {property.Name,-14} = {property.Value}");
            }
            Console.WriteLine("--------------------------");
        }

    }

}
