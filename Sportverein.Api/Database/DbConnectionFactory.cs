using System;
using System.ComponentModel;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Sportverein.Api.Database;

public class DbConnectionFactory
{
    private DatabaseSettings databaseSettings;

    public DbConnectionFactory(IOptions<DatabaseSettings> databaseSettings)
    {
        this.databaseSettings = databaseSettings.Value;
    }

    public NpgsqlConnection GetConnection()
    {
        var connection = new NpgsqlConnection(databaseSettings.ConnectionString);
        connection.Open();
        return connection;
    }
}
