using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Context
{
    public class MeuDbContext: DbContext
    {
        public MeuDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Fornecedor> Fornecedores;
        public DbSet<Endereco> Enderecos;
        public DbSet<Produto> Produtos;

        /* Para mapear as configuraçoes das colunas que eu fiz na pasta mappings. Como funciona: Ele vai pegar o contexto e vai pegar todas as entidades
         que estao mapeadas nesse contexto (DbSet) e vai buscar classes que herdam de IEntityTypeConfiguration do tipo das entidades que estao mapedas 
        no contexto; exemplo IEntityTypeConfiguration<Fornecedor>. Ai ele vai registrar todas de uma vez só sem precisar de fazer manualmente */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Passa o nome do contexto */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            /* Estudar dps */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
