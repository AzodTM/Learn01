using System;
using System.Collections.Generic;
using System.Text;

namespace Learn03
{
    
    class PrintConsole
    {
        
            
        /// <summary>
        /// Отрисовка всех элементов листа объектов с модной подцветкой
        /// </summary>
        /// <param name="value"></param>
        public static void ListObj(List<object> value)
        {
            Console.WriteLine(new string('_', 50));

            foreach(object i in value)
            {
              if(i.GetType() == typeof(double))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;                    
                }
              else if(i.GetType() == typeof(string))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
              else if(i.GetType() == typeof(char))
                {
                    if((char)i == ')' || (char)i == '(')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                }
                Console.Write(i + " ");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(new string('_', 50));
        }
    }
}
