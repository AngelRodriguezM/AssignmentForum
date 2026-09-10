using ProjectEntities;

namespace RepositoryContracts;

public interface ILikeAsync
{
    Task<Like> AddAsync(Like like);
    Task UpdateAsync(Like like);
    Task DeleteAsync(int id);
    Task<Like> GetSingleAsync(int id);
    IQueryable<Like> GetMany();
}