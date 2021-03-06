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
            bool v2 = true;


            while(v2)
            try
            {
                Console.WriteLine("enter exp");
                expression = Analisis.ParseSyntacticAnalisis(Console.ReadLine());
                expression = Analisis.SemanticAnalisis(expression);


                    Console.WriteLine("\n\nInfix Notation");
                PrintConsole.ListObj(expression);

                    
                    reversePolishNotation = ConvertInfixNotationToReversePolishNotation(expression);

                    Console.WriteLine("\n\nReverse Polish Notation");
                    PrintConsole.ListObj(reversePolishNotation);


                    //решение примера 
                    Console.WriteLine("\n\nAnswer:");
                    Console.WriteLine(SolutionOfExampleInReversPolishNotation(reversePolishNotation));





                }
            catch(Exception ex)
            {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine(ex);
                    Console.ResetColor();
                }
           
        }

        


       

        /// <summary>
        /// Этот монстр обрабатывает данные введенные пользователем и возвращает осмысленный пример
        /// Каждое число (положительно или отрицательное) знак действия и скобка - это отдельный объект
        /// </summary>
        /// <returns></returns>
        

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
                else if (item.GetType() == typeof(char)) 
                {
                    switch (item)
                    {
                        case '(':
                            stackConvert.Push(item);
                            break;
                        case ')':
                            bool inStackPeekLeftBracket = false;
                            while (!inStackPeekLeftBracket)
                            {
                                
                                if (stackConvert.Peek().GetType() == typeof(char))
                                {
                                    if ((char)stackConvert.Peek() == '(')
                                    {
                                        inStackPeekLeftBracket = true;
                                        stackConvert.Pop();
                                    }
                                    
                                }
                                if (stackConvert.Count > 0)
                                {
                                    reversePolishNotation.Add(stackConvert.Pop());
                                }
                            }

                            
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
                            else if(stackConvert.Peek().GetType() == typeof(string))                                
                            {
                                while (stackConvert.Count != 0 && stackConvert.Peek().GetType() == typeof(string))
                                {
                                    reversePolishNotation.Add(stackConvert.Pop());
                                }
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
                else if (item.GetType() == typeof(string))
                {
                    if (stackConvert.Count == 0)
                    {
                        stackConvert.Push(item);
                    }
                    else
                    {
                        while (stackConvert.Peek().GetType() == typeof(string))
                        {
                            reversePolishNotation.Add(stackConvert.Pop());
                        }
                        stackConvert.Push(item);
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
                    if (expression[i].GetType() == typeof(char) || expression[i].GetType() == typeof(string))
                    {
                        MathAction(ref expression, i);
                        break;
                    }
                }
            }

            try
            {
                return (double)expression[0];
            }
            catch(Exception ex)
            {
                throw new Exception("Function Error");
            }
            


        }

        /// <summary>
        /// Математическое дейтсвие над двумя числами в Обратной Польской Нотации
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="manipulator"></param>
        private static void MathAction(ref List<object> expression, int manipulator)
        {
            if (expression[manipulator].GetType() == typeof(char))
            {
                switch ((char)expression[manipulator])
                {
                    case '+':
                        expression[manipulator - 2] = (double)expression[manipulator - 2] + (double)expression[manipulator - 1];
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
                        expression[manipulator - 2] = Math.Pow((double)expression[manipulator - 2], (double)expression[manipulator - 1]);
                        expression.RemoveAt(manipulator - 1);
                        expression.RemoveAt(manipulator - 1);
                        break;

                }
            }
            else if(expression[manipulator].GetType() == typeof(string))
            {
                switch ((string)expression[manipulator])
                {
                    case "SIN":
                        expression[manipulator - 1] = Math.Sin((double)expression[manipulator - 1]);
                        expression.RemoveAt(manipulator);
                        break;
                    case "COS":
                        expression[manipulator - 1] = Math.Cos((double)expression[manipulator - 1]);
                        expression.RemoveAt(manipulator);
                        break;
                    case "SQRT":
                        expression[manipulator - 1] = Math.Sqrt((double)expression[manipulator - 1]);
                        expression.RemoveAt(manipulator);
                        break;
                }
            }
            else
            {
                throw new Exception("invalid type in Revers Polish Notation");
            }
        }

    }
}
