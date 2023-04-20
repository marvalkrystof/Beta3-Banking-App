using BankingSystemMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BankingSystemMVC.UnitOfWork
{
    /// <summary>
    /// Used for manipulation with the isolation level of the connection
    /// </summary>
    public class IsolationLevel
    {

        private System.Data.IsolationLevel isolationLevel;

        /// <summary>
        /// Loads the isolation level from the config and sets it as the objects variable.
        /// </summary>
        public void SetIsolationLevel()
        {
            string isolationLevelString = System.Configuration.ConfigurationManager.AppSettings["isolationLevel"];
            Console.WriteLine(System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
            if (string.IsNullOrEmpty(isolationLevelString))
            {
                Logger.Logger.LogCriticalFailure("Isolation level string is null or empty");
            }

            isolationLevelString = isolationLevelString.ToUpper();
            switch (isolationLevelString)
            {
                case "READ UNCOMMITED":
                    isolationLevel = System.Data.IsolationLevel.ReadUncommitted;
                    break;
                case "READ COMMITED":
                    isolationLevel = System.Data.IsolationLevel.ReadCommitted;
                    break;
                case "REPEATABLE READ":
                    isolationLevel = System.Data.IsolationLevel.RepeatableRead;
                    break;
                case "SNAPSHOT":
                    isolationLevel = System.Data.IsolationLevel.Snapshot;
                    break;
                case "SERIALIZABLE":
                    isolationLevel = System.Data.IsolationLevel.Serializable;
                    break;
                default:
                    Logger.Logger.Log("Couldnt find desired Isolation level, switching to read uncommited");
                    isolationLevel = System.Data.IsolationLevel.ReadUncommitted;
                    break;
            }

            Logger.Logger.Log("Switched to " + isolationLevel.ToString());
        }

        /// <summary>
        /// Returns the isolation level
        /// </summary>
        /// <returns></returns>
        public System.Data.IsolationLevel GetIsolationLevel()
        {
            return isolationLevel;
        }
       

        /// <summary>
        /// Gets isolation level from the DbContext
        /// </summary>
        /// <param name="context">DbContext</param>
        private static void GetIsolationLevelContext(BankingSystemDbContext context)
        {

            System.FormattableString query = $"SELECT CASE transaction_isolation_level \r\n    WHEN 0 THEN 'Unspecified' \r\n    WHEN 1 THEN 'ReadUncommitted' \r\n    WHEN 2 THEN 'ReadCommitted' \r\n    WHEN 3 THEN 'Repeatable' \r\n    WHEN 4 THEN 'Serializable' \r\n    WHEN 5 THEN 'Snapshot' END AS TRANSACTION_ISOLATION_LEVEL \r\nFROM sys.dm_exec_sessions \r\nwhere session_id = @@SPID\r\n";

            var isolationLevel = context.Database.SqlQuery<string>(query).ToList().FirstOrDefault();
            
            Logger.Logger.Log(isolationLevel);               
            
        }


        /// <summary>
        /// Sets the isolation level for the DbContext
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="isolationLevel">Isolation level</param>
        public static void SetIsolationLevelContext(BankingSystemDbContext context,System.Data.IsolationLevel isolationLevel) {

            string isolationLevelString = "";

            switch(isolationLevel)
            {
                case System.Data.IsolationLevel.ReadUncommitted:
                    isolationLevelString = "READ UNCOMMITTED";
                    break;
                case System.Data.IsolationLevel.ReadCommitted:
                    isolationLevelString = "READ COMMITTED";
                    break;
                case System.Data.IsolationLevel.RepeatableRead:
                    isolationLevelString = "REPEATABLE READ";
                    break;
                case System.Data.IsolationLevel.Snapshot:
                    isolationLevelString = "SNAPSHOT";
                    break;
                case System.Data.IsolationLevel.Serializable:
                    isolationLevelString = "SERIALIZABLE";
                    break;
                default:
                    isolationLevelString = "READ UNCOMMITTED";
                    break;
            }

           
                var connection = context.Database.GetDbConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SET TRANSACTION ISOLATION LEVEL "+isolationLevelString+";";
                command.ExecuteNonQuery();
            


        }
        /// <summary>
        /// Gets the connection string from the config file.
        /// </summary>
        /// <returns>Connection string</returns>
        public static string GetConnectionString()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                Logger.Logger.LogCriticalFailure("Connection string is null or empty");
            }
            return connectionString;
        }





    }





}
