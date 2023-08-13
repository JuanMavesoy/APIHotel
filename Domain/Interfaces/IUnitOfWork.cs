using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
        void Dispose();
        IGenericRepository<T> Repository<T>() where T : class;
        void Rollback();
    }
}