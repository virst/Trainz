using TrainzLib.Repository;

namespace TrainzApi.RepositoryMock
{
    public abstract class AbstractCrudMock<T> : ICrudRepository<T>
    {
        protected readonly static List<T> mockList = new ();

        protected abstract Func<T, int> GetId { get; }

        public void DeleteById(int id)
        {
            var e = GetById(id);
            if (e != null)
                mockList.Remove(e);
        }

        public IEnumerable<T> GetAll()
        {
            return mockList;
        }

        public T? GetById(int id)
        {
            return mockList.FirstOrDefault(t => GetId(t) == id);
        }

        public void Insert(T entity)
        {
            mockList.Add(entity);
        }

        public void Update(T entity)
        {
            DeleteById(GetId(entity));
            Insert(entity);
        }
    }
}
