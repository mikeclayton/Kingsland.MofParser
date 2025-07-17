using System.Diagnostics;

namespace Kingsland.MofParser.Annotations;

/// <summary>
/// Indicates that the value of the marked element could be <c>null</c> sometimes,
/// so checking for <c>null</c> is required before its usage.
/// </summary>
/// <example><code>
/// [CanBeNull] object Test() => null;
/// 
/// void UseTest() {
///   var p = Test();
///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
/// }
/// </code></example>
[AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
[Conditional("JETBRAINS_ANNOTATIONS")]
public sealed class CanBeNullAttribute : Attribute
{
}
