using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.5.3 Association declaration

    private static Association ConvertAssociationDeclarationAst(AssociationDeclarationAst node)
    {
        return new Association(
            ModelConverter.ConvertQualifierListAst(node.QualifierList),
            node.AssociationName.Name,
            node.SuperAssociation?.Name,
            node.ClassFeatures.Select(ModelConverter.ConvertClassFeatureAst).ToList()
        );
    }

    #endregion

}
