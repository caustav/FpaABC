using Application.Common;
using System;

namespace Infrastructure.Configuraions
{
    public class Configuration : IConfiguration
    {
        public string DatabaseName { get; set; } = default!;
        public string DatabaseConnectionString { get; set; } = default!;

        public void Load()
        {
            DatabaseName = "MFpaService";
            DatabaseConnectionString = "mongodb://127.0.0.1:27017";
        }
    }
}
