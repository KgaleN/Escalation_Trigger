using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Escalation_Trigger
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            string connectionString = "connection_string";
            string updateQuery = "UPDATE Tickets SET subject = @NewValue WHERE ID = @RowID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewValue", "people");
                    command.Parameters.AddWithValue("@RowID", 17);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.Write("it works");
                    }
                    else
                    {
                        Console.Write("it doesnt work");
                        // The update did not affect any rows (e.g., row with the given ID not found)
                    }
                }


            }
        }
    }
    
}
