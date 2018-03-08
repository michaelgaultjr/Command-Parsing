using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using ConsoleUtils;

namespace CommandParser
{
    class Commands
    {
        /// <summary>
        /// Stores Command Keys and Values
        /// </summary>
        public static Dictionary<string, string> commandList = new Dictionary<string, string>();

        /// <summary>
        /// Initalizes commands
        /// </summary>
        public static void InitCommands()
        {
            commandList.Add("echo", "Echo");
            commandList.Add("file", "FileCMD");
            commandList.Add("clear", "Clear");
            commandList.Add("run", "Run");
            commandList.Add("google", "Google");
            commandList.Add("close", "Close");
            commandList.Add("msgbox", "MsgBox");
            commandList.Add("var", "Var");
            commandList.Add("math", "MathCMD");
            commandList.Add("list", "List");
        }

        /// <summary>
        /// Returns the number of initalized commands
        /// </summary>
        public static int Count
        {
            get
            {
                return commandList.Count;
            }
        }

        /// <summary>
        /// Echo Method :: Echos specified message back to the console
        /// </summary>
        /// <param name="message">Message to echo</param>
        public void Echo(object message)
        {
            if (message.GetType() == typeof(string))
                Console.WriteLine(message);
            else
            {
                ConsoleUtil.Error($"Type is '{message.GetType()}' command requires type '{typeof(string)}'");
            }
        }

        /// <summary>
        /// Create File :: Creates the file at the path
        /// </summary>
        /// <param name="mode">Modes: Create | Edit | Delete</param>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public void FileCMD(string mode, string path, string content = null)
        {
            // Create
            if (mode.ToLower() == "create")
            {
                try
                {
                    File.Create(path + @"\" + content);
                    ConsoleUtil.Succeed($"{content} created at {path}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Create Error: {e}");
                }
            }

            // Edit
            else if (mode.ToLower() == "edit")
            {
                try
                {
                    File.AppendAllText(path, content + Environment.NewLine);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Edit Error: {e}");
                }
            }

            // Delete
            else if (mode.ToLower() == "delete")
            {
                ConsoleUtil.Warning($"Are you sure you want to delete '{path}'?" + Environment.NewLine + "Type 'CONFIRM' to confirm.");
                string input = Console.ReadLine();
                try
                {
                    if (input == "CONFIRM")
                    {
                        File.Delete(path);
                        Console.WriteLine($"Deleted file at {path}");
                    }
                    else
                    {
                        Console.WriteLine("File was not deleted");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Delete Error: {e}");
                }
            }
        }

        /// <summary>
        /// Clear Method :: Clears the console
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Run Command :: Runs the specified applcation
        /// </summary>
        /// <param name="application">Application name/directory</param>
        /// <param name="args">Args for the application</param>
        public void Run(string application, string args = null)
        {
            if (args == null)
                Process.Start(application);

            if (args != null)
                Process.Start(application, args);
        }

        /// <summary>
        /// Google Command :: Googles whatever you type, takes 1 argument
        /// </summary>
        /// <param name="question">Question to Search for</param>
        public void Google(string question)
        {
            Run("chrome", $@"https://www.google.com/search?q={question}");
        }

        /// <summary>
        /// Close Command :: Closes the console
        /// </summary>
        public void Close()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// MsgBox Method :: Displays a message box with text and title
        /// </summary>
        /// <param name="text">Text inside the Message Box</param>
        /// <param name="title">Tile of the Message Box</param>
        public void MsgBox(string text, string title = "Message Box")
        {
            MessageBox.Show(text, title);  
        }

        /// <summary>
        /// Var Method :: Can Create, List, or Remove varibles
        /// </summary>
        /// <param name="option">Options: add | list | remove</param>
        /// <param name="name">Name of the variable</param>
        /// <param name="value">Value of the variable</param>
        public void Var(string option, string name, object value)
        {
            if (option.ToLower() == "add")
                Variables.varList.Add(name, value);
            else if (option.ToLower() == "remove")
                Variables.varList.Remove(name);
            else if (option.ToLower() == "list")
            {
                bool ran;
                ran = false;
                foreach (KeyValuePair<string, object> kvp in Variables.varList)
                {
                    ran = true;
                    if (kvp.Value.GetType() == typeof(string))
                        ConsoleUtil.Succeed($"Key: {kvp.Key} || Value: \"{kvp.Value}\" || Value Type: {kvp.Value.GetType()}");
                    else
                        ConsoleUtil.Succeed($"Key: {kvp.Key} || Value: {kvp.Value} || Value Type: {kvp.Value.GetType()}");
                }
                if (ran == false)
                {
                    ConsoleUtil.Error("No Variables found");
                }
            }
            else
                ConsoleUtil.Error($"{option} is invaild");

        }

        /// <summary>
        /// MathCMD Method :: Does math on 2 numbers (Will probably be updated to work with more numbers)
        /// </summary>
        /// <param name="_operator">Operator: + | - | * | /</param>
        /// <param name="value1">First value</param>
        /// <param name="value2">Second Value</param>
        public void MathCMD(string _operator, int value1, int value2)
        {
            // Try pase value 1
            int math = 0;

            if (_operator.ToLower() == "add")
            {
                math = value1 + value2;
                Console.WriteLine(math.ToString("N0"));
            }
            if (_operator.ToLower() == "multiply")
            {
                math = value1 * value2;
                Console.WriteLine(math.ToString("N0"));
            }
            if (_operator.ToLower() == "subtract")
            {
                math = value1 - value2;
                Console.WriteLine(math.ToString("N0"));
            }
            if (_operator.ToLower() == "divide")
            {
                math = value1 / value2;
                Console.WriteLine(math.ToString("N0"));
            }
        }

        /// <summary>
        /// List Method :: Prints a list of all Initalized Commands
        /// </summary>
        public void List()
        {
            foreach (var cmd in commandList)
            {
                ConsoleUtil.Succeed($"{cmd.Key}");
            }
        }
    }
}