using AituConnectApi.Repositories.Abstractions;

namespace AituConnectApi.Services.Abstractions
{
    public abstract class Service<T> : IService<T> where T : class
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
