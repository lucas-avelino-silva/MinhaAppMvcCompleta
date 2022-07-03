using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            // A interface vai pedir para escrever esse método, vai fornecer pré pronto.
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 para 1 => Fornecedor : Endereco. Fornecedor tem um endereco onde o fornecedor tem apenas um fornecedor 
            builder.HasOne(p => p.Endereco)
                .WithOne(e => e.Fornecedor);

            // 1 para N => Fornecedor : Produtos.
            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
