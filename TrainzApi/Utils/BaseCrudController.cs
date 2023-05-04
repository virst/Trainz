using Microsoft.AspNetCore.Mvc;
using TrainzLib.Repository;


namespace TrainzApi.Utils
{
    public class BaseCrudController<T> : ControllerBase
    {
        protected readonly ILogger<BaseCrudController<T>> _logger;
        protected readonly ICrudRepository<T> _repository;

        public BaseCrudController(ILogger<BaseCrudController<T>> logger, ICrudRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<T> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public T Get(int id)
        {
            return _repository.GetById(id) ?? throw new HttpRequestException("Not Found");
        }

        [HttpPut]
        public void Update(T t)
        {
            _repository.Update(t);
        }

        [HttpPost]
        public void Insert(T t)
        {
            _repository.Insert(t);
        }

        [HttpDelete]
        public void Delete(int id) 
        {
            _repository.DeleteById(id);
        }
    }
}
