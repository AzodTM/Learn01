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

                //Пытаемся конвертировать введенные пользователем данные в осмысленый пример
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
            bool addNumber = false; //проверка был ли добавлен цифровой символ в итерации цикла
            bool addOperator = false; //проверка был ли добавлен последним символом оператор (не считая скобок)
            bool addDot = false; //проверка была ли установлена точка в числе которое сейчас считывается
            bool isNegativeNumber = false;
            int countDecimalCounter = -1;
            int countRightBreckets = 0;
            int countLeftBreckets = 0;
            int countEqualitySign = 0;

            for (int i = 0; i < userExpression.Length; i++)
            {
                

                if (userExpression[i] >= '0' && userExpression[i] <= '9')
                {
                    if (addDot)
                    {                       
                        number += ((userExpression[i] - '0') * Math.Pow(10, countDecimalCounter--));
                    }
                    else
                    {
                        number = number * 10 + (userExpression[i] - '0');
                    }
                    addOperator = false;
                    addNumber = true;
                }
                else if(userExpression[i] == '.')
                {
                    if (addDot)
                    {
                        throw new Exception("Double dot in number");
                    }
                    else if (addNumber)
                    {
                        if(number % 1 < 1)
                        {
                            addDot = true;
                        }
                    }
                    else 
                    {
                        throw new Exception("incorrect location of dots");
                    }
                }

                else if (userExpression[i] == '+' || userExpression[i] == '-' || userExpression[i] == '/' || userExpression[i] == '\\'
                    || userExpression[i] == '*' ||  userExpression[i] == '^')
                {
                    if (userExpression[i] == '-')
                    {
                        if (i == 0)
                        {
                            isNegativeNumber = true;
                            continue;
                        }
                        else if (userExpression[i - 1] < '0' || userExpression[i - 1] > '9')
                        {
                            isNegativeNumber = true;
                            continue;

                        }
                        
                    }
                    else if (i == 0)
                    {
                        throw new Exception("Operefor first sign");
                    }
                    else if (addOperator)
                    {
                        throw new Exception("two operators in a row");
                    }
                    else if (addNumber)
                    {
                        if (isNegativeNumber)
                        {
                            number *= (-1);
                            isNegativeNumber = false;
                        }
                        expression.Add(number);
                        addOperator = true;
                        addNumber = false;
                    }
                    expression.Add(userExpression[i]);
                    number = 0;
                    countDecimalCounter = -1;
                    addDot = false;

                }
                else if(userExpression[i] == '(' || userExpression[i] == ')')
                {
                    if (userExpression[i] == '(')
                    {
                        countLeftBreckets++;
                    }
                    else if (userExpression[i] == ')')
                    {
                        countRightBreckets++;
                        if (countRightBreckets > countLeftBreckets)
                        {
                            throw new Exception("Incorrect brackets");
                        }
                    }
                    if (addNumber)
                    {
                        if (isNegativeNumber)
                        {
                            number *= (-1);
                            isNegativeNumber = false;
                        }
                        expression.Add(number);
                        addNumber = false;
                    }
                    expression.Add(userExpression[i]);
                    number = 0;
                    countDecimalCounter = -1;
                    addDot = false;
                }
                else if (userExpression[i] == '=')
                {
                    if (addOperator)
                    {
                        throw new Exception("Error in operator berore Eqality sign");
                    }                    
                    countEqualitySign++;
                    if (addNumber)
                    {
                        if (isNegativeNumber)
                        {
                            number *= (-1);
                            isNegativeNumber = false;
                        }
                        expression.Add(number);
                    }
                }
                else
                {
                    throw new Exception("Invalid signs in expression");
                }
            }

            if (countLeftBreckets != countRightBreckets)
            {
                throw new Exception("Wrong number of brackets");
            }
            else if (countEqualitySign != 1)
            {
                throw new Exception("Wrong number of Eqality sign");
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
