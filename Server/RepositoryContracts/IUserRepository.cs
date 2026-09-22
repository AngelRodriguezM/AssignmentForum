using ProjectEntities;

namespace RepositoryContracts;

public interface IUserRepository
{
   Task<User> AddAsync(User user);
   Task UpdateAsync(User user);
   Task DeleteAsync(int id);
   Task<User> GetSingleAsync(string username);
   IQueryable<User> GetMany();

   Task<User> GetByIdAsync(int authorId);
}