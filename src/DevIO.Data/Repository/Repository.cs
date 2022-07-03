using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new() // Eu posso dar um new dessa entity
    {
        protected MeuDbContext Db;
        protected DbSet<TEntity> DbSet;
        public Repository(MeuDbContext contexto)
        {
            Db = contexto;
            DbSet = Db.Set<TEntity>();

        }

        public async Task Adicionar(TEntity entity)
        {
            DbSet.AddAsync(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await Db.Set<TEntity>().ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            var entity = new TEntity() { Id = id };
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
