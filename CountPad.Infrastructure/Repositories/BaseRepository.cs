﻿using System;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CountPad.Infrastructure.Repositories
{
    public class BaseRepository
    {
        private readonly IConfiguration configuration;

        public BaseRepository()
        {
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() +
                    "..\\..\\..\\..\\")
                        .AddJsonFile("appsettings.json", optional: true).Build();

            CheckDataBaseExists(configuration.GetConnectionString("Postgres"));
        }

        protected NpgsqlConnection CreateConnection() =>
            new NpgsqlConnection(this.configuration
                .GetConnectionString("CountPadConnection"));

        private async void CheckDataBaseExists(string stringConnection)
        {
            try
            {
                using (NpgsqlConnection connection = CreateConnection())
                {
                    connection.Open();
                }
            }
            catch (Exception)
            {
                using (var connection = new NpgsqlConnection(stringConnection))
                {
                    connection.Open();
                    string query = $"create database CountPadDb";

                    await connection.ExecuteAsync(query);
                    CreateTables();
                }
            }
        }

        private void CreateTables()
        {
            using (NpgsqlConnection connection = CreateConnection())
            {
                connection.Open();

                string query = $@"{File.ReadAllText("../../../../CountPad.Infrastructure/query.txt")}";
                connection.Execute(query);
            }
        }
    }
}
