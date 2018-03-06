using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

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
        string file_finalPath;

        // Create File :: Creates the file at the path
        public void FileCreate(string path, string name)
        {
            try
            {
                file_finalPath = path + @"\" + name;
                File.Create(file_finalPath);
                Console.WriteLine($"{name} created at {path}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Create Error: {e}");
            }
        }

        // Edit File :: Edits the file at the path
        public void FileEdit(string path, string contents)
        {
            try
            {
                    File.AppendAllText(path, contents + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Edit Error: {e}");
            }
        }
        
        // Delete File :: Deletes the file at the path
        public void FileDelete(string path)
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

        public void Google(string question)
        {
            Run("chrome", $@"https://www.google.com/search?q={question}");
        }

        public void Close()
        {
            Environment.Exit(0);
        }
    }
}
