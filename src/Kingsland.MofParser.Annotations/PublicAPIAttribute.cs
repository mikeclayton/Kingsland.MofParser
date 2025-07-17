using System.Diagnostics;

namespace Kingsland.MofParser.Annotations;

/// <summary>
/// This attribute is intended to mark publicly available APIs
/// that should not be removed and therefore should never be reported as unused.
/// </summary>
/// <remarks>
/// Private implementation of PublicAPIAttribute so we don't have to reference JetBrains.Annotations
/// See https://www.jetbrains.com/help/resharper/Reference__Code_Annotation_Attributes.html#PublicAPIAttribute
///     https://github.com/JetBrains/JetBrains.Annotations
/// </remarks>
#if JETBRAINS_ANNOTATIONS
[JetBrains.Annotations.MeansImplicitUse(JetBrains.Annotations.ImplicitUseTargetFlags.WithMembers)]
#endif
[AttributeUsage(AttributeTargets.All, Inherited = false)]
[Conditional("JETBRAINS_ANNOTATIONS")]
public sealed class PublicAPIAttribute : Attribute
{

    public PublicAPIAttribute()
    {
    }

#if JETBRAINS_ANNOTATIONS
    [JetBrains.Annotations.PublicAPI]
#endif
    public PublicAPIAttribute(

#if JETBRAINS_ANNOTATIONS
        [JetBrains.Annotations.NotNull]
        [JetBrains.Annotations.PublicAPI]
#endif
        string comment
    )
    {
        this.Comment = comment ?? throw new ArgumentNullException(nameof(comment));
    }

#if JETBRAINS_ANNOTATIONS
    [JetBrains.Annotations.CanBeNull]
#endif
    public string Comment 
    { 
        get;
    }

}
