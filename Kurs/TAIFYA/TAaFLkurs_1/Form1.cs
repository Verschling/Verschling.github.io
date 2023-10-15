using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TAaFLkurs_1
{
    public partial class Form1 : Form
    {
        //хранит список символов строки
        List<char> textChars = new List<char>();
        List<leks> leksem = new List<leks>();
        bool trigger = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringReader Reader = new StringReader(textBox1.Text);
            under one = new under(textChars);
            string line;
            List<char> bliat = new List<char>();
            textBox3.Text = "";
            for(int i = 0; i < textBox1.Text.Length; i++)
            {
                bliat.Add(textBox1.Text[i]);
            }
                one = new under(bliat);
                one.check(ref leksem);
                textBox3.Text = "";
                textBox2.Text = "";
                for (int i = 0; i < leksem.Count; i++)
                {
                    textBox3.Text += "    " + leksem[i].getLeksem() + " __ " + leksem[i].getTipe() + Environment.NewLine;
                }
                for (int i = 0; i < leksem.Count; i++)
                {
                    if (leksem[i].getTipe() == "I")
                    {
                        if (leks.IsSpecialWord(leksem[i].getLeksem()))
                        {
                            leksem[i] = new leks(leksem[i].getLeksem(), leksem[i].getTipe(), leks.SpecialWords[leksem[i].getLeksem()]);
                            textBox2.Text += $"{leksem[i].type}" + Environment.NewLine;
                            continue;
                        }
                        else
                        {
                            leksem[i] = new leks(leksem[i].getLeksem(), leksem[i].getTipe(), leks.TokenType.IDENTIFIER);
                            textBox2.Text += $"{leksem[i].type} __ {leksem[i].getLeksem()}" + Environment.NewLine;
                            continue;
                        }
                    }
                    else if (leksem[i].getTipe() == "D")
                    {
                        leksem[i] = new leks(leksem[i].getLeksem(), leksem[i].getTipe(), leks.TokenType.NUMBER);
                        textBox2.Text += $"{leksem[i].type} __ {leksem[i].getLeksem()}" + Environment.NewLine;
                        continue;
                    }
                    else if (leksem[i].getTipe() == "R")
                    {
                        leksem[i] = new leks(leksem[i].getLeksem(), leksem[i].getTipe(), leks.SpecialSymbols[leksem[i].getLeksem()]);
                        textBox2.Text += $"{leksem[i].type}" + Environment.NewLine;
                        continue;
                    }
                }
            trigger = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (trigger)
            {
                LLk llk = new LLk(ref leksem);
                int lk = llk.Programm();
                if (lk == 0)
                {
                    textBox4.Text = $"Ошибок не обнаружено";
                    try
                    {
                        LLk a = new LLk(ref leksem);
                        a.braier();
                        textBox4.Text += ", включая арифметические";
                    }
                    catch (Exception E) { MessageBox.Show(E.Message, "Результат проверки"); }
                }
                else
                {
                    try
                    {
                        textBox4.Text = $"Ошибка: получено {leksem[llk.getTokenNumber()].getTokenType()}, {llk.getTokenNumber()}";
                    }
                    catch
                    {
                        textBox4.Text = $"Ошибка: ожидалось: DOT, {llk.getTokenNumber()}";
                    }
                }
            }
            else
            {
                textBox4.Text = "Можно заварить чай";
            }
        }
    }
}
