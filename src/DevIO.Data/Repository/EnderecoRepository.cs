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
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        // Passa o contexto para a classe base que é a Repository, pq ela é abstrada e ela recebe no seu construtor o contexto
        public EnderecoRepository(MeuDbContext con): base(con)
        {

        }
        public async Task<Endereco> ObterEnderecoPorForncecedor(Guid FornecedorId)
        {
            return await Db.Enderecos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == FornecedorId);
        }
    }
}
