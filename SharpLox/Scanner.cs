using System;
using System.Collections.Generic;

namespace SharpLox
{
    public class Scanner
    {
        private static readonly Dictionary<string, TokenType> Keywords;
        private readonly string _source;
        private readonly List<Token> _tokens;
        private int _start = 0;
        private int _current = 0;
        private int _line = 1;

        static Scanner()
        {
            Keywords = new Dictionary<string, TokenType>
            {
                {"and", TokenType.And},
                {"class", TokenType.Class},
                {"else", TokenType.Else},
                {"false", TokenType.False},
                {"for", TokenType.For},
                {"fun", TokenType.Fun},
                {"if", TokenType.If},
                {"nil", TokenType.Nil},
                {"or", TokenType.Or},
                {"print", TokenType.Print},
                {"return", TokenType.Return},
                {"super", TokenType.Super},
                {"this", TokenType.This},
                {"true", TokenType.True},
                {"var", TokenType.Var},
                {"while", TokenType.While},
            };
        }

        public Scanner(
            string source)
        {
            _source = source;
            _tokens = new List<Token>();
        }

        private bool IsAtEnd => _current >= _source.Length;

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd)
            {
                // We are at the beginning of the next lexeme.
                _start = _current;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.Eof, "", null, _line));

            return _tokens;
        }

        private void ScanToken()
        {
            var c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LeftParen);
                    break;
                case ')':
                    AddToken(TokenType.RightParen);
                    break;
                case '{':
                    AddToken(TokenType.LeftBrace);
                    break;
                case '}':
                    AddToken(TokenType.RightBrace);
                    break;
                case ',':
                    AddToken(TokenType.Comma);
                    break;
                case '.':
                    AddToken(TokenType.Dot);
                    break;
                case '-':
                    AddToken(TokenType.Minus);
                    break;
                case '+':
                    AddToken(TokenType.Plus);
                    break;
                case ';':
                    AddToken(TokenType.Semicolon);
                    break;
                case '*':
                    AddToken(TokenType.Star);
                    break;
                case '!':
                    AddToken(Match('=') ? TokenType.BangEqual : TokenType.Bang);
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EqualEqual : TokenType.Equal);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LessEqual : TokenType.Less);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GreaterEqual : TokenType.Greater);
                    break;
                case '/':
                    if (Match('/'))
                    {
                        // A comment goes until the end of the line.
                        while (Peek() != '\n' && !IsAtEnd)
                        {
                            Advance();
                        }
                    }
                    else
                    {
                        AddToken(TokenType.Slash);
                    }

                    break;
                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.                      
                    break;

                case '\n':
                    _line++;
                    break;
                case '"':
                    HandleString();
                    break;
                default:
                    if (IsDigit(c))
                    {
                        HandleNumber();
                    }
                    else if (IsAlpha(c))
                    {
                        HandleIdentifier();
                    }
                    else
                    {
                        SharpLox.Error(_line, $"Unexpected character: {c}");
                    }

                    break;
            }
        }

        private void HandleString()
        {
            while (Peek() != '"' && !IsAtEnd)
            {
                if (Peek() == '\n')
                {
                    _line++;
                }

                Advance();
            }

            if (IsAtEnd)
            {
                // Unterminated string.
                SharpLox.Error(_line, "Unterminated string.");
                return;
            }

            // The closing ".
            Advance();

            var value = _source.Substring(_start + 1, _current - _start - 2);
            AddToken(TokenType.String, value);
        }

        private void HandleNumber()
        {
            while (IsDigit(Peek()))
            {
                Advance();
            }

            // Look for a fractional part.
            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                // Consume the "."
                Advance();

                while (IsDigit(Peek()))
                {
                    Advance();
                }
            }

            AddToken(TokenType.Number, double.Parse(_source.Substring(_start, _current - _start)));
        }

        private void HandleIdentifier()
        {
            while (IsAlphaNumeric(Peek()))
            {
                Advance();
            }

            var text = _source.Substring(_start, _current - _start);

            Keywords.TryGetValue(text, out var type);

            if (type == TokenType.Unknown)
            {
                type = TokenType.Identifier;
            }
            
            AddToken(type);
        }

        private char Advance()
        {
            return _source[_current++];
        }

        private void AddToken(
            TokenType type,
            object literal = null)
        {
            var text = _source.Substring(_start, _current - _start);
            _tokens.Add(new Token(type, text, literal, _line));
        }

        private bool Match(
            char expected)
        {
            if (IsAtEnd) return false;
            if (_source[_current] != expected) return false;

            _current++;
            return true;
        }

        private char Peek()
        {
            return IsAtEnd ? '\0' : _source[_current];
        }

        private bool IsAlphaNumeric(
            char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        private char PeekNext()
        {
            return _current + 1 >= _source.Length ? '\0' : _source[_current + 1];
        }

        private bool IsDigit(
            char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsAlpha(
            char c)
        {
            return c >= 'a' && c <= 'z' ||
                   c >= 'A' && c <= 'Z' ||
                   c == '_';
        }
    }
}