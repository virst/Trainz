namespace TrainzLib.Repository
{
    public interface ICrudRepository<T>
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void DeleteById(int id);
        void Update(T entity);
        void Insert(T entity);

    }
}
