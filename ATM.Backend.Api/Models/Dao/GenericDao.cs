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

    public T Get(T entity)
    {
        return _context.Set<T>().Find(entity.Id)!;
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id)!;
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        try
        {
            _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Set<T>().Any(c => c.Id == entity.Id))
            {
                return null;
            }
            
            throw;
        }
        
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