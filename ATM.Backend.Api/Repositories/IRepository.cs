namespace ATM.Backend.Api.Repositories;

public interface IRepository<T>
{
    List<T> ListAll();
    
    T GetById(int id);
    
    T Create(T entity);
    
    T Update(T entity);
    
    T Delete(int id);
}