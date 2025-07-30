using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Language;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.2 MOF specification

    internal static Module ConvertMofSpecificationAst(MofSpecificationAst node)
    {
        return new Module(
            node.Productions.Select(ModelConverter.ConvertMofProductionAst)
        );
    }

    private static Production ConvertMofProductionAst(MofProductionAst node)
    {
        return node switch
        {
            CompilerDirectiveAst n => ModelConverter.ConvertCompilerDirectiveAst(n),
            StructureDeclarationAst n => ModelConverter.ConvertStructureDeclarationAst(n),
            ClassDeclarationAst n => ModelConverter.ConvertClassDeclarationAst(n),
            AssociationDeclarationAst n => ModelConverter.ConvertAssociationDeclarationAst(n),
            EnumerationDeclarationAst n => ModelConverter.ConvertEnumerationDeclarationAst(n),
            InstanceValueDeclarationAst n => ModelConverter.ConvertInstanceValueDeclarationAst(n),
            StructureValueDeclarationAst n => ModelConverter.ConvertStructureValueDeclarationAst(n),
            //QualifierTypeDeclarationAst n => ModelConverter.ConvertQualifierTypeDeclaration(n),
            _ => throw new NotImplementedException($"Conversion for {node.GetType().Name} is not implemented.")
        };
    }

    #endregion

}
