using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostfixNotation
{
    class PostfixNotation
    {
        private string postfixNotation = "";
        private string[] operations = new string[7] { "(", ")", "+", "-", "*", "/", "^" };
        private Stack<string> operationStack;

        public PostfixNotation() { }

        public string MakePostfixNotation(string function)
        {
            postfixNotation = "";
            operationStack = new Stack<string>();
            for (int i = 0; i < function.Length; i++)
            {
                if (operations.Contains(function[i].ToString()))
                {
                    postfixNotation = OperationProcess(function,i);
                }
                else
                {
                    NumberCharProcess(function, ref i);
                }
            }

            int length = operationStack.Count;
            for (int i = 0; i < length; i++)
            {
                postfixNotation = postfixNotation + operationStack.Pop() + " ";
            }

            return postfixNotation;

        }

        private string OperationProcess(string function, int i)
        {
            if (function[i] == '(')
            {
                operationStack.Push(function[i].ToString());
            }
            else if (function[i] == ')')
            {

                while (operationStack.Peek() != "(")
                {
                    postfixNotation = postfixNotation + operationStack.Pop() + " ";
                }
                operationStack.Pop();
            }
            else
            {
                if (operationStack.Count != 0)
                {
                    while ((operationStack.Count != 0) && (GetPriority(operationStack.Peek()) >= GetPriority(function[i].ToString())))
                    {
                        postfixNotation = postfixNotation + operationStack.Pop() + " ";
                    }
                }
                operationStack.Push(function[i].ToString());
            }
            return postfixNotation;
        }

        private void NumberCharProcess(string function, ref int i)
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

        private int GetPriority(string operation)
        {
            int i = 0;
            for (i = 0; i < operations.Length; i++)
            {
                if (operations[i] == operation)

                    break;

            }
            return i / 2;
        }
    }
}
