using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ModelContext : DbContext
    {
        public ModelContext() : base(GetConnectionString("TextAnalyzer"))
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Database.CreateIfNotExists();

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EntityBase>();
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
        }

        static string GetConnectionString(string dbName, string userName = "root", string pass = "") => string.Format(ConfigurationManager.ConnectionStrings["mysqlCon"].ConnectionString, dbName, userName, pass);
    }
}

