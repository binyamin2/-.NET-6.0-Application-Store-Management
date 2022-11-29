using DO;
using System.Diagnostics;

namespace DalApi;

public interface ICrud<T> where T : struct
{

    public int Add(T value);
    public void Update(T value);

    public void Delete(int value);
    public T Get(int id);

    public T Get(Func<T?, bool>? predict);

    public IEnumerable<T> GetAll(Func<T?,bool>? predict =null);





}


