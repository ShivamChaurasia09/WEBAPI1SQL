using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WEBAPI1SQL.Model.WebApplication1.Models;
namespace WebApplication1.Models
{
    public class Appdbcontext : DbContext
    {

        public Appdbcontext(DbContextOptions<Appdbcontext> options)
                 : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectModels;Initial Catalog=DemoData;Integrated Security=True");
            }
        }
        public DbSet<Employee> Employees { get; set; }
    }

}
