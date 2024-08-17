using Dtat.Ddd.EntityFrameworkCore.Rich.Enums;

namespace Dtat.Ddd.EntityFrameworkCore.Rich
{
    public class Options : object
    {
        public Options() : base()
        {
        }

        // **********
        public Provider Provider { get; set; }
        // **********

        // **********
        public string ConnectionString { get; set; }
        // **********

        // **********
        public string InMemoryDatabaseName { get; set; }
        // **********
    }
}
