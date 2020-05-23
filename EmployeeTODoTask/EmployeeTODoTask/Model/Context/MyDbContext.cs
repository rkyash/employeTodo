using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTODoTask.Model.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Employee> employees { get; set; }
        public DbSet<EmployeeTask> employeeTasks { get; set; }
        public DbSet<Prioritylevel> prioritylevels { get; set; }
        public DbSet<State> states { get; set; }
    }
}
