using System;
using System.IO;
using ConsoleUtils;
using System.Windows.Forms;

namespace CommandParser
{
    class Program
    {
        static Parser fileParse = new Parser(); // Setup and Initalize parser instance
        static CommandHandler handler = new CommandHandler(); // Setup and Initalize CommandHandler instance

        // Main Method
        
        static void Main(string[] args)
        {
            Setup(); // Call Setup Method
            if (args.Length == 1) // Check if it's a script
            {
                FileInfo file = new FileInfo(args[0]); // Get Information about the file
                if (file.Exists && file.Extension == ".ns") // Check if the file exists and that it's a .ns(Ninja Script) File
                {
                    handler.Reset(CommandHandler.CommandCaller.Script); // Setup Handler
                    fileParse.RunFile(args[0]); // Run the script
                }
            }
            else
                handler.Reset(CommandHandler.CommandCaller.Console); // Setup Handler
            Console.ReadKey();
        }

        /// <summary>
        /// Setup Method :: Initalizes Commands, and sets the Window Title
        /// </summary>
        static void Setup()
        {
            Commands.InitCommands();
            ConsoleUtil.ColoredMessage($"[Initalized '{Commands.Count}' Commands]", ConsoleColor.White);
            DateTime today = DateTime.Today;
            Console.Title = "Ninja Script [" + today.ToShortDateString() + "]";
        }
    }
}