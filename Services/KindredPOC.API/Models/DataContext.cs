using KindredPOC.API.Code;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindredPOC.API.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {

        }
        public DataContext(string con) : base(con)
        {

        }
        public DbSet<Item> Items { get; set; }
    }
    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            AddInterceptor(new AppInsightEFDependencyTracker());
        }
    }
}
