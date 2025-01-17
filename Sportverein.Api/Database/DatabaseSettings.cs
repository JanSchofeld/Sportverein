using System;

namespace Sportverein.Api.Database;

public class DatabaseSettings
{
    public string Host { get; set; }
    public string Database { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string ConnectionString
    {
        get
        {
            return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};";
        }
    }
}