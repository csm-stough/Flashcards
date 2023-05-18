using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough
{
    internal class Input
    {
        /// <summary>
        /// Polls the user for string input, no longer than "maxLength"
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string InputString(string prompt, int maxLength = int.MaxValue)
        {
            Console.WriteLine(prompt);
            string input = string.Empty;

            do
            {
                input = Console.ReadLine();
            } while (String.IsNullOrEmpty(input) && input.Length <= maxLength);

            return input;
        }
    }
}
