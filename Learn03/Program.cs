using System;
using System.Collections.Generic;

namespace Learn03
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {

                //Пытаемся конвертировать введенные пользователем данные в осмысленый пример
                try
                {
                    ///ввод примера пользовалетем и проверка на ощибки
                    Console.WriteLine("Enter expression");
                    List<object> expression = ConvertStrinToListObj(Console.ReadLine().Replace(" ", string.Empty));

                    /*
                    ///отрисовка примера
                    Console.WriteLine("Show result");
                    foreach (object item in expression)
                    {
                        Console.WriteLine(item + " = " + item.GetType());
                        Console.WriteLine(item.GetType() == typeof(char));
                    }
                    */

                    Console.WriteLine("Revers poland notation");
                    foreach (object item in ReversePolishNotation(expression))
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
        /// <summary>
        /// Этот монстр обрабатывает данные введенные пользователем и возвращает осмысленный пример
        /// Каждое число (положительно или отрицательное) знак действия и скобка - это отдельный объект
        /// </summary>
        /// <returns></returns>
        private static List<object> ConvertStrinToListObj(string userExpression)
        {
            List<object> expression = new List<object>();

            double number = 0;
            bool addedNumber = false; //проверка был ли добавлен цифровой символ в итерации цикла
            bool addedOperator = false; //проверка был ли добавлен последним символом оператор (не считая скобок)
            bool addedDot = false; //проверка была ли установлена точка в числе которое сейчас считывается
            bool isNegativeNumber = false;
            int countDecimalCounter = -1;
            int countRightBreckets = 0;
            int countLeftBreckets = 0;
            int countEqualitySign = 0;

            for (int i = 0; i < userExpression.Length; i++)
            {


                if (userExpression[i] >= '0' && userExpression[i] <= '9')
                {
                    try
                    {
                        if (userExpression[i - 1] == ')')
                        {
                            expression.Add('*');
                        }
                    }
                    catch { };
                    if (addedDot)
                    {
                        number += ((userExpression[i] - '0') * Math.Pow(10, countDecimalCounter--));
                    }
                    else
                    {
                        number = number * 10 + (userExpression[i] - '0');
                    }
                    addedOperator = false;
                    addedNumber = true;
                }
                else if (userExpression[i] == '.')
                {
                    if (addedDot)
                    {
                        throw new Exception("Double dot in number");
                    }
                    else if (addedNumber)
                    {
                        if (number % 1 < 1)
                        {
                            addedDot = true;
                        }
                    }
                    else
                    {
                        throw new Exception("incorrect location of dots");
                    }
                }

                else if (userExpression[i] == '+' || userExpression[i] == '-' || userExpression[i] == '/' || userExpression[i] == '\\'
                    || userExpression[i] == '*' || userExpression[i] == '^')
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
                            if (isNegativeNumber)
                            {
                                throw new Exception("So more minus");
                            }
                            else if (addedOperator)
                            {
                                throw new Exception("So more minus");
                            }
                            isNegativeNumber = true;
                            continue;
                        }

                    }
                    else if (i == 0)
                    {
                        throw new Exception("Operefor first sign");
                    }

                    if (addedOperator)
                    {
                        throw new Exception("two operators in a row");
                    }
                    else if (addedNumber)
                    {
                        if (isNegativeNumber)
                        {
                            number *= (-1);
                            isNegativeNumber = false;
                        }
                        expression.Add(number);
                        addedOperator = true;
                        addedNumber = false;
                    }
                    expression.Add(userExpression[i]);
                    number = 0;
                    addedOperator = true;
                    addedNumber = false;
                    countDecimalCounter = -1;
                    addedDot = false;

                }
                else if (userExpression[i] == '(' || userExpression[i] == ')')
                {
                    if (userExpression[i] == '(')
                    {
                        countLeftBreckets++;
                        try
                        {
                            if (userExpression[i - 1] == ')')
                            {
                                expression.Add('*');
                            }
                        }
                        catch { };

                    }
                    else if (userExpression[i] == ')')
                    {
                        countRightBreckets++;
                        if (countRightBreckets > countLeftBreckets)
                        {
                            throw new Exception("Incorrect brackets");
                        }
                        else if (addedOperator)
                        {
                            throw new Exception("Incorrect operator before close bracket");
                        }
                    }
                    if (addedNumber)
                    {
                        if (isNegativeNumber)
                        {
                            number *= (-1);
                            isNegativeNumber = false;
                        }
                        expression.Add(number);
                        if (userExpression[i] == '(')
                        {
                            expression.Add('*');
                        }
                        addedNumber = false;
                    }

                    expression.Add(userExpression[i]);
                    number = 0;
                    countDecimalCounter = -1;
                    addedDot = false;
                    addedOperator = false;
                }
                else if (userExpression[i] == '=')
                {
                    if (i != userExpression.Length - 1)
                    {
                        throw new Exception("Equality sign is not last");
                    }
                    else if (addedOperator)
                    {
                        throw new Exception("Error in operator berore Equlity sign");
                    }
                    countEqualitySign++;
                    if (addedNumber)
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


        /// <summary>
        /// Конвертер инфексной нотации в обратную польскую нотацию
        /// </summary>
        /// <param name="infixNotation"></param>
        /// <returns></returns>
        private static List<object> ReversePolishNotation(List<object> infixNotation)
        {
            List<object> reversePolishNotation = new List<object>();
            Stack<object> stackConvert = new Stack<object>();


            foreach (object item in infixNotation)
            {
                if (item.GetType() == typeof(double))
                {
                    reversePolishNotation.Add(item);
                }
                else
                {
                    switch (item)
                    {
                        case '(':
                            stackConvert.Push(item);
                            break;
                        case ')':
                            while ((char)stackConvert.Peek() != '(')
                            {
                                reversePolishNotation.Add(stackConvert.Pop());
                            }
                            stackConvert.Pop();
                            break;
                        case '+':
                        case '-':
                        case '*':
                        case '\\':
                        case '/':
                            if (stackConvert.Count == 0)
                            {
                                stackConvert.Push(item);
                            }
                            else
                            {
                                switch (GetPriority((char)item))
                                {
                                    case 1:

                                        while (stackConvert.Count != 0 && GetPriority((char)stackConvert.Peek()) >= 1)
                                        {
                                            reversePolishNotation.Add(stackConvert.Pop());
                                        }

                                        stackConvert.Push(item);
                                        break;
                                    case 2:
                                        while (stackConvert.Count != 0 && GetPriority((char)stackConvert.Peek()) >= 2)
                                        {
                                            reversePolishNotation.Add(stackConvert.Pop());
                                        }
                                        stackConvert.Push(item);
                                        break;
                                    case 3:
                                        while (stackConvert.Count != 0 && GetPriority((char)stackConvert.Peek()) >= 3)
                                        {
                                            reversePolishNotation.Add(stackConvert.Pop());
                                        }
                                        stackConvert.Push(item);
                                        break;
                                }
                            }
                            break;
                        
                    }
                }

                

            }
            while (stackConvert.Count > 0)
            {
                reversePolishNotation.Add(stackConvert.Pop());
            }
            return reversePolishNotation;
        }

        /// <summary>
        /// Назначение приоритетов операторам (чем выше тем приоритетней)
        /// </summary>
        /// <param name="operatorExpression"></param>
        /// <returns></returns>
        private static int GetPriority(char operatorExpression)
        {
            switch (operatorExpression)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '\\':
                case '/':
                    return 2;
                case '^':
                    return 3;
            }
            return 0;
        }

    }
}
