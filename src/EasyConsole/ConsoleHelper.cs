using System;
using System.Collections.Generic;
using System.Text;

namespace EasyConsole
{
    public class ConsoleHelper
    {
        public static string AskQuestion(string question)
        {
            Console.Write($"{question} : ");
            return Console.ReadLine();
        }

        public static int AskQuestionInt(string question)
        {
            while (true)
            {
                Console.Write($"{question} : ");
                var input = Console.ReadLine();
                if (int.TryParse(input?.Trim(), out var result) ) return result;
                Console.WriteLine("Please enter a valid integer value.");
            }

        }
    }
}
