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
            Console.WriteLine(MakePostfixNotation("a+b-c"));
            Console.WriteLine(MakePostfixNotation("89+45^12"));
            Console.WriteLine(MakePostfixNotation("alpha+gamma^omega+12*16"));
            Console.ReadKey();
        }
        static string MakePostfixNotation(string function)
        {
            string postfixNotation = "";
            string[] operations = new string[7] { "(", ")", "+", "-", "*", "/", "^" };
            Stack<string> operationStack= new Stack<string>();
            for (int i = 0; i < function.Length; i++)
            {
                if (operations.Contains(function[i].ToString()))
                {
                    postfixNotation = OperationProcedure(function, postfixNotation, operationStack,operations, i);
                }
                else
                {
                    NumberCharProcedure(function, ref postfixNotation, operations, ref i);
                }
            }

            int length = operationStack.Count;
            for (int i = 0; i < length; i++)
            {
                postfixNotation = postfixNotation + operationStack.Pop()+" ";
            }

            return postfixNotation;
        }
        private static void NumberCharProcedure(string function, ref string postfixNotation, string[] operations, ref int i)
        {
            if (0 <= (int)function[i] && (int)function[i] <= 9)
            {
                while (i < function.Length && 0 <= (int)function[i] && (int)function[i] <= 9)
                {
                    postfixNotation = postfixNotation + function[i];
                    ++i;
                }
                postfixNotation = postfixNotation + " ";
                i--;
            }
            else
            {
                while (i < function.Length && !operations.Contains(function[i].ToString()))
                {
                    postfixNotation = postfixNotation + function[i];
                    ++i;
                }
                postfixNotation = postfixNotation + " ";
                i--;
            }
        }
        private static string OperationProcedure(string function, string postfix_notation, Stack<string> operationStack, string[] operations,int i)
        {
            if (function[i] == '(')
            {
                operationStack.Push(function[i].ToString());
            }
            else if (function[i] == ')')
            {

                while (operationStack.Peek() != "(")
                {
                    postfix_notation = postfix_notation + operationStack.Pop() + " ";
                }
                operationStack.Pop();
            }
            else
            {
                if (operationStack.Count != 0)
                {
                    while ((operationStack.Count != 0) && (GetPriority(operationStack.Peek(), operations) >= GetPriority(function[i].ToString(), operations)))
                    {
                        postfix_notation = postfix_notation + operationStack.Pop() + " ";
                    }
                }
                operationStack.Push(function[i].ToString());
            }
            return postfix_notation;
        }
        private static int GetPriority(string operation, string[] operations)
        {
            int i = 0;
            for (i = 0; i < operations.Length; i++)
            {
                if(operations[i]==operation)
                {
                    break;
                }
            }
            return i/2;
        }
    }
}
