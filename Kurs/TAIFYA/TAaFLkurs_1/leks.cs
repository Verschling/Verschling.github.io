using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TAaFLkurs_1
{
    
    internal class leks
    {
        string leksem;
        string tip;
        public TokenType type;
        public string Value;
        public leks(string leksem, string tip, TokenType tipe)
        {
            this.leksem = leksem;
            this.tip = tip;
            this.type = tipe;
        }

        public leks(string leksem, string tipe)
        {
            this.leksem = leksem;
            this.tip = tipe;
        }
        public leks(TokenType tipe)
        {
            this.type = tipe;
        }
        public enum TokenType
        {
            VAR, INTEGER, BEGIN, TO, DO, END, COMMA,
            OBRACKET, CBRACKET, PLUS, MINUS, MULTIPLICATION,
            DIVISION, SEMICOLON, COLON, DOT, ARTICLE,
            IDENTIFIER, EQUALIZATION, NUMBER, CASE, ELSE, OF, EQUAL
        }
        public string getLeksem()
        {
            return leksem;
        }
        public string getTipe()
        {
            return tip;
        }
        public TokenType getTokenType()
        {
            return type;
        }
        public void getAll(ref string L, ref string T)
        {
            L = leksem;
            T = tip;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", leksem, tip);
        }

        public static Dictionary<string, TokenType> SpecialWords = new
        Dictionary<string, TokenType>()
        {
            {"var", TokenType.VAR },
            {"integer", TokenType.INTEGER },
            {"begin",TokenType.BEGIN },
            {"to",TokenType.TO },
            {"do", TokenType.DO },
            {"end", TokenType.END },
            {"case", TokenType.CASE },
            {"else", TokenType.ELSE },
            {"of", TokenType.OF}
        };
        static TokenType[] Delimiters = new TokenType[]
        {
            TokenType.OBRACKET, TokenType.CBRACKET, TokenType.PLUS,
            TokenType.MINUS, TokenType.EQUAL, TokenType.MULTIPLICATION,
            TokenType.DIVISION,TokenType.COMMA, TokenType.SEMICOLON,
            TokenType.COLON, TokenType.DOT, TokenType.ARTICLE
        };
        public static bool IsSpecialWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return SpecialWords.ContainsKey(word);
        }
        public static Dictionary<string, TokenType> SpecialSymbols = new
       Dictionary<string, TokenType>()
        {
            { "(", TokenType.OBRACKET },
            { ")", TokenType.CBRACKET },
            { "+", TokenType.PLUS },
            { "-", TokenType.MINUS },
            { "=", TokenType.EQUAL },
            { "*", TokenType.MULTIPLICATION},
            { "/", TokenType.DIVISION },
            { ",", TokenType.COMMA },
            { ";", TokenType.SEMICOLON },
            { ":", TokenType.COLON },
            { ".", TokenType.DOT },
            { "\'", TokenType.ARTICLE },
            { ":=", TokenType.EQUALIZATION }
        };
        public static bool IsSpecialSymbol(string ch)
        {
            return SpecialSymbols.ContainsKey(ch);
        }
    }
}
