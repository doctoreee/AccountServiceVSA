using AccountService.API.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountService.API.Database;

public class AppDbContext<T, TKey>: IRepository<T, TKey> where T: Entity<TKey>, new()
{
    private readonly DbContext _context;
    private readonly DbSet<T> _entities;

    public AppDbContext(DbContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    public IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null) => predicate == null ? _entities.AsEnumerable() : _entities.Where(predicate).ToList();

    public async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate) => await _entities.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate) => await _entities.Where(predicate).ToListAsync();

    public async Task<T> GetByIdAsync(TKey id) => await _entities.SingleOrDefaultAsync(s => s.Id.Equals(id));

    public async Task<T> AddAsync(T entity)
    {
        var result = await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        await _entities.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<T> UpdateAsync(TKey id, T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity;
    }

    public T Update(TKey id, T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();

        return entity;
    }

    public T Add(T entity)
    {
        var result = _entities.Add(entity);
        _context.SaveChanges();
        return result.Entity;
    }

    public async Task<T> UpdateAsync(Expression<Func<T, bool>> predicate, T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity;
    }

    public Task DeleteAsync(TKey id)
    {
        var entity = _entities.SingleOrDefault(x => x.Id.Equals(id));
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return _context.SaveChangesAsync();
    }
}
