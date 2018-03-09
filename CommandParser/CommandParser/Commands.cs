using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using ConsoleUtils;
using System.Reflection;

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
            commandList.Add("help", "Help");
            commandList.Add("server", "RunServer");
            commandList.Add("client", "RunClient");
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
        public void MathCMD(object[] _math)
        {
            ConsoleUtil.Error("Not working currently");
            return;
            int math = 0;
            for (int i = 0; i < _math.Length; i++)
            {
                if (_math[i].ToString() == "+")
                {
                    int a = (int)_math[i - 1];
                    int b = (int)_math[i + 1];

                    math += a + b;
                }
                else if (_math[i].ToString() == "-")
                {
                    int a = (int)_math[i - 1];
                    int b = (int)_math[i + 1];

                    math += a - b;
                }
                else if (_math[i].ToString() == "*")
                {
                    int a = (int)_math[i - 1];
                    int b = (int)_math[i + 1];

                    math += a * b;
                }
                if (_math[i].ToString() == "/")
                {
                    int a = (int)_math[i - 1];
                    int b = (int)_math[i + 1];

                    math += a + b;
                }
                ConsoleUtil.Succeed(math.ToString());
            }

            // Try pase value 1
            /*int math = 0;

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
            } */


        }

        /// <summary>
        /// List Method :: Prints a list of all Initalized Commands
        /// </summary>
        public void Help()
        {
            foreach (var cmd in commandList)
            {
                ConsoleUtil.Succeed($"{cmd.Key}");
            }
        }

        [STAThread]
        public static void RunServer(int port)
        {
            Application.EnableVisualStyles();
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            if (port == 0)
                ServerForm.Port = 8080;
            else
            ServerForm.Port = port;

            Application.Run(new ServerForm()); // or whatever
        }

        [STAThread]
        public static void RunClient(string username, string ip, int port)
        {
            if (ip == null)
                ip = "127.0.0.1";
            if (port == 0)
                port = 8080;
            if (username == null)
                username = "Guest";

            ClientForm.Ip = ip;
            ClientForm.Port = port;
            ClientForm.Username = username;
            Application.EnableVisualStyles();
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            Application.Run(new ClientForm()); // or whatever
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CommandParser.SimpleTCP.dll"))
            {
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }
}