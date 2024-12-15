using System.Linq.Expressions;
using Assignment.Application;
using Assignment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly AssignmentDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AssignmentDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> GetCountAsync()
    {
        return await _dbSet.CountAsync();
    }
}
