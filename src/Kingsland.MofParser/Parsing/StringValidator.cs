﻿namespace Kingsland.MofParser.Parsing;

internal static class StringValidator
{

    #region 5.2 Whitespace

    #region WS = U+0020 / U+0009 / U+000D / U+000A

    private static readonly char[] WhitespaceChars = ['\u0020', '\u0009', '\u000D', '\u000A'];

    internal static bool IsWhitespace(char @char)
    {
        return StringValidator.WhitespaceChars.Contains(@char);
    }

    #endregion

    #endregion

    #region 5.3 Line termination

    private static readonly char[] LineTerminatorChars = ['\u000D', '\u000A'];

    internal static bool IsLineTerminator(char @char)
    {
        return StringValidator.LineTerminatorChars.Contains(@char);
    }

    #endregion

    #region 7.5.1 Structure declaration

    #region structureName = elementName

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// structureName = ( IDENTIFIER / schemaQualifiedName )
    /// </remarks>
    internal static bool IsStructureName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #endregion

    #region 7.5.2 Class declaration

    #region className = elementName

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// className = elementName
    /// </remarks>
    internal static bool IsClassName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #endregion

    #region 7.5.3 Association declaration

    #region associationName = elementName

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// associationName = schemaQualifiedName
    /// </remarks>
    internal static bool IsAssociationName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #endregion

    #region 7.5.4 Enumeration declaration

    #region enumName = elementName

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// enumName = elementName
    /// </remarks>
    internal static bool IsEnumName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #endregion

    #region 7.6.1.1 Integer value

    #region binaryDigit = "0" / "1"

    internal static bool IsBinaryDigit(char value)
    {
        return value is >= '0' and <= '1';
    }

    #endregion

    #region octalDigit = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7"

    internal static bool IsOctalDigit(char value)
    {
        return value is >= '0' and <= '7';
    }

    #endregion

    #region hexDigit = decimalDigit / "a" / "A" / "b" / "B" / "c" / "C" / "d" / "D" / "e" / "E" / "f" / "F"

    internal static bool IsHexDigit(char value)
    {
        return StringValidator.IsDecimalDigit(value) ||
               (value is >= 'a' and <= 'f') ||
               (value is >= 'A' and <= 'F');
    }

    #endregion

    #region decimalValue = [ "+" / "-" ] unsignedDecimalValue

