using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostfixNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MakePostfixNotation("a+(b-c)*d"));
            Console.WriteLine(MakePostfixNotation("3+4*2/(1-5)^2"));
            Console.ReadKey();
        }
        static string MakePostfixNotation(string function)
        {
            string postfix_notation = "";
            string []operation=new string[7]{"+","-","*","(",")","/","^"};
            Stack<string> operation_stack= new Stack<string>();
            for (int i = 0; i < function.Length; i++)
            {
                if (operation.Contains(function[i] + ""))
                {
                    if (function[i]+"" == "(")
                    {
                        operation_stack.Push(function[i]+"");
                    }
                    else if (function[i] + "" == ")")
                    {
                       
                        while (operation_stack.Peek() != "(")
                        {
                            postfix_notation = postfix_notation + operation_stack.Pop() + " ";
                        }
                        operation_stack.Pop();
                    }
                    else 
                    {
                        if (operation_stack.Count != 0)
                        {
                            while (GetPriority(operation_stack.Peek()) >= GetPriority(function[i] + ""))
                            {
                                postfix_notation = postfix_notation + operation_stack.Pop()+" ";
                            }
                        }
                        operation_stack.Push(function[i] + "");
                    }
                }
                else
                {
                    if ('0' <= function[i] && function[i] <= '9')
                    {
                        while (i < function.Length && '0' <= function[i] && function[i] <= '9')
                        {
                            postfix_notation = postfix_notation + function[i];
                            ++i;
                        }
                        postfix_notation = postfix_notation + " ";
                        i--;
                    }
                    else
                    {
                        postfix_notation = postfix_notation + function[i] + " ";
                    }
                }
            }

            int length = operation_stack.Count;
            for (int i = 0; i < length; i++)
            {
                postfix_notation = postfix_notation + operation_stack.Pop()+" ";
            }

            return postfix_notation;
        }
        static int GetPriority(string operation)
        {
            switch (operation)
            {
                case "^":
                    return 3;
                case "+":
                    return 1;
                case "-":
                    return 1;
                case "*":
                    return 2;
                case "/":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
