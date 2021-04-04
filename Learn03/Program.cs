using System;
using System.Collections.Generic;

namespace Learn03
{
    class Program
    {

        static void Main(string[] args)
        {
            
            
            Stack<char> operatorStack = new Stack<char>();
                        
            while(true)
            {
                List<object> expression = ConvertStrinToListObj();

                Console.WriteLine("try parse HERE");
                


                foreach (object item in expression)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }

        private static List<object> ConvertStrinToListObj()
        {
            List<object> expression = new List<object>();
            Console.WriteLine("Enter expression");
            string userExpression = Console.ReadLine().Replace(" ",string.Empty);            
            double number = 0;
            try
            {
                for (int i = 0; i < userExpression.Length; i++)
                {
                    if (userExpression[i] >= '0' && userExpression[i] <= '9')
                    {
                        number = number * 10 + (userExpression[i] - '0');
                    }
                    else if (userExpression[i] == '+' || userExpression[i] == '-' || userExpression[i] == '/' || userExpression[i] == '*' || userExpression[i] == '(' || userExpression[i] == ')')
                    {
                        expression.Add(number);
                        expression.Add(userExpression[i]);
                        number = 0;
                    }
                    else if (userExpression[i] == '=')
                    {
                        expression.Add(number);
                    }
                }
                /*foreach (char symvol in userExpression)
                {
                    if (symvol >= '0' && symvol <= '9')
                    {                        
                        number = number * 10 + (symvol - '0');
                    }
                    else if (symvol == '+' || symvol == '-' || symvol == '/' || symvol == '*' || symvol == '(' || symvol == ')')
                    {
                        expression.Add(number);
                        expression.Add(symvol);
                        number = 0;
                    }
                    else if (symvol == '=')
                    {
                        expression.Add(number);
                    }
                }*/
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return expression;
        }

        static double MathAction(double firstNumber, double secondNumber, char operationSign)
        {
            switch(operationSign)
            {
                case '+':
                    try
                    {
                        return firstNumber + secondNumber; 
                    }
                    catch(Exception ex)
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
