﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Angular_and_Dotnet.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
    }
}
