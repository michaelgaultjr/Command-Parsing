using System;
using System.Reflection;
using System.Linq;

namespace CommandParser
{
    class CommandHandler
    {
        // CommandCaller Enum :: Stores the caller options
        public enum CommandCaller
        {
            Script,
            Console
        };

        Commands _commands;
        Parser parser = new Parser();

        public void Command(CommandCaller caller, string cmd = "")
        {
            _commands = new Commands();

            if (cmd == "" || cmd == null)
            {
                cmd = Console.ReadLine();
            }

            string[] parameters = parser.Parse(cmd);
            if (string.IsNullOrWhiteSpace(cmd))
            {
                goto End;
            }
            string command = parameters[0].ToLower();

            try
            {
                switch (command)
                 {
                     // Echo Command :: Prints text to the console
                     case "echo":
                         _commands.Echo(parameters[1]);
                         goto Reset;

                     // File Command :: Allows you to Create, Edit, or Delete files
                     case "file":
                        try
                        {
                            if (parameters[1].ToLower() == "create" || parameters[1].ToLower() == "edit")
                            {
                                _commands.FileCMD(parameters[1], parameters[2], parameters[3]);
                                _commands.FileCMD(parameters[1], parameters[2], parameters[3]);
                            }
                            else if (parameters[1].ToLower() == "delete")
                                _commands.FileCMD(parameters[1], parameters[2]);
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e);
                         }
                         goto Reset;

                     // Clear Command :: Clears the console 
                     case "clear":
                         _commands.Clear();
                         goto Reset;

                     // Run Command :: Runs an application, can take arugments such as a browser link if using something like chrome
                     case "run":
                         if (3 > parameters.Length)
                             _commands.Run(parameters[1]);
                         else if (3 <= parameters.Length)
                             _commands.Run(parameters[1], parameters[2]);
                         goto Reset;

                     // Google Command :: Googles whatever you type as the arugment
                     case "google":
                         _commands.Google(parameters[1]);
                         goto Reset;

                     // Close Command :: Closes the console
                     case "close":
                         _commands.Close();
                         goto Reset;

                     case "msgbox":
                         if (3 > parameters.Length)
                             _commands.MsgBox(parameters[1]);
                         else if (3 <= parameters.Length)
                             _commands.MsgBox(parameters[1], parameters[2]);           
                         goto Reset;

                    case "var":
                        if (4 <= parameters.Length)
                            _commands.Var(parameters[1], parameters[2], parameters[3]);
                        else if (3 > parameters.Length)
                            _commands.Var(parameters[1]);
                        goto Reset;

                    case "return":
                        _commands.Return(parameters[1]);
                        goto Reset;

                    case "math":
                        _commands.MathCMD(parameters[1], parameters[2], parameters[3]);
                        goto Reset;

                     // Default Case :: Runs when there isn't a valid command
                     default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine($"Command '{command}' does not exist.");
                         Console.ForegroundColor = ConsoleColor.Cyan;
                        goto Reset;

                     // Reset Label :: Calls the reset function and breaks
                     Reset:
                         Reset(caller);
                         break;
                 }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"CommandHandler Error: {e}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Reset(CommandCaller.Console);
            }
            End:
            Reset(caller);
        }

        public void Reset(CommandCaller caller)
        {
            if (caller == CommandCaller.Console)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Command(CommandCaller.Console);
            }
            if (caller == CommandCaller.Script)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            
        }
    }
}
