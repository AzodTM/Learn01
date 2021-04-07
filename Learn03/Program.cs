using System;
using System.Collections.Generic;

namespace Learn03
{
    class Program
    {

        static void Main(string[] args)
        {
            List<object> expression = new List<object>();
            List<object> reversePolishNotation = new List<object>();
            while (true)
            {

                //Пытаемся конвертировать введенные пользователем данные в осмысленый пример
                try
                {
                    ///ввод примера пользовалетем и проверка на ощибки
                    Console.WriteLine("Enter expression");
                    expression = ConvertStrinToListObj(Console.ReadLine());


                    ///отрисовка примера в инфиксной нотации
                    Console.WriteLine("\nYour expression:");
                    foreach (object item in expression)
                    {
                        if (item.GetType() == typeof(char))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(item + " ");
                            Console.ResetColor();
                            
                        }
                        else
                        {
                            Console.Write(item + " ");
                        }
                    }

                    reversePolishNotation = ConvertInfixNotationToReversePolishNotation(expression);

                    //отрисовка примера в обратной польской нотации
                    Console.WriteLine("\nRevers poland notation:");
                    foreach (object item in reversePolishNotation)
                    {
                        Console.Write(item + " ");
                    }

                    //решение примера 
                    Console.WriteLine("\nAnswer:");
                    Console.WriteLine(SolutionOfExampleInReversPolishNotation(reversePolishNotation));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine(ex);
                    Console.ResetColor();
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
            bool addedSpase = false; //Проверка был ли добавлен пробел
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
                    if (i > 1)
                    {
                        if(addedSpase && addedNumber)
                        {
                            throw new Exception("Space between numbers");
                        }
                    }
                    addedOperator = false;
                    addedNumber = true;
                }
                else if(userExpression[i] == ' ')
                {
                    if (addedSpase)
                    {
                        throw new Exception("Double spase");
                    }
                    else if(i == 0)
                    {
                        throw new Exception("Space is first sign");
                    }
                    
                    addedSpase = true;

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
                    addedSpase = false;

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
                    addedSpase = false;

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
        private static List<object> ConvertInfixNotationToReversePolishNotation(List<object> infixNotation)
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
                        case '^':
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
        /// 
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

        /// <summary>
        /// Решение примера в обратной польской нотации
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static double SolutionOfExampleInReversPolishNotation(List<object> expression)
        {
            while (expression.Count != 1)
            {
                for (int i = 0; i < expression.Count; i++)
                {
                    if (expression[i].GetType() == typeof(char))
                    {
                        MathAction(ref expression, i);
                        break;
                    }
                }
            }

            return (double)expression[0];
        }

        /// <summary>
        /// Математическое дейтсвие над двумя числами в Обратной Польской Нотации
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="manipulator"></param>
        private static void MathAction(ref List<object> expression, int manipulator)
        {
            
            switch((char)expression[manipulator])
            {
                case '+':
                    expression[manipulator - 2] = (double)expression[manipulator-2] + (double)expression[manipulator - 1];
                    expression.RemoveAt(manipulator - 1);
                    expression.RemoveAt(manipulator - 1);
                    break;
                case '-':
                    expression[manipulator - 2] = (double)expression[manipulator - 2] - (double)expression[manipulator - 1];
                    expression.RemoveAt(manipulator - 1);
                    expression.RemoveAt(manipulator - 1);
                    break;
                case '*':
                    expression[manipulator - 2] = (double)expression[manipulator - 2] * (double)expression[manipulator - 1];
                    expression.RemoveAt(manipulator - 1);
                    expression.RemoveAt(manipulator - 1);
                    break;
                case '/':
                case '\\':
                    expression[manipulator - 2] = (double)expression[manipulator - 2] / (double)expression[manipulator - 1];
                    expression.RemoveAt(manipulator - 1);
                    expression.RemoveAt(manipulator - 1);
                    break;
                case '^':
                    expression[manipulator - 2] = Math.Pow((double)expression[manipulator - 2],(double)expression[manipulator - 1]);
                    expression.RemoveAt(manipulator - 1);
                    expression.RemoveAt(manipulator - 1);
                    break;

            }
        }

    }
}
