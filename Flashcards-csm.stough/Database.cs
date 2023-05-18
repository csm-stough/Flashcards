using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Flashcards_csm.stough
{
    internal class Database
    {
        private static string connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
        private static string databasePath = System.Configuration.ConfigurationManager.AppSettings.Get("DBPath");
        private static string databaseName = System.Configuration.ConfigurationManager.AppSettings.Get("DBName");
        private static string stacksTableName = System.Configuration.ConfigurationManager.AppSettings.Get("StacksTableName");
        private static string flashcardsTableName = System.Configuration.ConfigurationManager.AppSettings.Get("FlashcardsTableName");
        private static string sessionsTableName = System.Configuration.ConfigurationManager.AppSettings.Get("SessionsTableName");

        public static void Init()
        {
            CreateDatabase();

            CreateTables();
        }

        private static void CreateDatabase()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}')
                    BEGIN 
                    CREATE DATABASE {databaseName} ON
                    (NAME = {databaseName}_Data, FILENAME = '{databasePath}{databaseName}.mdf',
                    SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)
                    LOG ON (NAME = {databaseName}_log,
                    FILENAME = '{databasePath}{databaseName}.ldf',
                    SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)
                    END";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private static void CreateTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = '{stacksTableName}')
                    BEGIN
                    CREATE TABLE {stacksTableName}
                    (  
                        Id INT IDENTITY PRIMARY KEY,
                        Name nvarchar(50) NOT NULL
                    )
                    END";
                command.ExecuteNonQuery();
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @$"USE {databaseName};
                    IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = '{flashcardsTableName}')
                    BEGIN
                    CREATE TABLE {flashcardsTableName}
                    (
                        Id INT IDENTITY PRIMARY KEY,
                        Question TEXT NOT NULL,
                        Answer TEXT NOT NULL,
                        StackId INT NOT NULL,
                        CONSTRAINT FK_Stack FOREIGN KEY(StackId)
                        REFERENCES {stacksTableName}(Id)
                        ON DELETE CASCADE
                        ON UPDATE CASCADE
                    )
                    END";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
