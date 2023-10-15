using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TAaFLkurs_1
{
    internal class LLk
    {
        List<leks> leksem = new List<leks>();
        int tokenNumber = 0;
        bool triger = false;
        braur_zemelzon b;
        public List<Troyka> Troyka = new List<Troyka>();
        leks.TokenType token;
        public LLk(ref List<leks> leksem)
        {
            this.leksem = leksem;
            tokenNumber = 0;
            triger = true;
        }
        internal leks.TokenType getToken()
        {
            return token;
        }
        public int getTokenNumber()
        {
            return tokenNumber;
        }
        internal void braier()
        {

            tokenNumber = 0;
            while (tokenNumber < leksem.Count)
            {
                List<leks> stackeke = new List<leks>();
                if (leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
                {
                    tokenNumber++;
                    if (leksem[tokenNumber].getTokenType() == leks.TokenType.EQUALIZATION)
                    {
                        tokenNumber++;
                        while (leksem[tokenNumber].getTokenType() != leks.TokenType.SEMICOLON)
                        {
                            stackeke.Add(leksem[tokenNumber]);
                            tokenNumber++;
                        }
                        if (stackeke.Count == 0)
                        {
                            throw new Exception("Ожидалась операция");
                        }
                        if (b == null)
                            b = new braur_zemelzon(stackeke);
                        else
                        {
                            int ii = b.index;
                            b = new braur_zemelzon(stackeke);
                            b.index = ii;
                        }
                        b.listTroyka = Troyka;
                        b.Start();
                    }
                }
                tokenNumber++;
            }
        }
        internal int Programm()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.VAR)
            {
                tokenNumber++;
                if (perem() == false)
                {
                    return -1;
                }
                tokenNumber++;
                if (list_perem() == false)
                {
                    return -1;
                }
                tokenNumber++;
                if (list_oper() == false)
                {
                    return -1;
                }
            }
            else
            {
                token = leks.TokenType.VAR;
                return -1;
            }
            return 0;
        }
        private bool perem()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.NUMBER)
            {
                return true;
            }
            else
            {
                token = leks.TokenType.IDENTIFIER;
                return false;
            }
        }
        private bool type()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.INTEGER)
            {
                return true;
            }
            else
            {
                token = leks.TokenType.INTEGER;
                return false;
            }
        }
        private bool list_perem()
        {
            return list_perem2();
        }
        private bool list_perem2()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.COMMA)
            {
                tokenNumber++;
                return list_perem3();
            }
            else if (leksem[tokenNumber].getTokenType() == leks.TokenType.COLON)
            {
                tokenNumber++;
                return type_and_perem();
            }
            token = leks.TokenType.COLON;
            return false;
        }
        private bool list_perem3()
        {
            if (perem() == false)
            {
                return false;
            }
            tokenNumber++;
            return list_perem2();
        }
        private bool type_and_perem()
        {
            if (type() == false)
            {
                return false;
            }
            tokenNumber++;
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
            {
                tokenNumber++;
                return continu();
            }
            token = leks.TokenType.SEMICOLON;
            return false;
        }
        private bool continu()
        {
            if (perem())
            {
                tokenNumber++;
                return list_perem();
            }
            else
            {
                if (leksem[tokenNumber].getTokenType() == leks.TokenType.BEGIN)
                {
                    return true;
                }
            }
            token = leks.TokenType.BEGIN;
            return false;
        }
        private bool list_oper()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.END)
            {
                return list_oper2();
            }
            return false;
        }
        private bool list_oper2()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                return list_oper3();
            }
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.END)
            {
                tokenNumber++;
                if (leksem[tokenNumber].getTokenType() == leks.TokenType.DOT)
                {
                    return true;
                }
            }
            return false;
        }
        private bool list_oper3()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                if (oper())
                {
                    return list_oper();
                }
            }
            return false;
        }
        private bool oper()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE)
            {
                return inter();
            }
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                return prisv();
            }
            return false;
        }
        private bool prisv()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                if (perem())
                {
                    tokenNumber++;
                    if (leksem[tokenNumber].getTokenType() == leks.TokenType.EQUALIZATION)
                    {
                        tokenNumber++;
                        if (perem())
                        {
                            tokenNumber++;
                            if (list_operand())
                            {
                                if (leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
            }
            return false;
        }
        private bool list_operand()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.PLUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MINUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.ARTICLE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.DIVISION ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MULTIPLICATION ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
            {
                return list_operand2();
            }
            return false;
        }
        private bool list_operand2()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.PLUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MINUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.ARTICLE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.DIVISION ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MULTIPLICATION)
            {
                if (znak())
                {
                    tokenNumber++;
                    if (operand())
                    {
                        tokenNumber++;
                        if (list_operand3())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
            {
                return true;
            }
            return false;
        }
        private bool znak()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.PLUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MINUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.ARTICLE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MULTIPLICATION ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.DIVISION)
            {
                return true;
            }
            return false;
        }
        private bool operand()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.NUMBER)
            {
                return true;
            }
            return false;
        }
        private bool list_operand3()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.PLUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.MINUS ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.ARTICLE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.DIVISION ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
            {
                return list_operand2();
            }
            return false;
        }
        private bool inter()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE)
            {
                tokenNumber++;
                if (perem())
                {
                    tokenNumber++;
                    if (leksem[tokenNumber].getTokenType() == leks.TokenType.OF)
                    {
                        tokenNumber++;
                        if (oper_inter())
                        {
                            if (els())
                            {
                                tokenNumber++;
                                if (leksem[tokenNumber].getTokenType() == leks.TokenType.END)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool oper_inter()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.NUMBER)
            {
                if (list_inter())
                {
                    tokenNumber++;
                    if (start_prisv())
                    {

                        if (leksem[tokenNumber].getTokenType() == leks.TokenType.ELSE)
                        {
                            return true;
                        }
                        tokenNumber++;
                        if (leksem[tokenNumber].getTokenType() == leks.TokenType.NUMBER)
                        {
                            return oper_inter();
                        }
                    }
                }
            }
            return false;
        }
        private bool list_inter()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.NUMBER)
            {
                if (num())
                {
                    tokenNumber++;
                    if (inter2())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool inter2()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.DOT)
            {
                tokenNumber++;
                if (leksem[tokenNumber].getTokenType() == leks.TokenType.DOT)
                {
                    tokenNumber++;
                    if (num())
                    {
                        tokenNumber++;
                        if (leksem[tokenNumber].getTokenType() == leks.TokenType.COLON)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (leksem[tokenNumber].getTokenType() == leks.TokenType.COLON)
            {
                return true;
            }
            return false;
        }
        private bool num()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.NUMBER)
            {
                return true;
            }
            return false;
        }
        private bool start_prisv()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                return oper();
            }
            else if (leksem[tokenNumber].getTokenType() == leks.TokenType.BEGIN)
            {
                tokenNumber++;
                if (seclist_oper())
                {
                    tokenNumber++;
                    if(leksem[tokenNumber].getTokenType() == leks.TokenType.ELSE)
                    {
                        return true;
                    }
                    if (leksem[tokenNumber].getTokenType() == leks.TokenType.END)
                    {
                        if (leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool seclist_oper()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                return seclist_oper2();
            }
            else if (leksem[tokenNumber].getTokenType() == leks.TokenType.END)
            {
                tokenNumber++;
                if (leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
                {
                    return seclist_oper2();
                }
            }
            return false;
        }

        private bool seclist_oper2()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
                leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                return seclist_oper3();
            }
            else if (leksem[tokenNumber].getTokenType() == leks.TokenType.END)
            {
                tokenNumber++;
                if (leksem[tokenNumber].getTokenType() == leks.TokenType.SEMICOLON)
                {
                    return true;
                }
            }
            return false;
        }
        private bool seclist_oper3()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.CASE ||
               leksem[tokenNumber].getTokenType() == leks.TokenType.IDENTIFIER)
            {
                if (oper())
                {
                    tokenNumber++;
                    if (seclist_oper2())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool els()
        {
            if (leksem[tokenNumber].getTokenType() == leks.TokenType.ELSE)
            {
                tokenNumber++;
                return seclist_oper2();
            }
            return false;
        }
    }
}