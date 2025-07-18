﻿using Kingsland.MofParser.Parsing;
using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Types;

[PublicAPI]
public sealed record Instance
{

    internal Instance(string typeName, IEnumerable<Property>? properties = null)
        : this(typeName, null, properties)
    {
    }

    internal Instance(string typeName, string? alias, IEnumerable<Property>? properties = null)
    {
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.Alias = alias;
        this.Properties = (properties ?? []).ToList().AsReadOnly();
    }

    [PublicAPI]
    public string TypeName
    {
        get;
    }

    [PublicAPI]
    public string? Alias
    {
        get;
    }

    [PublicAPI]
    public ReadOnlyCollection<Property> Properties
    {
        get;
    }

    //public T GetValue<T>(string name)
    //{
    //    return (T)this.Properties.Single(p => p.Name == name).Value;
    //}

    //public bool TryGetValue<T>(string name, out T result)
    //{
    //    var property = this.Properties.SingleOrDefault(p => p.Name == name);
    //    if (property == null)
    //    {
    //        result = default;
    //        return false;
    //    }
    //    var value = property.Value;
    //    if (value is T typed)
    //    {
    //        result = typed;
    //        return true;
    //    }
    //    result = default;
    //    return false;
    //}

    public override string ToString()
    {
        var result = new StringBuilder();
        result.Append($"{Constants.INSTANCE} {Constants.OF} {this.TypeName}");
        if (!string.IsNullOrEmpty(this.Alias))
        {
            result.Append($" {Constants.AS} ${this.Alias}");
        }
        return result.ToString();
    }

}
