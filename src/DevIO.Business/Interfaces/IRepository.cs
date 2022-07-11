using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where TEntity : Entity
    {
        /* Existe um método adicionar onde qualquer entidade que vai ser recebida como parametro desde que ela seja filha de Entity vai ser aceita.
         quando só tem Task é um método void, ou seja, não retorna nada.*/
        Task Adicionar(TEntity entity);

        // Retorna um objeto da entidade
        Task<TEntity> ObterPorId(Guid id);

        // Retorna uma lista de objeto da entidade
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
