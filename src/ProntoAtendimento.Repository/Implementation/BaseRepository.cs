using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Implementation
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly EntityContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(EntityContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<T> PesquisarPorIdAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(entidade => entidade.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<T>> PesquisarTodosAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _dataset.AnyAsync(entidade => entidade.Id.Equals(id));
        }

        public async Task<ICollection<T>> PesquisarPorQueryAsnc(string stringQuery)
        {
            return await _dataset.FromSql(stringQuery).ToListAsync();
        }

        public async Task<bool> CadastrarAsync(T obj)
        {
            try
            {
                _dataset.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EditarAsync(T obj)
        {
            try
            {
                _dataset.Update(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(aux => aux.Id.Equals(id));
                if (result != null)
                {
                    _dataset.Remove(result);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public virtual void DetachLocal(Func<T,bool> predicate)
        {
            var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }


    }
}
