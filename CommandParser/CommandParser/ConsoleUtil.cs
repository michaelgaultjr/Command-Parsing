using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUtils
{
    class ConsoleUtil
    {
        /// <summary>
        /// Error Method :: Displays the message in the Red Color
        /// </summary>
        /// <param name="message">The 'Error' Message</param>
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            ResetColor();
        }

        /// <summary>
        /// Warning Method :: Displays the message in the Yellow Color
        /// </summary>
        /// <param name="message">The 'Warning' Message</param>
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            ResetColor();
        }

        /// <summary>
        /// Succeed Method :: Displays the message in the Green Color
        /// </summary>
        /// <param name="message">The 'Succeed' Message</param>
        public static void Succeed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            ResetColor();
        }

        /// <summary>
        /// ColoredMessage Method :: Displays the message from the color variable
        /// </summary>
        /// <param name="message">Message to Display</param>
        /// <param name="color">Color to print message in</param>
        public static void ColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            ResetColor();
        }

        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}
