using System;
using System.Collections.Generic;
using System.Text;

namespace Learn03
{
    class Analisis
    {
                
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
        public static List<object> SemanticAnalisis(List<object> userExpression)
        {
            throw new Exception("zaglushka"); 
        }
        
    }
}
