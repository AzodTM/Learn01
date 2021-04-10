using System;
using System.Collections.Generic;
using System.Text;

namespace Learn03
{
    class Converter
    {
        [Flags]
        enum RecordingStatus
        {
            isNumber,
            isDot,            
        }
        private static List<object> SemanticAnalisisOfString(string userExpression)
        {
            List<object> expression = new List<object>();
            double number = 0;
            RecordingStatus recordingStatisFlags = new RecordingStatus();

            foreach (char item in userExpression)
            {
                if ((recordingStatisFlags & RecordingStatus.isNumber) == RecordingStatus.isNumber)
                {
                    number = number * 10 + (userExpression[item] - '0');
                }
                else if (userExpression[item] == '+' || userExpression[item] == '-' || userExpression[item] == '*' || userExpression[item] == '/' || 
                    userExpression[item] == '^' || userExpression[item] == '(' || userExpression[item] == ')')
                {
                    expression.Add(userExpression[item]);
                }


            }
            return new List<object>();
        }
        public static List<object> SemanticAnalisisOfStringV2(string userExpression)
        {
            List<object> result = new List<object>();
            
            if(userExpression.Length == 0)
            {
                result.Add('=');
                return result;
            }
            else if(userExpression[0] == '+' || userExpression[0] == '-' || userExpression[0] == '*' || userExpression[0] == '/' ||
                userExpression[0] == '^' || userExpression[0] == '(' || userExpression[0] == ')')
            {
                result.Add(userExpression[0]);
                result.AddRange(SemanticAnalisisOfStringV2(userExpression.Remove(0, 1)));
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
                result.AddRange(SemanticAnalisisOfStringV2(userExpression.Remove(0,userExpressionCopy.Length)));
                return result;
            }
            else if(userExpression.StartsWith("COS",System.StringComparison.CurrentCultureIgnoreCase))
            {
                result.Add("COS");
                result.AddRange(SemanticAnalisisOfStringV2(userExpression.Remove(0,3)));
                return result;
            }
            else if (userExpression.StartsWith("SIN", System.StringComparison.CurrentCultureIgnoreCase))
            {
                result.Add("SIN");
                result.AddRange(SemanticAnalisisOfStringV2(userExpression.Remove(0, 3)));
                return result;
            }
            else if (userExpression.StartsWith("SQRT", System.StringComparison.CurrentCultureIgnoreCase))
            {
                result.Add("SQRT");
                result.AddRange(SemanticAnalisisOfStringV2(userExpression.Remove(0, 4)));
                return result;
            }
            throw new Exception("Error with SemanticAnalisisOfStringV2");
        }

    }
}
