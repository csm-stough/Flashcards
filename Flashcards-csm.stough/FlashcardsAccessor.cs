using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough
{
    internal class FlashcardsAccessor
    {
        private static string connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
        private static string databaseName = System.Configuration.ConfigurationManager.AppSettings.Get("DBName");
        private static string flashcardsTableName = System.Configuration.ConfigurationManager.AppSettings.Get("FlashcardsTableName");

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
                    SELECT COUNT(*) FROM {flashcardsTableName}
                    END";
                count = (int)command.ExecuteScalar();

                connection.Close();
            }

            return count;
        }

        public static DTOs.Flashcard Insert(string question, string answer, DTOs.Stack stack)
        {
            DTOs.Flashcard flashcard = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    INSERT INTO {flashcardsTableName} VALUES ('{question}', '{answer}', {stack.Id});
                    SELECT SCOPE_IDENTITY();
                    END";

                object result = command.ExecuteScalar();
                result = (result == DBNull.Value) ? null : result;
                int Id = Convert.ToInt32(result);

                flashcard = new DTOs.Flashcard(Id, question, answer, stack);

                connection.Close();
            }

            return flashcard;
        }

        public static void Update(DTOs.Flashcard flashcard)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    UPDATE {flashcardsTableName} SET Question = '{flashcard.Question}', Answer = '{flashcard.Answer}',
                    StackId = {flashcard.stack.Id} WHERE Id = {flashcard.Id}
                    END";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static List<DTOs.Flashcard> Get(DTOs.Stack stack)
        {
            List<DTOs.Flashcard> flashcards = new List<DTOs.Flashcard>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    BEGIN
                    SELECT TOP 100 * FROM {flashcardsTableName} WHERE StackId = {stack.Id}
                    END";
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        flashcards.Add(new DTOs.Flashcard(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), stack));
                    }
                }

                connection.Close();
            }

            return flashcards;
        }
    }
}
