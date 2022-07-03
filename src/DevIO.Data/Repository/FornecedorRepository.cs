using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext con) : base(con)
        {

        }
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            // Para usar o AsNoTracking() precisa usar o pacote do Entity Framework Core
            return await Db.Fornecedores.AsNoTracking().Include(f => f.Endereco).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking().Include(f  => f.Produtos).Include(f => f.Endereco).FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
