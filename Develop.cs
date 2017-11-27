using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc
{
    class Program
    {
        static void Main()
        {
            Start:
            Console.WriteLine("Enter expression");
            string expr = Console.ReadLine();
            //getting all the numbers
            string[] split_expr = expr.Split(new Char[] { '/', '*', '+', '-' });
     
            //to verify that no extra operator was entered
            for (int i = 0; i < split_expr.Length; i++)
            {
                if (split_expr[i]=="")
                {
                    Console.WriteLine("expression is not correct (extra operator)");
                    goto Start;
                }
            }
            //verify that no not allowed symbols are entred
            double number;
            foreach (string s in split_expr)
            {
                if (Double.TryParse(s, out number)) continue;
                else Console.WriteLine("expression is not correct (invalide character)");
                goto Start;
            }
            //getting all operators
            char[] ch_expr = expr.ToCharArray();
            List<char> delim = new List<char>();
            int c=0;
            for(int i=0;i<split_expr.Length-1;i++)
            {
                if (i == 0)
                {
                    c += split_expr[i].Length;
                }
                else
                {
                    c += split_expr[i].Length+1;
                }
                delim.Add(ch_expr[c]);
            };

            //creating one list with operands and operators
            List<string> oo = new List<string>();
            for (int i=0; i< split_expr.Length-1;i++)
            {
                oo.Add(split_expr[i]);
                oo.Add(delim[i].ToString());
            }
            int n = split_expr.Length - 1;
            oo.Add(split_expr[n]);

            //Calculate * and /
            for (int i = 0; i < oo.Count; i++)
            {
                if (i % 2 != 0)
                    {
                    if (oo[i] == "*" || oo[i] == "/")
                    {
                        double a = Convert.ToDouble(oo[i - 1]);
                        double b = Convert.ToDouble(oo[i + 1]);
                        oo[i - 1]="delete";
                        if (oo[i] == "*")
                        { 
                            string d = Convert.ToString(a * b);
                            oo[i+1] = d;
                            oo[i] = "delete";
                        }
                        else
                        {
                            string e = Convert.ToString(a / b);
                            oo[i+1] = e;
                            oo[i] = "delete";
                        }
                    }
                    else { continue;}
                }
            }
            for (int i = oo.Count-1; i >= 0; i--)
            {
                if (oo[i] == "delete") oo.RemoveAt(i);
            }

            //Calculate + and -
            for (int i = 0; i < oo.Count; i++)
            {
                if (i % 2 != 0)
                {
                        double a = Convert.ToDouble(oo[i - 1]);
                        double b = Convert.ToDouble(oo[i + 1]);
                        oo[i - 1] = "delete";
                        if (oo[i] == "+")
                        {
                            string d = Convert.ToString(a + b);
                            oo[i + 1] = d;
                            oo[i] = "delete";
                        }
                        else
                        {
                            string e = Convert.ToString(a - b);
                            oo[i + 1] = e;
                            oo[i] = "delete";
                        }
                }
            }
            for (int i = oo.Count - 1; i >= 0; i--)
            {
                if (oo[i] == "delete") oo.RemoveAt(i);
            }
            foreach (string s in oo)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
