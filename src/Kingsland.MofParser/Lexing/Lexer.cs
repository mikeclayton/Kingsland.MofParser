using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Lexing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System.Numerics;
using System.Text;

namespace Kingsland.MofParser.Lexing;

public static class Lexer
{

    #region Methods

    public static ParseFx.Lexing.Lexer Create()
    {
        return new ParseFx.Lexing.Lexer()
            .AddScanner('$', Lexer.ReadAliasIdentifierToken)
            .AddScanner(']', Lexer.ReadAttributeCloseToken)
            .AddScanner('[', Lexer.ReadAttributeOpenToken)
            .AddScanner('}', Lexer.ReadBlockCloseToken)
            .AddScanner('{', Lexer.ReadBlockOpenToken)
            .AddScanner(':', Lexer.ReadColonToken)
            .AddScanner(',', Lexer.ReadCommaToken)
            .AddScanner('/', Lexer.ReadCommentToken)
            .AddScanner('=', Lexer.ReadEqualsOperatorToken)
            .AddScanner(')', Lexer.ReadParenthesisCloseToken)
            .AddScanner('(', Lexer.ReadParenthesisOpenToken)
            .AddScanner('#', Lexer.ReadPragmaToken)
            .AddScanner(';', Lexer.ReadStatementEndToken)
            .AddScanner('"', Lexer.ReadStringLiteralToken)
            .AddScanner("[+|\\-]", Lexer.ReadNumericLiteralToken)
            .AddScanner('.', (reader) => {
                // if the next character is a decimalDigit then we're reading a RealLiteralToken with
                // no leading digits before the decimal point (e.g. ".45"), otherwise we're reading
                // a DotOperatorToken (e.g. the "." in "MyPropertyValue = MyEnum.Value;")
                var readAhead = reader.Read().NextReader;
                if (!readAhead.Eof() && StringValidator.IsDecimalDigit(readAhead.Peek().Value))
                {
                    return Lexer.ReadNumericLiteralToken(reader);
                }
                else
                {
                    return Lexer.ReadDotOperatorToken(reader);
                }
            })
            .AddScanner("[a-z|A-Z|_]", (reader) => {
                // firstIdentifierChar
                var result = Lexer.ReadIdentifierToken(reader);
                var identifierToken = (IdentifierToken)result.Token;
                var normalized = identifierToken.Name.ToLowerInvariant();
                switch (normalized)
                {
                    case Constants.FALSE:
                        var falseToken = new BooleanLiteralToken(identifierToken.Extent, false);
                        return new ScannerResult(falseToken, result.NextReader);
                    case Constants.TRUE:
                        var trueToken = new BooleanLiteralToken(identifierToken.Extent, true);
                        return new ScannerResult(trueToken, result.NextReader);
                    case Constants.NULL:
                        var nullLiteralToken = new NullLiteralToken(identifierToken.Extent);
                        return new ScannerResult(nullLiteralToken, result.NextReader);
                    default:
                        return new ScannerResult(identifierToken, result.NextReader);
                }
            })
            .AddScanner("[0-9]", Lexer.ReadNumericLiteralToken)
            .AddScanner(
                [
                    '\u0020', // space
                    '\u0009', // horizontal tab
                    '\u000D', // carriage return
                    '\u000A'  // line feed
                ],
                Lexer.ReadWhitespaceToken
            );
    }

    public static IEnumerable<SyntaxToken> Lex(SourceReader reader)
    {
        return Lexer.Create().ReadToEnd(reader);
    }

    #endregion

    #region Symbols

