using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough
{
    internal class StacksAccessor
    {
        private static string connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
        private static string databaseName = System.Configuration.ConfigurationManager.AppSettings.Get("DBName");
        private static string stacksTableName = System.Configuration.ConfigurationManager.AppSettings.Get("StacksTableName");

        public static int Count()
        {
            int count;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    SELECT COUNT(*) FROM {stacksTableName}
                    END";
                count = (int)command.ExecuteScalar();

                connection.Close();
            }

            return count;
        }

        public static DTOs.Stack Insert(string Name)
        {
            DTOs.Stack stack = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    INSERT INTO {stacksTableName} VALUES ('{Name}');
                    SELECT SCOPE_IDENTITY() AS INTEGER;
                    END";

                object result = command.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                int Id = Convert.ToInt32(result);

                stack = new DTOs.Stack(Id, Name);

                connection.Close();
            }

            return stack;
        }

        public static void Update(DTOs.Stack stack)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    UPDATE {stacksTableName} SET Name = '{stack.Name}' WHERE Id = {stack.Id}
                    END";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static List<DTOs.Stack> Get()
        {
            List<DTOs.Stack> stacks = new List<DTOs.Stack>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    SELECT TOP 100 * FROM {stacksTableName}
                    END";
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        stacks.Add(new DTOs.Stack(reader.GetInt32(0), reader.GetString(1)));
                    }
                }

                connection.Close();
            }

            return stacks;
        }

        public static DTOs.Stack GetStackByName(string name)
        {
            DTOs.Stack stack = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    SELECT * FROM {stacksTableName} WHERE Name = '{name}'
                    END";
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        stack = new DTOs.Stack(reader.GetInt32(0), reader.GetString(1));
                    }
                }

                connection.Close();
            }

            return stack;
        }

        public static void Delete(DTOs.Stack stack)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    DELETE FROM {stacksTableName} WHERE Id = {stack.Id}
                    END";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
