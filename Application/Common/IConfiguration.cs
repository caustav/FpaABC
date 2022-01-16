using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public interface IConfiguration
    {
        public string DatabaseName { get; set; }
        public string DatabaseConnectionString { get; set; }
        void Load();
    }
}
