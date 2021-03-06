using System;
using System.Collections.Generic;
using System.Text;

namespace Learn03
{
    class Analisis
    {
        [Flags]
        public enum Status
        {
            AddedNumber = 1,
            AddedOperator = 2,
            AddedNegative = 4,
            AddedRightBracket = 8,
            AddedLeftBracket = 16,
            AddedFunction = 32
        }
        /// <summary>
        /// парсер строки на символы, цифры и функции
        /// </summary>
        /// <param name="userExpression"></param>
        /// <returns></returns>
        public static List<object> ParseSyntacticAnalisis(string userExpression)
        {
            List<object> result = new List<object>();
            if (userExpression.Contains(','))
            {
                throw new Exception("Error ParseSemanticAnalisisOfString char ','");
            }

            if(userExpression.Length == 0)
            {
                result.Add('=');
                return result;
            }            
            else if(userExpression[0] == '+' || userExpression[0] == '-' || userExpression[0] == '*' || userExpression[0] == '/' ||
                userExpression[0] == '^' || userExpression[0] == '(' || userExpression[0] == ')')
            {
                result.Add(userExpression[0]);
                result.AddRange(ParseSyntacticAnalisis(userExpression.Remove(0, 1)));
                return result;
            }
            else if(userExpression[0] >= '0' && userExpression[0] <= '9')
            {
                double number;
                string userExpressionCopy = userExpression;
                while(!double.TryParse(userExpressionCopy,out number))
                {
                    userExpressionCopy = userExpressionCopy.Remove(userExpressionCopy.Length-1);
                }
                result.Add(number);
                result.AddRange(ParseSyntacticAnalisis(userExpression.Remove(0,userExpressionCopy.Length)));
                return result;
            }
            else if(userExpression.StartsWith("COS",System.StringComparison.CurrentCultureIgnoreCase))
            {
                result.Add("COS");
                result.AddRange(ParseSyntacticAnalisis(userExpression.Remove(0,3)));
                return result;
            }
            else if (userExpression.StartsWith("SIN", System.StringComparison.CurrentCultureIgnoreCase))
            {
                result.Add("SIN");
                result.AddRange(ParseSyntacticAnalisis(userExpression.Remove(0, 3)));
                return result;
            }
            else if (userExpression.StartsWith("SQRT", System.StringComparison.CurrentCultureIgnoreCase))
            {
                result.Add("SQRT");
                result.AddRange(ParseSyntacticAnalisis(userExpression.Remove(0, 4)));
                return result;
            }
            throw new Exception("Error ParseSemanticAnalisisOfString");
        }

        /// <summary>
        /// Синтаксический анализ выражения
        /// </summary>
        /// <param name="userExpression"></param>
        /// <returns></returns>
        public static List<object> SemanticAnalisis(List<object> userExpression, Status status = 0)
        {
            int countRightBreckets = 0;
            int countLeftBreckets = 0;
            List<object> result = new List<object>();
            

            foreach(object i in userExpression)
            {
                if(i.GetType() == typeof(string) || i.GetType() == typeof(double))
                {
                    if ((status & Status.AddedRightBracket) == Status.AddedRightBracket)
                    {
                        result.Add('*');                       
                    }   
                    else if((status & (Status.AddedLeftBracket | Status.AddedNegative)) == (Status.AddedLeftBracket | Status.AddedNegative))
                    {
                        result.Add(-1);
                        result.Add('*');
                    }
                    else if((status & Status.AddedNumber) == Status.AddedNumber)
                    {
                        if(i.GetType() == typeof(double))
                        {
                            throw new Exception("doble number");
                        }
                        result.Add('*');
                    }
                    else if((status & Status.AddedFunction) == Status.AddedFunction && i.GetType() == typeof(string))
                    {
                        throw new Exception("doble function");
                    }
                    else if((status & Status.AddedNegative) == Status.AddedNegative)
                    {
                        result.Add('-');
                    }
                    status = 0;
                    if (i.GetType() == typeof(string))
                    {
                        status = Status.AddedFunction;
                    }
                    else
                    {
                        status = Status.AddedNumber;
                    }
                }
                else if(i.GetType() == typeof(char))
                {
                    if ((char)i == '=')
                    {
                        status = 0;
                    }
                    else if(status == Status.AddedOperator && ((char)i != '(' && (char)i != ')'))
                    {
                        throw new Exception("Double operator");
                    }
                    else if ((char)i == '-')
                    {
                        if ((status & Status.AddedNegative) == Status.AddedNegative)
                        {
                            throw new Exception("Double negative");
                        }                        
                        else
                        {
                            status = Status.AddedNegative | Status.AddedOperator | (status & Status.AddedLeftBracket);
                        }
                        
                        continue;
                    }
                    else if((char)i == '(')
                    {
                        if((status & Status.AddedNegative) == Status.AddedNegative)
                        {
                            result.Add('-');
                        }
                        else if((status & Status.AddedNumber) == Status.AddedNumber)
                        {
                            result.Add('*');
                        }  
                        status = Status.AddedLeftBracket;
                        countLeftBreckets++;
                    }
                    else if((char)i == ')')
                    {
                        if (status == Status.AddedNegative)
                        {
                            throw new Exception("-) negative right bracket");
                        }
                        else if(status == Status.AddedOperator)
                        {
                            throw new Exception("*+^ operator before right bracket");
                        }
                        status = 0;
                        status = Status.AddedRightBracket;
                        countRightBreckets++;
                    }
                    
                    else
                    {
                        if (status == Status.AddedLeftBracket)
                        {
                            throw new Exception("(+-*^ operator after right bracket");
                        }
                        
                        status = Status.AddedOperator;                        
                    }
                }
               
                
                result.Add(i);
            }

            if (countLeftBreckets != countRightBreckets)
            {
                throw new Exception("countLeftBreckets != countRightBreckets");
            }
            else if(status == Status.AddedOperator || status == Status.AddedNegative)
                {
                    throw new Exception("error at end expression");
                }
            return result;
        }
        
    }
}
