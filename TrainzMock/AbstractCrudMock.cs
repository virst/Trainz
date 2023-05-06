using System.Reflection;
using TrainzLib.Repository;

namespace TrainzMock
{
    public abstract class AbstractCrudMock<T> : ICrudRepository<T>
    {
        protected readonly static List<T> mockList = new();

        protected abstract PropertyInfo IdProperty { get; }

        private int GetId(T entity) => (int)IdProperty.GetValue(entity);
        private void SetId(T entity, int id) => IdProperty.SetValue(entity, id);

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
            if (GetId(entity) == 0)
                SetId(entity, mockList.Count() + 1);
            mockList.Add(entity);
        }

        public void Update(T entity)
        {

        }

        public void ClearAll()
        {
            mockList.Clear();
        }
    }
}
