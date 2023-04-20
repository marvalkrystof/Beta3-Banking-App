namespace Logger
{
    /// <summary>
    /// Interface specifying what logging method must contain
    /// </summary>
    internal interface ILoggingMethod
    {
 
        /// <summary>
        /// Logs the message into the chosen logging method
        /// </summary>
        /// <param name="message">Message</param>
        void Log(string message);
        

    }
}