using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleUtils;

namespace CommandParser
{
    class Parser
    {
        CommandHandler handler;
        // Run Method :: Handles the running of script files
        public void RunFile(string path)
        {
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
                {
                    handler.HandleCommand(CommandHandler.CommandCaller.Script, lines[i]);
                }
                else // If it's a comment, ignore this line and move onto the next
                {
                    handler.Reset(CommandHandler.CommandCaller.Script);
                }          
            }
        }

        // New Parser :: Supports Strings, Ints, & Variables 
        public ArrayList Parse(string _input)
        {
            // Splits up the string for parsing // [\""].+?[\""]|[^ ]+
            object[] splitObjects = Regex.Matches(_input, @"[\""].+?[\""]|[[\""].+?[]\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToArray();

            // Contains all parsed args
            ArrayList parsedObjects = new ArrayList();
            parsedObjects.AddRange(splitObjects);
            // Loops through command
            for (int i = 0; i < parsedObjects.Count; i++)
            {
                // Varible checking
                if (parsedObjects[i].ToString().Contains("<"))
                {
                    string input = Regex.Match(parsedObjects[i].ToString(), @"\<([^)]*)\>").Groups[1].Value;                
                    if (Variables.varList.ContainsKey(input.Trim(new Char[] { '<', '>' })))
                    {
                        object value = Variables.varList[input.Trim(new Char[] { '<', '>' })];
                        string output = Regex.Replace(parsedObjects[i].ToString(), @"\<([^)]*)\>", value.ToString());
                        parsedObjects[i] = output;
                    }
                    else
                    {
                        ConsoleUtil.Error($"Variable '{input}' doesn't exist");
                    }
                }

                if (parsedObjects[i].ToString().Contains("["))
                {
                    parsedObjects[i] = parsedObjects[i].ToString().Trim(new Char[] { '[', ']' });

                    // object[] array = parsedObjects[i].ToString().Split(new Char[] { ' ' });
                    ArrayList array = new ArrayList();
                    array.AddRange(parsedObjects[i].ToString().Split(new Char[] { ' ' }));
                    for (int v = 0; v < array.Count; v++)
                    {
                        if (IsDigits(array[v].ToString()))
                        {
                            array[v] = ToInt(array[v].ToString());
                        }                 
                    }
                    parsedObjects[i] = (object[])array.ToArray(typeof(object));                 
                }

                // Int parsing
                if (IsDigits(parsedObjects[i].ToString()))
                {
                    try
                    {
                        parsedObjects[i] = ToInt(parsedObjects[i].ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Int Parse Error: {e}");
                    }
                }
                else
                {
                    try
                    {
                        parsedObjects[i] = parsedObjects[i].ToString().Trim(new Char[] { '"' });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Else Error: {e}");
                    }
                }
            }

            // Return parsed Command
            return parsedObjects;
        }

        // IsDigits Bool :: Checks if the string is a number
        bool IsDigits(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        // ToInt Int :: Converts the string to and int and returns the value
        int ToInt(string s)
        {
            Int32.TryParse(s, out int value);
            return value;
        }
    }
}
