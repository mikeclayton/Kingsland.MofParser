using System.Runtime.CompilerServices;

namespace Kingsland.MofParser.Parsing;

internal static class StringValidator
{

    #region 5.2 Whitespace

    private static readonly char[] WhitespaceChars = [
        '\u0020', // space
        '\u0009', // horizontal tab
        '\u000D', // carriage return
        '\u000A'  // line feed
    ];

    /// <summary>
    /// Checks if the specified character is a whitespace character as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// WS = U+0020 / U+0009 / U+000D / U+000A
    /// </remarks>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsWhitespace(char @char)
    {
        return StringValidator.WhitespaceChars.Contains(@char);
    }

    #endregion

    #region 5.3 Line termination

    private static readonly char[] LineTerminatorChars = [
        '\u000D', // carriage return
        '\u000A'  // line feed
    ];

    /// <summary>
    /// Checks if the specified character is a line terminator.
    /// </summary>
    /// <remarks>
    /// U+000D / U+000A
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsLineTerminator(char @char)
    {
        return StringValidator.LineTerminatorChars.Contains(@char);
    }

    #endregion

    #region 7.5.1 Structure declaration

    /// <summary>
    /// Checks if the given value is a valid structure name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// structureName = elementName
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsStructureName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #region 7.5.2 Class declaration

    /// <summary>
    /// Checks if the given value is a valid class name as defined in the MOF specification.
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// className = elementName
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsClassName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #region 7.5.3 Association declaration

    /// <summary>
    /// Checks if the given value is a valid association name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// associationName = schemaQualifiedName
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsAssociationName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #region 7.5.4 Enumeration declaration

    /// <summary>
    /// Checks if the given value is a valid enumeration name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// enumName = elementName
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsEnumName(string value)
    {
        return StringValidator.IsElementName(value);
    }

    #endregion

    #region 7.6.1.1 Integer value

    /// <summary>
    /// Checks if the specified character is a binary digit as defined in the MOF specification.
    /// </summary>
    /// <returns>
    /// binaryDigit = "0" / "1"
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsBinaryDigit(char value)
    {
        return value is >= '0' and <= '1';
    }

    /// <summary>
    /// Checks if the specified character is a valid octal digit as defined in the MOF specification.
    /// </summary>
    /// <returns>
    /// octalDigit = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7"
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsOctalDigit(char value)
    {
        return value is >= '0' and <= '7';
    }

    /// <summary>
    /// Checks if the specified character is a hexadecimal digit as defined in the MOF specification.
    /// </summary>
    /// <returns>
    /// hexDigit = decimalDigit / "a" / "A" / "b" / "B" / "c" / "C" / "d" / "D" / "e" / "E" / "f" / "F"
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsHexDigit(char value)
    {
        return StringValidator.IsDecimalDigit(value) ||
               (value is >= 'a' and <= 'f') ||
               (value is >= 'A' and <= 'F');
    }

    /// <summary>
    /// Checks if the given value is a valid decimal value as defined in the MOF specification.
    /// </summary>
    /// <returns>
    /// decimalValue = [ "+" / "-" ] unsignedDecimalValue
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    /// <summary>
    /// Checks if the given value represents an unsigned decimal value.
    /// </summary>
    /// <remarks>
    /// unsignedDecimalValue = positiveDecimalDigit *decimalDigit
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsUnsignedDecimalValue(string value)
    {
        if (string.IsNullOrEmpty(value)) { return false; }
        return StringValidator.IsPositiveDecimalDigit(value.First()) &&
               value.Skip(1).All(StringValidator.IsDecimalDigit);
    }

    #endregion

    #region 7.6.1.2 Real value

    /// <summary>
    /// Checks if the given value is a valid real value as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// realValue = ["+" / "-"] * decimalDigit "." 1*decimalDigit
    ///             [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsRealValue(string value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Checks if the specified character is a decimal digit as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// decimalDigit = "0" / positiveDecimalDigit
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsDecimalDigit(char value)
    {
        return (value == '0') || StringValidator.IsPositiveDecimalDigit(value);
    }

    /// <summary>
    /// Checks if the specified character is a positive decimal digit as defined in the MOF specification.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>
    /// positiveDecimalDigit = "1"..."9"
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsPositiveDecimalDigit(char value)
    {
        return value is >= '1' and <= '9';
    }

    #endregion

    #region 7.6.1.3 String values

    /// <summary>
    /// Checks if the specified character is a backslash as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// BACKSLASH = U+005C ; \
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsBackslash(char @char)
    {
        return (@char == Constants.BACKSLASH);
    }

    /// <summary>
    /// Checks if the specified character is a double quote as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// DOUBLEQUOTE = U+0022 ; "
    /// </remarks>>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsDoubleQuote(char @char)
    {
        return (@char == Constants.DOUBLEQUOTE);
    }

