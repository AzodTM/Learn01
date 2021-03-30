using System;

namespace Learn02
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[2];
            for (int i = 0; i < numbers.Length; i++)
            {
                while (true)
                {

                    Console.WriteLine("\nEnter {0} integer from {1} for NOD & NOK", i + 1, numbers.Length);
                    try
                    {
                        numbers[i] = Convert.ToInt32((Convert.ToDecimal(Console.ReadLine())));
                        Console.WriteLine("you entering {0}", numbers[i]);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.Error.Write(ex);
                    }
                }


            }
            Console.WriteLine("NOD = {0}",calculateNOD(numbers[0], numbers[1]));
            Console.WriteLine("NOK = {0}",CalculateNOK(numbers[0], numbers[1]));
        }
        
        static decimal calculateNOD(decimal firstValue, decimal secondValue)
        {
                       
            if (secondValue < 0)
                secondValue = -secondValue;
            if (firstValue < 0)
                firstValue = -firstValue;
            while (secondValue > 0)
            {
                decimal temp = secondValue;
                secondValue = firstValue % secondValue;
                firstValue = temp;
            }
            return firstValue;
        }

        static decimal CalculateNOK(decimal firstValue, decimal secondValue)
        {
            return Math.Abs(firstValue * secondValue) / calculateNOD(firstValue, secondValue);
        }
    }
}
