namespace Logger
{
    /// <summary>
    /// Main class used as proxy for logging
    /// </summary>
    public class Logger
    {

        static ILoggingMethod loggingMethod = new ConsoleLogging();
       

        /// <summary>
        /// Logs the message into the specified logging method and Exits the app with exit code 1
        /// </summary>
        /// <param name="message">Message</param>
        public static void LogCriticalFailure(string message)
        {
            loggingMethod.Log(message);
            Environment.Exit(1);
        }

        /// <summary>
        /// Logs the message into the specified logging method
        /// </summary>
        /// <param name="message">Message</param>
        public static void Log(string message)
        {
            loggingMethod.Log(message);
        }



    }
}