using System;
using System.IO;

namespace CommandParser
{
    class Program
    {
        static Parser fileParse = new Parser();
        static CommandHandler handler = new CommandHandler();
        static void Main(string[] args)
        {
            if (args.Length == 1) //make sure an argument is passed
            {
                FileInfo file = new FileInfo(args[0]);
                string extention = Path.GetExtension(args[0]);
                if (file.Exists && extention == ".ns") //make sure it's actually a file
                {
                    fileParse.Run(args[0]);
                }
            }
            handler.Reset(CommandHandler.CommandCaller.Console);
            Console.ReadKey();
        }

        
    }
}