    private static ScannerResult ReadAttributeCloseToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read(']');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new AttributeCloseToken(extent), nextReader);
    }

    private static ScannerResult ReadAttributeOpenToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read('[');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new AttributeOpenToken(extent), nextReader);
    }

    private static ScannerResult ReadBlockCloseToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read('}');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new BlockCloseToken(extent), nextReader);
    }

    private static ScannerResult ReadBlockOpenToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read('{');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new BlockOpenToken(extent), nextReader);
    }

    private static ScannerResult ReadColonToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read(':');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new ColonToken(extent), nextReader);
    }

    private static ScannerResult ReadCommaToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read(',');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new CommaToken(extent), nextReader);
    }

    private static ScannerResult ReadDotOperatorToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read('.');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new DotOperatorToken(extent), nextReader);
    }

    private static ScannerResult ReadEqualsOperatorToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read('=');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new EqualsOperatorToken(extent), nextReader);
    }

    private static ScannerResult ReadParenthesisCloseToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read(')');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new ParenthesisCloseToken(extent), nextReader);
    }

    private static ScannerResult ReadParenthesisOpenToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read('(');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new ParenthesisOpenToken(extent), nextReader);
    }

    private static ScannerResult ReadStatementEndToken(SourceReader reader)
    {
        var (sourceChar, nextReader) = reader.Read(';');
        var extent = SourceExtent.From(sourceChar);
        return new ScannerResult(new StatementEndToken(extent), nextReader);
    }

    #endregion

    #region 5.2 Whitespace

    /// <summary>
    ///
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 5.2 Whitespace
    ///
    /// Whitespace in a MOF file is any combination of the following characters:
    ///
    ///     * Space (U+0020),
    ///     * Horizontal Tab (U+0009),
    ///     * Carriage Return (U+000D) and
    ///     * Line Feed (U+000A).
    ///
    /// The WS ABNF rule represents any one of these whitespace characters:
    ///
    ///     WS = U+0020 / U+0009 / U+000D / U+000A
    ///
    /// </remarks>
    private static ScannerResult ReadWhitespaceToken(SourceReader reader)
    {
        var thisReader = reader;
        var sourceChars = new List<SourceChar>();
        // read the first whitespace character
        (var sourceChar, thisReader) = thisReader.Read(StringValidator.IsWhitespace);
        sourceChars.Add(sourceChar);
        // read the remaining whitespace
        while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsWhitespace))
        {
            (sourceChar, thisReader) = thisReader.Read();
            sourceChars.Add(sourceChar);
        }
        // return the result
        var text = SourceExtent.ConvertToString(sourceChars);
        return new ScannerResult(
            new WhitespaceToken(
                sourceChars.First().Position,
                sourceChars.Last().Position,
                text
            ),
            thisReader
        );
    }

    #endregion

    #region 5.4 Comments

    /// <summary>
    ///
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 5.4 Comments
    ///
    /// Comments in a MOF file do not create, modify, or annotate language elements. They shall be treated as if
    /// they were whitespace.
    ///
    /// Comments may appear anywhere in MOF syntax where whitespace is allowed and are indicated by either
    /// a leading double slash (//) or a pair of matching /* and */ character sequences. Occurrences of these
    /// character sequences in string literals shall not be treated as comments.
    ///
    /// A // comment is terminated by the end of line (see 5.3), as shown in the example below.
    ///
    ///     Integer MyProperty; // This is an example of a single-line comment
    ///
    /// A comment that begins with /* is terminated by the next */ sequence, or by the end of the MOF file,
    /// whichever comes first.
    ///
    ///     /* example of a comment between property definition tokens and a multi-line comment */
    ///     Integer /* 16-bit integer property */ MyProperty; /* and a multi-line
    ///                             comment */
    ///
    /// </remarks>
    private static ScannerResult ReadCommentToken(SourceReader reader)
    {
        var thisReader = reader;
        var sourceChars = new List<SourceChar>();
        // read the starting '/'
        (var sourceChar, thisReader) = thisReader.Read('/');
        sourceChars.Add(sourceChar);
        switch (thisReader.Peek().Value)
        {
            case '/': // single-line
                (sourceChar, thisReader) = thisReader.Read('/');
                sourceChars.Add(sourceChar);
                // read the comment text
                while (!thisReader.Eof() && !thisReader.Peek(StringValidator.IsLineTerminator))
                {
                    (sourceChar, thisReader) = thisReader.Read();
                    sourceChars.Add(sourceChar);
                }
                break;
            case '*': // multi-line
                (sourceChar, thisReader) = thisReader.Read('*');
                sourceChars.Add(sourceChar);
                // read the comment text
                while (!thisReader.Eof())
                {
                    (sourceChar, thisReader) = thisReader.Read();
                    sourceChars.Add(sourceChar);
                    if ((sourceChar.Value == '*') && thisReader.Peek('/'))
                    {
                        // read the closing sequence
                        (sourceChar, thisReader) = thisReader.Read('/');
                        sourceChars.Add(sourceChar);
                        break;
                    }
                }
                break;
            default:
                throw new UnexpectedCharacterException(reader.Peek());
        }
        // return the result
        var text = SourceExtent.ConvertToString(sourceChars);
        return new ScannerResult(
            new CommentToken(
                sourceChars.First().Position,
                sourceChars.Last().Position,
                text
            ),
            thisReader
        );
    }

    #endregion

    #region 7.3 Compiler directives

    /// <summary>
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.3 Compiler directives
    ///
    /// Compiler directives direct the processing of MOF files. Compiler directives do not create, modify, or
    /// annotate the language elements.
    ///
    /// Compiler directives shall conform to the format defined by ABNF rule compilerDirective (whitespace
    /// as defined in 5.2 is allowed between the elements of the rules in this ABNF section):
    ///
    ///     compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
    ///                         "(" pragmaParameter ")"
    ///
    ///     pragmaName         = directiveName
    ///     standardPragmaName = INCLUDE
    ///     pragmaParameter    = stringValue ; if the pragma is INCLUDE,
    ///                                      ; the parameter value
    ///                                      ; shall represent a relative
    ///                                      ; or full file path
    ///     PRAGMA             = "#pragma"  ; keyword: case insensitive
    ///     INCLUDE            = "include"  ; keyword: case insensitive
    ///
    ///     directiveName      = org-id "_" IDENTIFIER
    ///
    /// </remarks>
    private static ScannerResult ReadPragmaToken(SourceReader reader)
    {
        var (sourceChars, thisReader) = reader.ReadString(Constants.PRAGMA, true);
        var extent = SourceExtent.From(sourceChars);
        return new ScannerResult(new PragmaToken(extent), thisReader);
    }

    #endregion

    #region 7.7.1 Names

    /// <summary>
    ///
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.7.1 Names
    ///
    /// MOF names are identifiers with the format defined by the IDENTIFIER rule.
    ///
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     IDENTIFIER          = firstIdentifierChar *( nextIdentifierChar )
    ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
    ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
    ///     elementName         = localName / schemaQualifiedName
    ///     localName           = IDENTIFIER
    ///
    /// </remarks>
    private static ScannerResult ReadIdentifierToken(SourceReader reader)
    {
        var thisReader = reader;
        var sourceChars = new List<SourceChar>();
        var nameChars = new StringBuilder();
        // firstIdentifierChar
        (var sourceChar, thisReader) = thisReader.Read(StringValidator.IsFirstIdentifierChar);
        sourceChars.Add(sourceChar);
        nameChars.Append(sourceChar.Value);
        // *( nextIdentifierChar )
        while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsNextIdentifierChar))
        {
            (sourceChar, thisReader) = thisReader.Read();
            sourceChars.Add(sourceChar);
            nameChars.Append(sourceChar.Value);
        }
        // return the result
        var extent = SourceExtent.From(sourceChars);
        var name = nameChars.ToString();
        return new ScannerResult(new IdentifierToken(extent, name), thisReader);
    }

    #endregion

    #region 7.7.3 Alias identifier

    /// <summary>
    ///
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.7.3 Alias identifier
    ///
    /// An aliasIdentifier identifies an Instance or Value within the context of a MOF compilation unit.
    ///
    /// No whitespace is allowed between the elements of this rule.
    ///
    ///     aliasIdentifier     = "$" IDENTIFIER
    ///
    ///     IDENTIFIER          = firstIdentifierChar *( nextIdentifierChar )
    ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
    ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
    ///     elementName         = localName / schemaQualifiedName
    ///     localName           = IDENTIFIER
    ///
    /// </remarks>
    private static ScannerResult ReadAliasIdentifierToken(SourceReader reader)
    {
        var thisReader = reader;
        var sourceChars = new List<SourceChar>();
        var nameChars = new StringBuilder();
        // "$"
        (var sourceChar, thisReader) = thisReader.Read('$');
        sourceChars.Add(sourceChar);
        // firstIdentifierChar
        (sourceChar, thisReader) = thisReader.Read(StringValidator.IsFirstIdentifierChar);
        sourceChars.Add(sourceChar);
        nameChars.Append(sourceChar.Value);
        // *( nextIdentifierChar )
        while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsNextIdentifierChar))
        {
            (sourceChar, thisReader) = thisReader.Read();
            sourceChars.Add(sourceChar);
            nameChars.Append(sourceChar.Value);
        }
        // return the result
        var extent = SourceExtent.From(sourceChars);
        var name = nameChars.ToString();
        return new ScannerResult(new AliasIdentifierToken(extent, name), thisReader);
    }

    #endregion

    #region 7.6.1.1 Integer value / 7.6.1.2 Real value

    /// <summary>
    ///
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.1 Integer value
    ///
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     integerValue         = binaryValue / octalValue / hexValue / decimalValue
    ///
    ///     binaryValue          = [ "+" / "-" ] 1*binaryDigit ( "b" / "B" )
    ///     binaryDigit          = "0" / "1"
    ///
    ///     octalValue           = [ "+" / "-" ] unsignedOctalValue
    ///     unsignedOctalValue   = "0" 1*octalDigit
    ///     octalDigit           = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7"
    ///
    ///     hexValue             = [ "+" / "-" ] ( "0x" / "0X" ) 1*hexDigit
    ///     hexDigit             = decimalDigit / "a" / "A" / "b" / "B" / "c" / "C" /
    ///                                           "d" / "D" / "e" / "E" / "f" / "F"
    ///
    ///     decimalValue         = [ "+" / "-" ] unsignedDecimalValue
    ///     unsignedDecimalValue = "0" / positiveDecimalDigit *decimalDigit
    ///
    ///     decimalDigit         = "0" / positiveDecimalDigit
    ///     positiveDecimalDigit = "1"..."9"
    ///
    /// 7.6.1.2 Real value
    ///
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     realValue            = [ "+" / "-" ] *decimalDigit "." 1*decimalDigit
    ///                            [ ( "e" / "E" ) [ "+" / "-" ] 1*decimalDigit ]
    ///
    ///     decimalDigit         = "0" / positiveDecimalDigit
    ///     positiveDecimalDigit = "1"..."9"
    ///
    /// </remarks>
    private static ScannerResult ReadNumericLiteralToken(SourceReader reader)
    {


        static int CharToDigit(char c, int radix)
        {
            return c switch
            {
                >= '0' and <= '9' => c - '0',
                >= 'a' and <= 'z' => c - 'a' + 10,
                >= 'A' and <= 'Z' => c - 'A' + 10,
                _ =>
                    throw new FormatException($"Digit '{c}' is not valid for base {radix}.")
            };
        }

        static long ParseIntegerValueDigits(IEnumerable<SourceChar> chars, int radix, int sign = 1)
        {
            if (radix < 2 || radix > 36)
            {
                throw new ArgumentOutOfRangeException(nameof(radix), "Value must be between 2 and 36.");
            }
            var literalValue = new BigInteger(0);
            foreach (var digit in chars)
            {
                var digitValue = CharToDigit(digit.Value, radix);
                literalValue = (literalValue * radix) + digitValue;
            }
            if (literalValue < long.MinValue || literalValue > long.MaxValue)
            {
                throw new OverflowException($"Value value is out of range for a {typeof(long).Name}.");
            }
            return (sign == -1) ? -(long)literalValue : (long)literalValue;
        }

        static double ParseFractionValueDigits(IEnumerable<SourceChar> chars, int sign = 1)
        {
            var literalValue = 0.0;
            var radix = 10;
            var scale = 1.0 / radix;
            foreach (var digit in chars)
            {
                var digitValue = CharToDigit(digit.Value, radix);
                literalValue += digitValue * scale;
                scale /= radix;
            }
            return (sign == -1) ? -literalValue : literalValue;
        }

        static long BigIntegerToLong(BigInteger value)
        {
            return (long)value;
        }

        const int stateInvalidState = -1;
        const int stateLeadingSign = 1;
        const int stateFirstDigitBlock = 2;
        const int stateOctalOrDecimalValue = 3;
        const int stateBinaryValue = 4;
        const int stateOctalValue = 5;
        const int stateHexValue = 6;
        const int stateDecimalValue = 7;
        const int stateRealDecimalPoint = 8;
        const int stateRealValueInteger = 9;
        const int stateRealValueFraction = 10;
        const int stateRealValueExponent = 11;
        const int stateRealValueComplete = 12;
        const int stateDone = 99;

        var thisReader = reader;
        var sourceChars = new List<SourceChar>();
        var token = default(SyntaxToken);

        // the sign for the integer part of the number
        //  1 => '+' (or missing)
        // -1 => '-'
        var integerSign = 1;

        // the part of the number before the decimal place
        var integerSourceChars = new List<SourceChar>();

        // the part of the number after the decimal place
        var fractionSourceChars = new List<SourceChar>();

        // the part of the number that contains the exponent
        var exponentSourceChars = new List<SourceChar>();

        // the sign for the exponent part of the number
        //  1 => '+' (or missing)
        // -1 => '-'
        var exponentSign = 1;

        var currentState = stateLeadingSign;
        while (currentState != stateDone)
        {
            var nextState = stateInvalidState;
            switch (currentState)
            {

                case stateLeadingSign:
                    {
                        // read the optional leading sign
                        // [ "+" / "-" ]
                        var peekChar = thisReader.Peek();
                        if (peekChar.Value is '+' or '-')
                        {
                            (var signChar, thisReader) = thisReader.Read();
                            sourceChars.Add(signChar);
                            integerSign = (signChar.Value == '-') ? -1 : 1;
                            peekChar = thisReader.Peek();
                        }
                        // if the next character is a decimal place we're reading a realValue with no leading integer part
                        // (e.g. ".45", "+.45", "-.45")
                        if (peekChar.Value is '.')
                        {
                            nextState = stateRealDecimalPoint;
                            break;
                        }
                        // otherwise, start reading digits
                        nextState = stateFirstDigitBlock;
                        break;
                    }

                case stateFirstDigitBlock:
                    {
                        // the first digit block could be:
                        //
                        // * a binaryValue                   => 1*binaryDigit ( "b" / "B" )
                        // * an octalValue                   => "0" 1*octalDigit
                        // * a hexValue prefix               => "0x" / "0X"
                        // * a decimalValue                  => "0" / positiveDecimalDigit *decimalDigit
                        // * the integer part of a realValue => *decimalDigit
                        var peekChar = thisReader.Peek();
                        // we don't know which base the value is in yet, but if it's hexadecimal then
                        // we should be reading the "0" from the "0x" prefix here, so regardless of base
                        // the next thing we want to read is a block of decimal digits
                        (var firstDigitBlockChars, thisReader) = thisReader.ReadWhile(StringValidator.IsDecimalDigit);
                        sourceChars.AddRange(firstDigitBlockChars);
                        // now we can do some validation
                        if (firstDigitBlockChars.Count == 0)
                        {
                            throw new UnexpectedCharacterException(
                                thisReader.Peek() ?? throw new NullReferenceException()
                            );
                        }
                        // if we've reached the end of the stream then there's no suffix
                        // (e.g. 'b', 'x') so this can't be a realValue (no '.'), a
                        // binaryValue (no 'b' suffix) or hexadecimalValue (no 'x' suffix)
                        // so it must be an octalValue or decimalValue
                        if (thisReader.Eof())
                        {
                            integerSourceChars = firstDigitBlockChars;
                            nextState = stateOctalOrDecimalValue;
                            break;
                        }
                        // check the next character to see if it tells us anything
                        // about which type of literal we're reading
                        peekChar = thisReader.Peek();
                        if (peekChar.Value is 'b' or 'B')
                        {
                            // binaryValue
                            integerSourceChars = firstDigitBlockChars;
                            nextState = stateBinaryValue;
                        }
                        else if (peekChar.Value is 'x' or 'X')
                        {
                            // hexValue
                            integerSourceChars = firstDigitBlockChars;
                            nextState = stateHexValue;
                        }
                        else if (peekChar.Value == '.')
                        {
                            // realValue
                            integerSourceChars = firstDigitBlockChars;
                            nextState = stateRealValueInteger;
                        }
                        else
                        {
                            // octalValue or decimalValue
                            integerSourceChars = firstDigitBlockChars;
                            nextState = stateOctalOrDecimalValue;
                        }
                        break;
                    }

                case stateOctalOrDecimalValue:
                    {
                        // we're reading an octalValue or decimalValue, but we're not sure which yet...
                        if ((integerSourceChars.Count > 1) && (integerSourceChars[0].Value == '0'))
                        {
                            nextState = stateOctalValue;
                        }
                        else
                        {
                            nextState = stateDecimalValue;
                        }
                        break;
                    }

                case stateBinaryValue:
                    {
                        // we're trying to read a binaryValue, so check all the characters in the digit block are valid,
                        // i.e. 1*binaryDigit
                        var invalidChar = integerSourceChars.FirstOrDefault(c => !StringValidator.IsBinaryDigit(c.Value));
                        if (invalidChar is not null)
                        {
                            throw new UnexpectedCharacterException(
                                invalidChar ?? throw new NullReferenceException()
                            );
                        }
                        // all the characters are valid, so consume the 'b' suffix
                        (var sourceChar, thisReader) = thisReader.Read(c => (c is 'b' or 'B'));
                        sourceChars.Add(sourceChar);
                        // now build the return value
                        var binaryValue = ParseIntegerValueDigits(integerSourceChars, 2, integerSign);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.BinaryValue, BigIntegerToLong(binaryValue));
                        // and we're done
                        nextState = stateDone;
                        break;
                    }

                case stateOctalValue:
                    {
                        // we're trying to read an octalValue (since decimalValue can't start with a
                        // leading '0') so check all the characters in the digit block are valid,
                        // i.e. "0" 1*octalDigit
                        if ((integerSourceChars.Count < 2) || (integerSourceChars[0].Value != '0'))
                        {
                            throw new UnexpectedCharacterException(
                                integerSourceChars[0] ?? throw new NullReferenceException()
                            );
                        }
                        var invalidChar = integerSourceChars.Skip(1).FirstOrDefault(c => !StringValidator.IsOctalDigit(c.Value));
                        if (invalidChar is not null)
                        {
                            throw new UnexpectedCharacterException(
                                invalidChar ?? throw new NullReferenceException()
                            );
                        }
                        // now build the return value
                        var octalValue = ParseIntegerValueDigits(integerSourceChars, 8, integerSign);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.OctalValue, BigIntegerToLong(octalValue));
                        // and we're done
                        nextState = stateDone;
                        break;
                    }

                case stateHexValue:
                    {
                        // we're trying to read a hexValue, so we should have just read a leading zero
                        if ((integerSourceChars.Count != 1) || (integerSourceChars[0].Value != '0'))
                        {
                            throw new UnexpectedCharacterException(
                                integerSourceChars[0] ?? throw new NullReferenceException()
                           );
                        }
                        // check for a 'x' next
                        (var sourceChar, thisReader) = thisReader.Read(c => c is 'x' or 'X');
                        sourceChars.Add(sourceChar);
                        // 1*hexDigit
                        (var hexDigits, thisReader) = thisReader.ReadWhile(StringValidator.IsHexDigit);
                        if (hexDigits.Count == 0)
                        {
                            throw new UnexpectedCharacterException(thisReader.Peek());
                        }
                        sourceChars.AddRange(hexDigits);
                        // build the return value
                        var hexValue = ParseIntegerValueDigits(hexDigits, 16, integerSign);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.HexValue, BigIntegerToLong(hexValue));
                        // and we're done
                        nextState = stateDone;
                        break;
                    }

                case stateDecimalValue:
                    {
                        // we're trying to read a decimalValue (since that's the only remaining option),
                        // so check all the characters in the digit block are valid,
                        // i.e. "0" / positiveDecimalDigit *decimalDigit
                        if ((integerSourceChars.Count == 1) && (integerSourceChars[0].Value == '0'))
                        {
                            // "0"
                        }
                        else if (!StringValidator.IsPositiveDecimalDigit(integerSourceChars[0].Value))
                        {
                            // decimalValues with more than one digit can't start with a zero,
                            // (because that would make it an octalValue instead), so the first
                            // digit must be a positiveDecimalDigit
                            throw new UnexpectedCharacterException(
                                integerSourceChars[0] ?? throw new NullReferenceException()
                            );
                        }
                        else
                        {
                            var invalidChar = integerSourceChars.FirstOrDefault(c => !StringValidator.IsDecimalDigit(c.Value));
                            if (invalidChar is not null)
                            {
                                throw new UnexpectedCharacterException(
                                    invalidChar ?? throw new NullReferenceException()
                                );
                            }
                        }
                        // build the return value
                        var decimalValue = ParseIntegerValueDigits(integerSourceChars, 10, integerSign);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.DecimalValue, BigIntegerToLong(decimalValue));
                        // and we're done
                        nextState = stateDone;
                        break;
                    }

                case stateRealValueInteger:
                    {
                        // we're trying to read a realValue, so check all the characters in the digit block are valid,
                        // i.e. *decimalDigit
                        var invalidChar = integerSourceChars.FirstOrDefault(c => !StringValidator.IsDecimalDigit(c.Value));
                        if (invalidChar is not null)
                        {
                            throw new UnexpectedCharacterException(
                                invalidChar ?? throw new NullReferenceException()
                            );
                        }
                        // all the characters are valid, so consume a decimal point
                        // (if there's no decimal point this would be an integer value, not a real value)
                        nextState = stateRealDecimalPoint;
                        break;
                    }

                case stateRealDecimalPoint:
                    {
                        // read the decimal point for a realValue
                        (var sourceChar, thisReader) = thisReader.Read('.');
                        sourceChars.Add(sourceChar);
                        nextState = stateRealValueFraction;
                        break;
                    }

                case stateRealValueFraction:
                    {
                        // 1*decimalDigit
                        (fractionSourceChars, thisReader) = thisReader.ReadWhile(StringValidator.IsDecimalDigit);
                        if (fractionSourceChars.Count == 0)
                        {
                            throw new UnexpectedCharacterException(thisReader.Peek());
                        }
                        sourceChars.AddRange(fractionSourceChars);
                        // ( "e" / "E" )
                        if (!thisReader.Eof())
                        {
                            var peekChar = thisReader.Peek();
                            if (peekChar.Value is 'e' or 'E')
                            {
                                nextState = stateRealValueExponent;
                                break;
                            }
                        }
                        nextState = stateRealValueComplete;
                        break;
                    }

                case stateRealValueExponent:
                    {
                        // read the exponent character
                        // ( "e" / "E" )
                        (var sourceChar, thisReader) = thisReader.Read(c => c is 'e' or 'E');
                        sourceChars.Add(sourceChar);
                        // read the optional exponent sign
                        // [ "+" / "-" ]
                        var peekChar = thisReader.Peek();
                        if (peekChar.Value is '+' or '-')
                        {
                            (var signChar, thisReader) = thisReader.Read();
                            sourceChars.Add(signChar);
                            exponentSign = (signChar.Value == '-') ? -1 : 1;
                        }
                        // read the exponent value
                        // 1*decimalDigit
                        (exponentSourceChars, thisReader) = thisReader.ReadWhile(StringValidator.IsDecimalDigit);
                        sourceChars.AddRange(exponentSourceChars);
                        nextState = stateRealValueComplete;
                        break;
                    }

                case stateRealValueComplete:
                    {
                        // build the return value
                        var realIntegerValue = ParseIntegerValueDigits(integerSourceChars, 10, integerSign);
                        var realFractionValue = ParseFractionValueDigits(fractionSourceChars, integerSign);
                        var realExponentValue = (exponentSourceChars.Count > 0)
                            ? ParseIntegerValueDigits(exponentSourceChars, 10, exponentSign)
                            : 0;
                        token = new RealLiteralToken(
                            SourceExtent.From(sourceChars),
                            (realIntegerValue + realFractionValue) * Math.Pow(10, realExponentValue)
                        );
                        // and we're done
                        nextState = stateDone;
                        break;
                    }

                case stateDone:
                    // the main while loop should exit before we ever get here
                    throw new InvalidOperationException();

                default:
                    throw new InvalidOperationException();

            }

            currentState = nextState;
        }

        return new ScannerResult(
            token ?? throw new NullReferenceException(),
            thisReader
        );

    }

    #endregion

    #region 7.6.1.3 String values

    /// <summary>
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.3 String values
    ///
    /// Unless explicitly specified via ABNF rule WS, no whitespace is allowed between the elements of the rules
    /// in this ABNF section.
    ///
    ///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
    ///     stringValue       = singleStringValue *( *WS singleStringValue )
    ///
    ///     stringChar        = stringUCSchar / stringEscapeSequence
    ///     stringUCSchar     = U+0020...U+0021 / U+0023...U+D7FF /
    ///                         U+E000...U+FFFD / U+10000...U+10FFFF
    ///                         ; Note that these UCS characters can be
    ///                         ; represented in XML without any escaping
    ///                         ; (see W3C XML).
    ///
    ///     stringEscapeSequence = BACKSLASH ( BACKSLASH / DOUBLEQUOTE / SINGLEQUOTE /
    ///                            BACKSPACE_ESC / TAB_ESC / LINEFEED_ESC /
    ///                            FORMFEED_ESC / CARRIAGERETURN_ESC /
    ///                            escapedUCSchar )
    ///
    ///     BACKSPACE_ESC      = "b" ; escape for back space (U+0008)
    ///     TAB_ESC            = "t" ; escape for horizontal tab(U+0009)
    ///     LINEFEED_ESC       = "n" ; escape for line feed(U+000A)
    ///     FORMFEED_ESC       = "f" ; escape for form feed(U+000C)
    ///     CARRIAGERETURN_ESC = "r" ; escape for carriage return (U+000D)
    ///
    ///     escapedUCSchar     = ( "x" / "X" ) 1*6(hexDigit ) ; escaped UCS
    ///                          ; character with a UCS code position that is
    ///                          ; the numeric value of the hex number
    ///
    /// The following special characters are also used in other ABNF rules in this specification:
    ///
    ///     BACKSLASH   = U+005C ; \
    ///     DOUBLEQUOTE = U+0022 ; "
    ///     SINGLEQUOTE = U+0027 ; '
    ///     UPPERALPHA  = U+0041...U+005A ; A ... Z
    ///     LOWERALPHA  = U+0061...U+007A ; a ... z
    ///     UNDERSCORE  = U+005F ; _
    ///
    /// </remarks>
    private static ScannerResult ReadStringLiteralToken(SourceReader reader)
    {
        // BUGBUG - no support for *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
        // BUGBUG - incomplete escape sequences
        // BUGBUG - no support for UCS characters
        var thisReader = reader;
        var sourceChars = new List<SourceChar>();
        var stringChars = new StringBuilder();
        // read the first double-quote character
        (var sourceChar, thisReader) = thisReader.Read(Constants.DOUBLEQUOTE);
        sourceChars.Add(sourceChar);
        // read the remaining characters
        bool isEscaped = false;
        bool isTerminated = false;
        while (!thisReader.Eof())
        {
            var peek = thisReader.Peek();
            if (isEscaped)
            {
                // read the second character in an escape sequence
                var escapedChar = peek.Value switch
                {
                    Constants.BACKSLASH => Constants.BACKSLASH,
                    Constants.DOUBLEQUOTE => Constants.DOUBLEQUOTE,
                    Constants.SINGLEQUOTE => Constants.SINGLEQUOTE,
                    Constants.BACKSPACE_ESC => '\b',
                    Constants.TAB_ESC => '\t',
                    Constants.LINEFEED_ESC => '\n',
                    Constants.FORMFEED_ESC => '\f',
                    Constants.CARRIAGERETURN_ESC => '\r',
                    _ => throw new UnexpectedCharacterException(peek)
                };
                thisReader = thisReader.Next();
                sourceChars.Add(peek);
                stringChars.Append(escapedChar);
                isEscaped = false;
            }
            else if (peek.Value == Constants.BACKSLASH)
            {
                // read the first character in an escape sequence
                thisReader = thisReader.Next();
                sourceChars.Add(peek);
                isEscaped = true;
            }
            else if (peek.Value == Constants.DOUBLEQUOTE)
            {
                // read the last double-quote character
                (_, thisReader) = thisReader.Read(Constants.DOUBLEQUOTE);
                sourceChars.Add(peek);
                isTerminated = true;
                break;
            }
            else
            {
                // read a literal string character
                thisReader = thisReader.Next();
                sourceChars.Add(peek);
                stringChars.Append(peek.Value);
            }
        }
        // make sure we found the end of the string
        if (!isTerminated)
        {
            throw new InvalidOperationException("Unterminated string found.");
        }
        // return the result
        var extent = SourceExtent.From(sourceChars);
        var stringValue = stringChars.ToString();
        return new ScannerResult(new StringLiteralToken(extent, stringValue), thisReader);
    }

    #endregion

}
