using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CommandParser
{
    class Commands
    {

        public void Echo(string message)
        {
            Console.WriteLine(message);
        }
        
        /// <summary>
        /// File Functions
        /// <summary>

        // Create File :: Creates the file at the path
        public void FileCMD(string mode, string path, string content = null)
        {
            // Create
            if (mode.ToLower() == "create")
            {
                try
                {
                    File.Create(path + @"\" + content);
                    Console.WriteLine($"{content} created at {path}");
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
                Console.WriteLine($"Are you sure you want to delete '{path}'?" + Environment.NewLine + "Type 'CONFIRM' to confirm.");
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
        /// File Functions
        /// <summary>

        // Clear Command :: Clears the console
        public void Clear()
        {
            Console.Clear();
        }
        
        // Run Command :: Runs an application, takes up to 1 argument
        public void Run(string application, string args = null)
        {
            if (args == null)
                Process.Start(application);

            if (args != null)
                Process.Start(application, args);
        }

        // Google Command :: Googles whatever you type, takes 1 argument
        public void Google(string question)
        {
            Run("chrome", $@"https://www.google.com/search?q={question}");
        }

        // Close Command :: Closes the console
        public void Close()
        {
            Environment.Exit(0);
        }

        public void MsgBox(string text, string title = "Message Box")
        {
            MessageBox.Show(text, title);
            
        }

        public void Var(string option, string arg1 = "", string arg2 = "")
        {
            
            if (option.ToLower() == "add")
                Variables.varList.Add(arg1, arg2);
            if (option.ToLower() == "remove")
                Variables.varList.Remove(arg1);
            if (option.ToLower() == "list")
            {
                bool ran;
                ran = false;
                foreach (KeyValuePair<string, object> kvp in Variables.varList)
                {
                    ran = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Key: {kvp.Key} || Value: {kvp.Value}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                if (ran == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No Variables");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void Return(string _var)
        {
            Console.WriteLine($"{_var} >> {Variables.varList[_var]}");
        }

        double value1;
        double value2;
        public void MathCMD(string _operator, string _value1, string _value2)
        {

            // Try pase value 1
            try
            { double.TryParse(_value1, out value1); }      
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Value 1 isn't a number");
                Console.BackgroundColor = ConsoleColor.Cyan;
            }

            // Try parse value 2
            try
            { double.TryParse(_value2, out value2); }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Value 2 isn't a number ");
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            double math = 0.0;

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
    }
}
