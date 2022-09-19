using TFG.Assessment.Domain.Entities;

namespace TFG.Assessment.Domain.Interfaces.Repository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
