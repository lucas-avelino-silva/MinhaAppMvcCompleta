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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext con) : base(con)
        {

        }
        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutoFornecedores()
        {
            return await Db.Produtos.AsNoTracking().Include(P => P.Fornecedor).OrderBy(P => P.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedor)
        {
            //return await Db.Produtos.AsNoTracking().Include(P => P.Fornecedor).Where(p => p.FornecedorId == fornecedor).ToListAsync;

            return await Buscar(p => p.FornecedorId == fornecedor);
        }
    }
}
