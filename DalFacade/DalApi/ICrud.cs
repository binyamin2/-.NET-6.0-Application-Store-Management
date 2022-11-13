using DO;

namespace DalApi;

public interface ICrud<T>
{

    public void Add(T value);
    public void Update(T value);

    public void Delete(T value);

    public T Get(int id);

    


}


