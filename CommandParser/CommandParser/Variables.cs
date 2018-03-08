using System.Collections.Generic;

namespace CommandParser
{
    class Variables
    {
        Parser _parser = new Parser();

        /// <summary>
        /// Stores all The Varibles
        /// </summary>
        public static Dictionary<string, object> varList = new Dictionary<string, object>();
    }
}
