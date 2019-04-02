using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using dm = DAL.Domain;

namespace DAL.Domain
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options)
        {

        }

        public DbSet<dm.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category {Id = 1, Name = "Category 1"},
                new Category { Id = 2, Name = "Category 2" }
                );
            //base.OnModelCreating(modelBuilder);
        }
    }
}
