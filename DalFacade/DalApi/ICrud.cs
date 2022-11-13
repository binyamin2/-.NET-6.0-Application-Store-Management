using DO;

namespace DalApi;

public interface ICrud<T>
{

    public int? Add(T value);
    public void Update(T value);

    public void Delete(int value);

    public T Get(int id);

    


}


