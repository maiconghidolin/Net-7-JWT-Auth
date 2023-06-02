using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApiDbContext _context;

    public BaseRepository(ApiDbContext context)
    {
        _context = context;
    }

    public TEntity GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
    {
        var data = _context.Set<TEntity>().Where(predicate);

        if (!tracking)
        {
            data = data.AsNoTracking();
        }

        return data;
    }


    public bool Add(TEntity entity, bool autoSave = true)
    {
        _context.Set<TEntity>().Add(entity);

        if (autoSave)
            Save();

        return true;
    }

    public bool AddRange(TEntity[] entities, bool autoSave = true)
    {
        _context.Set<TEntity>().AddRange(entities);

        if (autoSave)
            Save();

        return true;
    }


    public bool Update(TEntity entity, bool autoSave = true)
    {
        _context.Set<TEntity>().Update(entity);

        if (autoSave)
            Save();

        return true;
    }

    public bool UpdateRange(TEntity[] entities, bool autoSave = true)
    {
        _context.Set<TEntity>().UpdateRange(entities);

        if (autoSave)
            Save();

        return true;
    }


    public bool Delete(int id)
    {
        var entity = GetById(id);

        if (entity == null)
            throw new KeyNotFoundException("Entity not found");

        _context.Set<TEntity>().Remove(entity);
        Save();
        return true;
    }

    public bool Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        Save();
        return true;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}
