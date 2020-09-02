using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        Task<T> PesquisarPorIdAsync(Guid id);
        Task<ICollection<T>> PesquisarTodosAsync();
        Task<bool> ExisteAsync(Guid id);
        Task<bool> CadastrarAsync(T obj);
        Task<bool> EditarAsync(T obj);
        Task<bool> ExcluirAsync(Guid id);
    }
}
