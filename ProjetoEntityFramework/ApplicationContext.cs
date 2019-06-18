using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ProjetoEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEntityFramework
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext( DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        // comando para usar o Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //O Fluent API serve para especificar critérios relacionados aos atributos do banco
            //Como por exemplo: Especificar o tamanho mx e min de campo da tabela no banco

            // comando para definir o nome da tabela
            modelBuilder.Entity<Produto>().ToTable("Produto");
            //comando para definir o tam. max. do campo - Nome
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(48);
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
    }
}
