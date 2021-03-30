using System;

namespace Learn03
{
    class Program
    {

        static void Main(string[] args)
        {
            string expression = "";
            decimal test = 0;
            

            while(true)
            {                
                Console.WriteLine("THE CALC!");
                

                bool inputIsCorect = false;
                while (!inputIsCorect)
                {
                    expression = Console.ReadLine();
                    foreach (char symvol in expression)
                    { 
                        if(((int)symvol > 47 && (int)symvol < 58) || (int)symvol == 44)
                        {
                            inputIsCorect = true;
                        }
                        else
                        {
                            inputIsCorect = false;
                            break;
                        }
                        
                    }
                    
                }
                Console.WriteLine(test);
               

            }
        }
    }
}
