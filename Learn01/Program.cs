using System;

namespace Learn01
{
    class Program
    {

        static void Main(string[] args)
        {
            
            decimal[] numbers = new decimal[2];
            for (int i = 0; i < numbers.Length; i++)
            { 
                while (true)
                {
                    
                    Console.WriteLine("Enter {0} from {1} for sum",i+1,numbers.Length);
                    try
                    {
                        numbers[i] = Convert.ToDecimal(Console.ReadLine());
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                        Console.ResetColor();
                    }
                }
            }

            decimal finalSum = 0;
            foreach (decimal number in numbers)
            {
                try
                {
                    finalSum += number;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.WriteLine("Sum = {0}",finalSum);
            
                       
        }

       
    }
}
