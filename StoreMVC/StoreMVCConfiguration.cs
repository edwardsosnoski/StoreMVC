using System;
namespace StoreMVC
{
    public class StoreMVCConfiguration
    {
        public Database Database { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }
}