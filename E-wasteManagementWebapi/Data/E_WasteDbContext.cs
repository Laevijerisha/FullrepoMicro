using Microsoft.EntityFrameworkCore;
using E_wasteManagementWebapi.Model;
using System;
using E_wasteManagementWebapi.DTO;
namespace E_wasteManagementWebapi.Data
{
    public class E_WasteDbContext:DbContext
    {
        public E_WasteDbContext(DbContextOptions<E_WasteDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Center> centers { get; set; }
      

        public DbSet<E_WasteItem> waste_items { get; set; }

       

    }
}
