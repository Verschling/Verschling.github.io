using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TAaFLkurs_1.leks;

namespace TAaFLkurs_1
{
    public class Troyka
    {
        internal leks operand1;
        internal leks operand2;
        internal leks operat;
        internal Troyka(leks operat, leks opd2, leks opd1)
        {
            this.operat = operat;
            operand2 = opd2;
            operand1 = opd1;
        }
    }
    internal class braur_zemelzon
    {
        public List<Troyka> listTroyka = new List<Troyka>();
        private List<leks> tokens = new List<leks>();
        private Stack<leks> E = new Stack<leks>();
        private Stack<leks> T = new Stack<leks>();
        private int lex = 0;
        public int index = 1;
        public List<string> strings = new List<string>();
        private void ListTroykaToStrings(List<Troyka> listTroyka)
        {
            int ind = 1;
            foreach (Troyka t in listTroyka)
            {
                strings.Add($"M{ind}: {t.operat.Value}, {t.operand1.Value}, {t.operand2.Value}");
                ind++;
            }
        }
        private leks GetLexem(int lex)
        {
            return tokens[lex];
        }
        public braur_zemelzon(List<leks> expr)
        {
            tokens = expr;
        }
        private void K_id()
        {
            E.Push(GetLexem(lex));
            lex++;
        }
        private void K_op()
        {
            if (E.Count < 2)
                throw new Exception("Невозможно выполнить арифметическое выражение: число операндов не удовлетворяет условию");
            Troyka k = new Troyka(T.Pop(), E.Pop(), E.Pop());
            listTroyka.Add(k);
            leks token = new leks(TokenType.IDENTIFIER);
            token.Value = $"M{index}";
            E.Push(token);
            index++;
        }
        public void Start()
        {
            CheckContains(tokens);
            CheckSyntax();
            Method();
            ListTroykaToStrings(listTroyka);
        }
        private void Method()
        {
            if (lex == tokens.Count)
            {
                if (T.Count == 0)
                {
                    return;
                }
                else
                {
                    EndList();
                }
            }
            else
            {
                switch (GetLexem(lex).getTokenType())
                {
                    case TokenType.IDENTIFIER:
                        K_id();
                        break;
                    case TokenType.NUMBER:
                        K_id();
                        break;
                    case TokenType.OBRACKET:
                        Lpar();
                        break;
                    case TokenType.PLUS:
                        PlusOrMinus();
                        break;
                    case TokenType.MINUS:
                        PlusOrMinus();
                        break;
                    case TokenType.MULTIPLICATION:
                        MultiplicationOrDivision();
                        break;
                    case TokenType.DIVISION:
                        MultiplicationOrDivision();
                        break;
                    case TokenType.CBRACKET:
                        Rpar();
                        break;
                }
            }
            Method();
        }
        private void Rpar()
        {
            if (T.Count == 0)
                D5("лишняя \")\"");
            else
            {
                switch (T.Peek().getTokenType())
                {
                    case TokenType.OBRACKET:
                        D3();
                        break;
                    case TokenType.PLUS:
                        D4();
                        break;
                    case TokenType.MINUS:
                        D4();
                        break;
                    case TokenType.MULTIPLICATION:
                        D4();
                        break;
                    case TokenType.DIVISION:
                        D4();
                        break;
                }
            }
        }
        private void MultiplicationOrDivision()
        {
            if (T.Count == 0)
                D1();
            else
            {
                switch (T.Peek().getTokenType())
                {
                    case TokenType.OBRACKET:
                        D1();
                        break;
                    case TokenType.PLUS:
                        D1();
                        break;
                    case TokenType.MINUS:
                        D1();
                        break;
                    case TokenType.MULTIPLICATION:
                        D2();
                        break;
                    case TokenType.DIVISION:
                        D2();
                        break;
                }
            }
        }
        private void PlusOrMinus()
        {
            if (T.Count == 0)
                D1();
            else
            {
                switch (T.Peek().getTokenType())
                {
                    case TokenType.OBRACKET:
                        D1();
                        break;
                    case TokenType.PLUS:
                        D2();
                        break;
                    case TokenType.MINUS:
                        D2();
                        break;
                    case TokenType.MULTIPLICATION:
                        D4();
                        break;
                    case TokenType.DIVISION:
                        D4();
                        break;
                }
            }
        }
        private void Lpar()
        {
            if (T.Count == 0)
                D1();
            else
            {
                switch (T.Peek().getTokenType())
                {
                    case TokenType.OBRACKET:
                        D1();
                        break;
                    case TokenType.PLUS:
                        D1();
                        break;
                    case TokenType.MINUS:
                        D1();
                        break;
                    case TokenType.MULTIPLICATION:
                        D1();
                        break;
                    case TokenType.DIVISION:
                        D1();
                        break;
                }
            }
        }
        private void EndList()
        {
            if (T.Count == 0)
            {
                return;
            }
            else
            {
                switch (T.Peek().getTokenType())
                {
                    case TokenType.OBRACKET:
                        D5("лишняя \"(\"");
                        break;
                    case TokenType.PLUS:
                        D4();
                        break;
                    case TokenType.MINUS:
                        D4();
                        break;
                    case TokenType.MULTIPLICATION:
                        D4();
                        break;
                    case TokenType.DIVISION:
                        D4();
                        break;
                }
            }
        }
        private void D1()
        {
            T.Push(GetLexem(lex));
            lex++;
        }
        private void D2()
        {
            K_op();
            T.Push(GetLexem(lex));
            lex++;
        }
        private void D3()
        {
            T.Pop();
            lex++;
        }
        private void D4()
        {
            K_op();
        }
        private void D5(string error)
        {
            throw new Exception($"Ошибка в арифметическом выражении: {error}");
        }
        private void CheckSyntax()
        {
            int current = 0;
            int next = 1;

            while (current < tokens.Count - 1)
            {
                if (tokens[current].getTokenType() == TokenType.IDENTIFIER ||
                    tokens[current].getTokenType() == TokenType.NUMBER ||
                    tokens[current].getTokenType() == TokenType.CBRACKET)
                {
                    if (tokens[next].getTokenType() == TokenType.IDENTIFIER ||
                       tokens[next].getTokenType() == TokenType.NUMBER ||
                       tokens[next].getTokenType() == TokenType.OBRACKET)
                        throw new Exception($"Ошибка в арифметическом выражении. Ожидалось: или +, или -, или *, или /, а встретилось: {tokens[next].Value}");
                    else { current++; next++; }
                }
                else { current++; next++; }
            }
        }
        private void CheckContains(List<leks> tokens)
        {
            foreach (leks token in tokens)
            {
                if (token.getTokenType() != TokenType.OBRACKET &&
                    token.getTokenType() != TokenType.CBRACKET &&
                    token.getTokenType() != TokenType.MINUS &&
                    token.getTokenType() != TokenType.PLUS &&
                    token.getTokenType() != TokenType.DIVISION &&
                    token.getTokenType() != TokenType.MULTIPLICATION &&
                    token.getTokenType() != TokenType.IDENTIFIER &&
                    token.getTokenType() != TokenType.NUMBER)
                    throw new Exception($"Недопустимый символ в арифмeтическом выражении: {token.getTokenType()}");
            }
        }
    }
}