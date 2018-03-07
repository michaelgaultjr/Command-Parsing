using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommandParser
{
    class Parser
    {
        CommandHandler handler;
        // Run Method :: Handles the running of script files
        public void Run(string path)
        {
            // Create handler instance
            handler = new CommandHandler();

            // Reads all the lines of the file and puts them in an array
            string[] lines = File.ReadAllLines(path);

            // Loops through each line
            for (int i = 0; i < lines.Length; i++)
            {
                // Check Comment :: Splits the first 2 chars into their own sting and checks is it's a comment line, if it is it skips that line. 
                string firstChars = lines[i].Substring(0, 2);

                // If it's not a comment pass the command
                if (!string.IsNullOrWhiteSpace(lines[i]) || firstChars != "//")
                    handler.Command(CommandHandler.CommandCaller.Script, lines[i]);
                else // If it's a comment, ignore this line and move onto the next
                    handler.Reset(CommandHandler.CommandCaller.Script);
            }

        }

        // Parse Method :: Handles all the parsing and "filtering" of commands.
        public string[] Parse(string cmd)
        {
            // Split up parameters
            var parts = Regex.Matches(cmd, @"[\""].+?[\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToArray();

            // Loop through each string in the array
            for (int i = 0; i < parts.Length; i++)
            {
                // Remove quotes from strings
                parts[i] = parts[i].Trim(new Char[] { '"' });
                if (parts[i].Contains("<"))
                {
                    // Get the key
                    string input = Regex.Match(parts[i], @"\<([^)]*)\>").Groups[1].Value;

                    // Use key to get value
                    object text = Variables.varList[input.Trim(new Char[] { '<', '>'})];

                    // Replace key with value
                    string output = Regex.Replace(parts[i], @"\<([^)]*)\>", text.ToString());
                    parts.SetValue(output, i);
                    
                }
            }
            return parts;
        }
    }
}
