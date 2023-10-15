using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAaFLkurs_1
{
    internal class under
    {
        //сюда приходит список символов строки
        List<char> chars = new List<char>();
        //здесь храняться лексемы
        List<leks> leksem = new List<leks>();

        bool fa = false;
        string word;

        public under(List<char> list)
        {
            chars = list;
        }

        internal int check(ref List<leks> leks)
        {
            Console.WriteLine("start{0}", chars.Count);
            int b = -1;
            for(int i = 0; i < chars.Count; i++)
            {
                //Console.WriteLine("startCheck");
                if (chars[i] != ' ' && chars[i] != '\n')
                {
                    if (Regex.IsMatch(Convert.ToString(chars[i]), @"^[0-9]+$"))
                    {
                        if(b == -1 || b == 3)
                        {
                            b = 1;
                        }
                        word = word + chars[i];
                    }
                    else
                    if (Regex.IsMatch(Convert.ToString(chars[i]), @"^[a-zA-Zа-яА-Я]+$"))
                    {
                        if(b == 1)
                        {
                            return -1;
                        }
                        if (b == -1 || b == 2 || b == 3)
                        {
                            word = word + chars[i];
                            b = 2;
                        }
                    }
                    else
                    if (Regex.IsMatch(Convert.ToString(chars[i]), @"^[,;:.='()+\-/*]+$"))
                    {
                        if (b == 1 || b == 2)
                        {
                            add(b);
                            //Console.WriteLine("Yes-3(1-2)");
                            b = 3;
                        }
                        word = word + chars[i];
                        if (!(chars[i] == ':' && chars[i + 1] == '='))
                        {
                            add(3);
                            //Console.WriteLine("Yes-3");
                        }
                        else if (chars[i] == '=')
                        {
                            add(3);
                            //Console.WriteLine("Yes-3");
                        }
                    }
                }
                else
                {
                    if(b == 1)
                    {
                        add(1);
                        //Console.WriteLine("Yes-1");
                    }
                    if(b == 2)
                    {
                        add(2);
                        //Console.WriteLine("Yes-2");
                    }
                    b = -1;
                }
                if(i == chars.Count)
                {

                }
            }

            leks = leksem;
            return 1;
        }
        private void add(int x)
        {
            if (x == 1)
            {
                leksem.Add(new leks(word, "D"));
                word = null;
                //Console.WriteLine("Yes-1-Yes");
            }
            if (x == 2)
            {
                leksem.Add(new leks(word, "I"));
                word = null;
                //Console.WriteLine("Yes-2-Yes");
            }
            if (x == 3)
            {
                leksem.Add(new leks(word, "R"));
                word = null;
                //Console.WriteLine("Yes-3-Yes");
            }
        }
    }
}
