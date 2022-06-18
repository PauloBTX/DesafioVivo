using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
    }
}
