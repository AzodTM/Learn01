using System;
using System.Collections.Generic;

namespace Learn03
{
    class Program
    {

        static void Main(string[] args)
        {


            Stack<char> operatorStack = new Stack<char>();

            while (true)
            {
                try
                {
                    List<object> expression = ConvertStrinToListObj();
                    Console.WriteLine("Show result");
                    foreach (object item in expression)
                    {
                        Console.Write(item + " ");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }






                Console.WriteLine();
            }
        }

        private static List<object> ConvertStrinToListObj()
        {
            List<object> expression = new List<object>();
            Console.WriteLine("Enter expression");
            string userExpression = Console.ReadLine().Replace(" ", string.Empty);
            double number = 0;
            int countRightBreckets = 0;
            int countLeftBreckets = 0;
            int countEqualitySign = 0;

            for (int i = 0; i < userExpression.Length; i++)
            {
                if (userExpression[i] >= '0' && userExpression[i] <= '9')
                {
                    number = number * 10 + (userExpression[i] - '0');
                }
                else if (userExpression[i] == '+' || userExpression[i] == '-' || userExpression[i] == '/'
                    || userExpression[i] == '*' || userExpression[i] == '(' || userExpression[i] == ')' || userExpression[i] == '^')
                {
                    if (userExpression[i] == '(')
                    {
                        countLeftBreckets++;
                    }
                    else if (userExpression[i] == ')')
                    {
                        countRightBreckets++;
                    }
                    expression.Add(number);
                    expression.Add(userExpression[i]);
                    number = 0;
                }
                else if (userExpression[i] == '=')
                {
                    countEqualitySign++;
                    expression.Add(number);
                }
                else
                {
                    throw new Exception("Bad signs");
                }
            }

            if (countLeftBreckets != countRightBreckets)
            {
                throw new Exception("Wrong number of brackets");
            }
            else if (countEqualitySign != 1)
            {
                throw new Exception("Wrong number of Eqality sing");
            }

            return expression;
        }

        static double MathAction(double firstNumber, double secondNumber, char operationSign)
        {
            switch (operationSign)
            {
                case '+':
                    try
                    {
                        return firstNumber + secondNumber;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.Write(ex);
                    }
                    break;

                case '-':
                    try
                    {
                        return firstNumber - secondNumber;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.Write(ex);
                    }
                    break;
                case '*':
                    try
                    {
                        return firstNumber * secondNumber;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.Write(ex);
                    }
                    break;
                case '/':
                    try
                    {
                        return firstNumber / secondNumber;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.Write(ex);
                    }
                    break;
                case '^':
                    try
                    {
                        return Math.Pow(firstNumber, secondNumber);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.Write(ex);
                    }
                    break;
            }
            return 0;
        }
    }
}
