using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    /// <summary>
    /// Console logging logging method
    /// </summary>
    internal class ConsoleLogging : ILoggingMethod
    {
    
        /// <summary>
        /// Logs msg to the console
        /// </summary>
        /// <param name="message">Message</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    
    }
}
