namespace Kingsland.ParseFx.Attributes.StaticAnalysis;

/// <summary>
/// Private implementation of PublicAPIAttribute so we don't have to reference JetBrains.Annotations
/// </summary>
/// <remarks>
/// See https://www.jetbrains.com/help/resharper/Reference__Code_Annotation_Attributes.html#PublicAPIAttribute
/// </remarks>
internal sealed class PublicAPIAttribute : Attribute
{
}
