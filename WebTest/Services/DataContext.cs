using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Xml;
using WebTest.Models;

namespace WebTest.Services
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

        public void InsertOrUpdate<T>(T model)
            where T : class, IModel
        {
            var set = Set<T>();

            var id = model.GetId();
            if (set.Any(e => e.GetId() == id))
            {
                set.Attach(model);
                Entry(model).State = EntityState.Modified;
            }
            else
            {
                set.Add(model);
            }

            SaveChanges();
        }
    }
}
