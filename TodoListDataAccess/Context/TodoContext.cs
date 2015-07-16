using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Initialiser;

namespace TodosList.DataAccess.Context
{
    public class TodoContext: DbContext
    {
        public TodoContext() : base("TodoList")
        {
            Database.SetInitializer<TodoContext>(new ContextInitialiser());
        }
        public DbSet<TodoCategory> TodoCategories { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<SubTodo> SubTodos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}