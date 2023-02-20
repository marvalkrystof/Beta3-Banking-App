using BankingSystemMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankingSystemMVC.UnitOfWork
{
    public class IsolationLevel
    {

        private static System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted;


        public static void SetIsolationLevel(System.Data.IsolationLevel isolation)
        {
            isolationLevel = isolation;
        }
        public static System.Data.IsolationLevel GetIsolationLevel() 
        {
            return isolationLevel;
        }

        private static void GetIsolationLevelContext(BankingSystemDbContext context)
        {

            System.FormattableString query = $"SELECT CASE transaction_isolation_level \r\n    WHEN 0 THEN 'Unspecified' \r\n    WHEN 1 THEN 'ReadUncommitted' \r\n    WHEN 2 THEN 'ReadCommitted' \r\n    WHEN 3 THEN 'Repeatable' \r\n    WHEN 4 THEN 'Serializable' \r\n    WHEN 5 THEN 'Snapshot' END AS TRANSACTION_ISOLATION_LEVEL \r\nFROM sys.dm_exec_sessions \r\nwhere session_id = @@SPID\r\n";

            var isolationLevel = context.Database.SqlQuery<string>(query).ToList().FirstOrDefault();

            Logger.Logger.Log(isolationLevel);               
            
        }

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

    
    
    
    
    }





}