    internal static bool IsDecimalValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false; 
        }
        var chars = value[0] switch
        {
            '+' or '-' => value[1..],
            _ => value

        };
        return StringValidator.IsPositiveDecimalDigit(chars[0]) &&
               chars[1..].All(StringValidator.IsDecimalDigit);
    }

    #endregion

    #region  unsignedDecimalValue = positiveDecimalDigit *decimalDigit

    internal static bool IsUnsignedDecimalValue(string value)
    {
        if (string.IsNullOrEmpty(value)) { return false; }
        return StringValidator.IsPositiveDecimalDigit(value.First()) &&
               value.Skip(1).All(StringValidator.IsDecimalDigit);
    }

    #endregion

    #endregion

    #region 7.6.1.2 Real value

    // realValue = ["+" / "-"] * decimalDigit "." 1*decimalDigit
    //             [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]

    #region decimalDigit = "0" / positiveDecimalDigit

    internal static bool IsDecimalDigit(char value)
    {
        return (value == '0') || StringValidator.IsPositiveDecimalDigit(value);
    }

    #endregion

    #region positiveDecimalDigit = "1"..."9"

    internal static bool IsPositiveDecimalDigit(char value)
    {
        return value is >= '1' and <= '9';
    }

    #endregion

    #endregion

    #region 7.6.1.3 String values

    // The following special characters are used in other ABNF rules in this specification:

    #region BACKSLASH = U+005C ; \

    internal static bool IsBackslash(char @char)
    {
        return (@char == Constants.BACKSLASH);
    }

    #endregion

    #region DOUBLEQUOTE = U+0022 ; "

    internal static bool IsDoubleQuote(char @char)
    {
        return (@char == Constants.DOUBLEQUOTE);
    }

    #endregion

    #region SINGLEQUOTE = U+0027 ; '

    internal static bool IsSingleQuote(char @char)
    {
        return (@char == Constants.SINGLEQUOTE);
    }

    #endregion

    #region UPPERALPHA = U+0041...U+005A ; A ... Z

    internal static bool IsUpperAlpha(char @char)
    {
        return @char is >= '\u0041' and <= '\u005A';
    }

    #endregion

    #region LOWERALPHA = U+0061...U+007A ; a ... z

    internal static bool IsLowerAlpha(char @char)
    {
        return @char is >= '\u0061' and <= '\u007A';
    }

    #endregion

    #region UNDERSCORE = U+005F ; _

    internal static bool IsUnderscore(char @char)
    {
        return (@char == Constants.UNDERSCORE);
    }

    #endregion

    #endregion

    #region 7.6.1.5 Boolean value

    #region FALSE = "false" ; keyword: case insensitive

    internal static bool IsFalse(string value)
    {
        return string.Equals(value, Constants.FALSE, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region TRUE = "true" ; keyword: case insensitive

    internal static bool IsTrue(string value)
    {
        return string.Equals(value, Constants.TRUE, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #endregion

    #region 7.6.1.6 Null value

    #region NULL = "null" ; keyword: case insensitive

    internal static bool IsNull(string value)
    {
        return string.Equals(value, Constants.NULL, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #endregion

    #region 7.7.1 Names

    #region IDENTIFIER = firstIdentifierChar *( nextIdentifierChar )

    internal static bool IsIdentifier(string value)
    {
        return !string.IsNullOrEmpty(value) &&
               StringValidator.IsFirstIdentifierChar(value.First()) &&
               value.Skip(1).All(StringValidator.IsNextIdentifierChar);
    }

    #endregion

    #region firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE

    internal static bool IsFirstIdentifierChar(char value)
    {
        return StringValidator.IsUpperAlpha(value) ||
               StringValidator.IsLowerAlpha(value) ||
               StringValidator.IsUnderscore(value);
    }

    #endregion

    #region nextIdentifierChar = firstIdentifierChar / decimalDigit

    internal static bool IsNextIdentifierChar(char value)
    {
        return StringValidator.IsFirstIdentifierChar(value) ||
               StringValidator.IsDecimalDigit(value);
    }

    #endregion

    #region elementName = localName / schemaQualifiedName

    internal static bool IsElementName(string value)
    {
        return StringValidator.IsLocalName(value) ||
               StringValidator.IsSchemaQualifiedName(value);
    }

    #endregion

    #region localName = IDENTIFIER

    internal static bool IsLocalName(string value)
    {
        return StringValidator.IsIdentifier(value);
    }

    #endregion

    #endregion

    #region 7.7.2 Schema-qualified name

    #region schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER

    internal static bool IsSchemaQualifiedName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }
        var underscore = value.IndexOf(Constants.UNDERSCORE);
        if ((underscore < 1) || (underscore == value.Length - 1))
        {
            return false;
        }
        return StringValidator.IsSchemaName(value[..underscore]) &&
               StringValidator.IsIdentifier(value[(underscore + 1)..]);
    }

    #endregion

    #region schemaName = firstSchemaChar *( nextSchemaChar )

    internal static bool IsSchemaName(string value)
    {
        return !string.IsNullOrEmpty(value) &&
               StringValidator.IsFirstSchemaChar(value[0]) &&
               value.Skip(1).All(StringValidator.IsNextSchemaChar);
    }

    #endregion

    #region firstSchemaChar = UPPERALPHA / LOWERALPHA

    internal static bool IsFirstSchemaChar(char value)
    {
        return StringValidator.IsUpperAlpha(value) ||
               StringValidator.IsLowerAlpha(value);
    }

    #endregion

    #region nextSchemaChar = firstSchemaChar / decimalDigit

    internal static bool IsNextSchemaChar(char value)
    {
        return StringValidator.IsFirstSchemaChar(value) ||
               StringValidator.IsDecimalDigit(value);
    }

    #endregion

    #endregion

    #region 7.7.3 Alias identifier

    #region aliasIdentifier = "$" IDENTIFIER

    internal static bool IsAliasIdentifier(string value)
    {
        return !string.IsNullOrEmpty(value) &&
               (value.First() == '$') &&
               StringValidator.IsIdentifier(value[1..]);
    }

    #endregion

    #endregion

    private static bool IsSpecialName(string name)
    {
        return name.StartsWith("__") &&
            StringValidator.IsIdentifier(name[2..]);
    }

}
