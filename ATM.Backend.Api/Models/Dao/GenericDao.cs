using Microsoft.EntityFrameworkCore;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public abstract class GenericDao<T> : IDao<T> where T : class, Model
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
        // O operador ! indica que confiamos que o Find não retornará nulo ou 
        // que trataremos isso na camada de serviço.
        return _context.Set<T>().Find(id)!;
    }

    public T Create(T entity)
    {
        // Se o Service já abriu uma transação, o EF Core a usará automaticamente.
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Verifica se o registro ainda existe no banco
            if (!_context.Set<T>().Any(c => c.Id == entity.Id))
            {
                return null!; 
            }
            throw;
        }
        return entity;
    }

    public T Delete(int id)
    {
        T entity = _context.Set<T>().Find(id)!;

        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        
        return entity!;
    }
}