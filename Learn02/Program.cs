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




            
        }
    }
}
