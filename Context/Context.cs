using Microsoft.EntityFrameworkCore;
using ProyectoFinalDicsys.Models;
using System;

namespace ProyectoFinalDicsys.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
