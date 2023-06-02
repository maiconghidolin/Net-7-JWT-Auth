using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    TEntity GetById(int id);
    IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate, bool tracking = false);

    bool Add(TEntity entity, bool autoSave = true);
    bool AddRange(TEntity[] entities, bool autoSave = true);

    bool Update(TEntity entity, bool autoSave = true);
    bool UpdateRange(TEntity[] entities, bool autoSave = true);

    bool Delete(int id);
    public bool Delete(TEntity entity);

    void Save();
}
