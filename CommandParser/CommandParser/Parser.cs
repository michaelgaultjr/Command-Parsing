using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommandParser
{
    class Parser
    {
        CommandHandler handler;
        public void Run(string path)
        {
            handler = new CommandHandler();

            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                handler.Command(CommandHandler.CommandCaller.Script, lines[i]);
            }

        }

        public string[] Parse(string cmd)
        {
            var parts = Regex.Matches(cmd, @"[\""].+?[\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToArray();
            return parts;
        }
    }
}
