using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using ConsoleUtils;

namespace CommandParser
{
    class CommandHandler
    {
        /// <summary>
        /// CommandCaller Enum :: Stores the caller options
        /// </summary>
        public enum CommandCaller
        {
            Script,
            Console
        };

        Commands _commandList; // Create _commandList to be initalized
        Parser parser = new Parser(); // Create parser and intalize

        /// <summary>
        /// HandleCommand Method :: Handles Command Inputs
        /// </summary>
        /// <param name="caller">Caller: Console | Script</param>
        /// <param name="input">Unparsed Command</param>
        public void HandleCommand(CommandCaller caller, string input = "")
        {
            // Initalize _commandList instance
            _commandList = new Commands();

            // Check if the input is empty
            if (string.IsNullOrWhiteSpace(input))
            {
                input = Console.ReadLine(); // Get current input if the current input is empty
            }
            
            // Create and Initalize parameters array list
            ArrayList parameters = new ArrayList();
            
            parameters.AddRange(parser.Parse(input)); // Add parsed parameters to parameters array list
            string command = parameters[0].ToString().ToLower(); // Check what the command is
            parameters.RemoveAt(0); // Remove the command from parsed parameters

            if (Commands.commandList.ContainsKey(command)) // Check is the command is vaild
            {
                ExecCommand(Commands.commandList[command], parameters); // Execute command
                Reset(caller); // Reset Handler
            }
            else
            {
                ConsoleUtil.Error($"{command} is invaild"); // Pint error if command is invaild
                Reset(caller); // Reset Handler
            }
        }

        /// <summary>
        /// Execute Command Method :: Takes the Method name and Parameters and runs it 
        /// </summary>
        /// <param name="_command">Name of the method</param>
        /// <param name="_parameters">Parameters for the method</param>
        void ExecCommand(string _command, ArrayList _parameters)
        {
            
            _commandList = new Commands();

            // Get type of method and get method info
            Type _commandType = _commandList.GetType();
            MethodInfo runMethod = _commandType.GetMethod(_command);
            
            // Get amount of Parameters
            int count = runMethod.GetParameters().Count();

            // Insert or remove parameters depending on amount
            for (int p = _parameters.Count; p < count; p++)
            {
                    _parameters.Insert(p, null);
            }
            for (int p = _parameters.Count; p > count; p--)
            {
                _parameters.RemoveAt(p - 1);
            }
            // Convert parameters to object array
            object[] finalParameters = (object[])_parameters.ToArray(typeof(object));

            // Run Command/Method
            runMethod.Invoke(_commandList, finalParameters); 
        }

        /// <summary>
        /// Reset Method :: Resets everything so it can run the next command properly
        /// </summary>
        /// <param name="caller">Caller: Console | Script</param>
        public void Reset(CommandCaller caller)
        {
            if (caller == CommandCaller.Console)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                HandleCommand(CommandCaller.Console);
            }
            if (caller == CommandCaller.Script)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }
    }
}
