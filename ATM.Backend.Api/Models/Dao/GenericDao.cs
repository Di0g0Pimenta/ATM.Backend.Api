using Microsoft.EntityFrameworkCore;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;

namespace ATM.Backend.Api.Repositories;

public abstract class GenericDao<T> : IDao<T>  where T : class, Model
{
    
    protected readonly AppDbContext _context;

    protected GenericDao(AppDbContext context)
    {
        _context = context;
    }

    public List<T> ListAll()
    {
        return _context.Set<T>().ToList();
    }
    
    public T GetById(int id)
    {
        return _context.Set<T>().Find(id)!;
    }

    public T Create(T entity)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        
        strategy.Execute(() =>
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }); 
        
        return entity;
        
    }

    public T Update(T entity)
    {
        
        var strategy = _context.Database.CreateExecutionStrategy();

        strategy.Execute(() =>
        {
            var transaction = _context.Database.BeginTransaction();
            
            try
            { 
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<T>().Any(c => c.Id == entity.Id))
                {
                    entity = null; 
                    transaction.Rollback();
                    return;
                }
            
                throw;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        });
        
        
        return  entity;
    }

    public T Delete(int id)
    {

        T entity = _context.Set<T>().Find(id)!;

        if (entity == null)
        {
            return entity;
        }

        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}