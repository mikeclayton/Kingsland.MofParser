using System.Globalization;

namespace Kingsland.MofParser.HtmlReport.Wrappers;

internal static class HtmlHelper
{

    internal static string HtmlEncode(string value)
    {
        return System.Web.HttpUtility.HtmlEncode(value);
    }

    internal static string SplitTitleCaseWords(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        foreach (var upperChar in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            value = value.Replace(upperChar.ToString(CultureInfo.InvariantCulture), " " + upperChar);
        }
        return HtmlHelper.HtmlEncode(value);
    }

    internal static string ReplaceUnderscores(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        value = value.Replace('_', ' ');
        return HtmlHelper.HtmlEncode(value);
    }

}
