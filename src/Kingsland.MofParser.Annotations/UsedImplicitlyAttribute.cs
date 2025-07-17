using System.Diagnostics;

namespace Kingsland.MofParser.Annotations;

/// <summary>
/// Indicates that the marked symbol is used implicitly (via reflection, in an external library, and so on),
/// so this symbol will be ignored by usage-checking inspections. <br/>
/// You can use <see cref="ImplicitUseKindFlags"/> and <see cref="ImplicitUseTargetFlags"/>
/// to configure how this attribute is applied.
/// </summary>
/// <example><code>
/// [UsedImplicitly]
/// public class TypeConverter {}
/// 
/// public class SummaryData
/// {
///   [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
///   public SummaryData() {}
/// }
/// 
/// [UsedImplicitly(ImplicitUseTargetFlags.WithInheritors | ImplicitUseTargetFlags.Default)]
/// public interface IService {}
/// </code></example>
[AttributeUsage(AttributeTargets.All)]
[Conditional("JETBRAINS_ANNOTATIONS")]
public sealed class UsedImplicitlyAttribute : Attribute
{

    public UsedImplicitlyAttribute()
      : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
    {
    }

    public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
      : this(useKindFlags, ImplicitUseTargetFlags.Default)
    {
    }

    public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
      : this(ImplicitUseKindFlags.Default, targetFlags)
    {
    }

    public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
    {
        this.UseKindFlags = useKindFlags;
        this.TargetFlags = targetFlags;
    }

    public ImplicitUseKindFlags UseKindFlags
    {
        get;
    }

    public ImplicitUseTargetFlags TargetFlags
    {
        get;
    }

    public string Reason
    {
        get;
        set;
    }

}