    /// <summary>
    /// Checks if the specified character is a single quote as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// SINGLEQUOTE = U+0027 ; '
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsSingleQuote(char @char)
    {
        return (@char == Constants.SINGLEQUOTE);
    }

    /// <summary>
    /// Checks if the specified character is an uppercase alphabetic character as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// UPPERALPHA = U+0041...U+005A ; A ... Z
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsUpperAlpha(char @char)
    {
        return @char is >= '\u0041' and <= '\u005A';
    }

    /// <summary>
    /// Checks if the specified character is a lowercase alphabetic character as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// LOWERALPHA = U+0061...U+007A ; a ... z
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsLowerAlpha(char @char)
    {
        return @char is >= '\u0061' and <= '\u007A';
    }

    /// <summary>
    /// Checks if the specified character is an underscore as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// UNDERSCORE = U+005F ; _
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsUnderscore(char @char)
    {
        return (@char == Constants.UNDERSCORE);
    }

    #endregion

    #region 7.6.1.5 Boolean value

    /// <summary>
    /// Checks if the given value is a valid representation of a false boolean value as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// FALSE = "false" ; keyword: case insensitive
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsFalse(string value)
    {
        return string.Equals(value, Constants.FALSE, StringComparison.OrdinalIgnoreCase);
    }


    /// <summary>
    /// Checks if the given value is a valid representation of a true boolean value as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// TRUE = "true" ; keyword: case insensitive
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsTrue(string value)
    {
        return string.Equals(value, Constants.TRUE, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region 7.6.1.6 Null value

    /// <summary>
    /// Checks if the given value is a valid representation of a null value as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// NULL = "null" ; keyword: case insensitive
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsNull(string value)
    {
        return string.Equals(value, Constants.NULL, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region 7.7.1 Names

    /// <summary>
    /// Checks if the given value is a valid identifier as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// IDENTIFIER = firstIdentifierChar *( nextIdentifierChar )
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsIdentifier(string value)
    {
        return !string.IsNullOrEmpty(value) &&
               StringValidator.IsFirstIdentifierChar(value.First()) &&
               value.Skip(1).All(StringValidator.IsNextIdentifierChar);
    }

    /// <summary>
    /// Checks if the specified character is a valid first character of an identifier as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsFirstIdentifierChar(char value)
    {
        return StringValidator.IsUpperAlpha(value) ||
               StringValidator.IsLowerAlpha(value) ||
               StringValidator.IsUnderscore(value);
    }

    /// <summary>
    /// Checks if the specified character is a valid next character of an identifier as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// nextIdentifierChar = firstIdentifierChar / decimalDigit
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsNextIdentifierChar(char value)
    {
        return StringValidator.IsFirstIdentifierChar(value) ||
               StringValidator.IsDecimalDigit(value);
    }

    /// <summary>
    /// Checks if the given value is a valid element name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// elementName = localName / schemaQualifiedName
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsElementName(string value)
    {
        return StringValidator.IsLocalName(value) ||
               StringValidator.IsSchemaQualifiedName(value);
    }

    /// <summary>
    /// Checks if the given value is a valid local name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// localName = IDENTIFIER
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsLocalName(string value)
    {
        return StringValidator.IsIdentifier(value);
    }

    #endregion

    #region 7.7.2 Schema-qualified name

    /// <summary>
    /// Check if the given value is a valid schema-qualified name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    /// <summary>
    /// Check if the given value is a valid schema name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// schemaName = firstSchemaChar *( nextSchemaChar )
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsSchemaName(string value)
    {
        return !string.IsNullOrEmpty(value) &&
               StringValidator.IsFirstSchemaChar(value[0]) &&
               value.Skip(1).All(StringValidator.IsNextSchemaChar);
    }

    /// <summary>
    /// Check if the specified character is a valid first character of a schema name as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// firstSchemaChar = UPPERALPHA / LOWERALPHA
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsFirstSchemaChar(char value)
    {
        return StringValidator.IsUpperAlpha(value) ||
               StringValidator.IsLowerAlpha(value);
    }

    /// <summary>
    /// CHeck if the specified character is a valid next character of a schema name as defined in the MOF specification.
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// nextSchemaChar = firstSchemaChar / decimalDigit
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsNextSchemaChar(char value)
    {
        return StringValidator.IsFirstSchemaChar(value) ||
               StringValidator.IsDecimalDigit(value);
    }

    #endregion

    #region 7.7.3 Alias identifier

    /// <summary>
    /// Check if the given value is a valid alias identifier as defined in the MOF specification.
    /// </summary>
    /// <remarks>
    /// aliasIdentifier = "$" IDENTIFIER
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsAliasIdentifier(string value)
    {
        return !string.IsNullOrEmpty(value) &&
               (value.First() == '$') &&
               StringValidator.IsIdentifier(value[1..]);
    }

    #endregion

}
