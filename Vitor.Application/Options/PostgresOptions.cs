using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Vitor.Application.Options
{
    public class PostgresOptions
    {
        public const string SectionName = "PostgresOptions";

        public string Server { get; private set; }
        public string UserId { get; private set; }
        public string Password { get; private set; }
        public string Database { get; private set; }

        public PostgresOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            var server = section["PostgresServer"];
            var userId = section["PostgresUserId"];
            var password = section["PostgresPassword"];
            var database = section["PostgresDatabase"];

            validateConfiguration(server, userId, password, database);

            this.Server = server;
            this.UserId = userId;
            this.Password = password;
            this.Database = database;
        }

        private static void validateConfiguration(string server, string userId, string password, string database)
        {
            if (string.IsNullOrWhiteSpace(server))
                throw new KeyNotFoundException($"Config key {SectionName}:Endpoint not set.");
            if (string.IsNullOrWhiteSpace(userId))
                throw new KeyNotFoundException($"Config key {SectionName}:SavePassword not set.");
            if (string.IsNullOrWhiteSpace(password))
                throw new KeyNotFoundException($"Config key {SectionName}:SavePassword not set.");
            if (string.IsNullOrWhiteSpace(database))
                throw new KeyNotFoundException($"Config key {SectionName}:SavePassword not set.");
        }
    }
}
