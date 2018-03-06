using System;

namespace CommandParser
{
    class CommandHandler
    {

        public enum CommandCaller
        {
            Script,
            Console
        };

        Commands cmds;
        Parser parser = new Parser();

        public void Command(CommandCaller caller, string cmd = "")
        {
            cmds = new Commands();

            if (cmd == "" || cmd == null)
            {
                cmd = Console.ReadLine();
            }

            string[] parameters = parser.Parse(cmd);
            string command = parameters[0];

            try
            {
                switch (command)
                {
                    // Echo Command :: Prints text to the console
                    case "Echo":
                        cmds.Echo(parameters[1]);
                        goto Reset;

                    // File Command :: Allows you to Create, Edit, or Delete files
                    case "File":
                        try
                        {
                            if (parameters[1].ToLower() == "create")
                                cmds.FileCreate(parameters[2], parameters[3]);
                            else if (parameters[1].ToLower() == "edit")
                                cmds.FileEdit(parameters[2], parameters[3]);
                            else if (parameters[1].ToLower() == "delete")
                                cmds.FileDelete(parameters[2]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        goto Reset;

                    // Clear Command :: Clears the console 
                    case "Clear":
                        cmds.Clear();
                        goto Reset;

                    // Run Command :: Runs an application, can take arugments such as a browser link if using something like chrome
                    case "Run":
                        if (parameters[2] == null)
                            cmds.Run(parameters[1]);
                        if (parameters[2] != null)
                            cmds.Run(parameters[1], parameters[2]);
                        goto Reset;

                    // Google Command :: Googles whatever you type as the arugment
                    case "Google":
                        cmds.Google(parameters[1]);
                        goto Reset;
                    
                    // Close Command :: Closes the console
                    case "Close":
                        cmds.Close();
                        goto Reset;

                    // Default Case :: Runs when there isn't a valid command
                    default:
                        Console.WriteLine($"{command} does not exist.");
                        goto Reset;
                    
                    // Reset Label :: Calls the reset function and breaks
                    Reset:
                        Reset(caller);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"CommandHandler Error: {e}");
            }
        }

        public void Reset(CommandCaller caller)
        {
            if (caller == CommandCaller.Console)
            {
                Console.Write("> ");
                Command(CommandCaller.Console);
            }
        }
    }
}
