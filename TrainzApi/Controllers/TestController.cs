using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TrainzLib.Models;
using TrainzLib.Repository;

namespace TrainzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Утилиты для тестирования")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ICrudRepository<Vagon> _vagonRepository;
        private readonly ICrudRepository<GruzType> _gruzRepository;
        private readonly ICrudRepository<Station> _stationRepository;
        private readonly ICrudRepository<Way> _wayRepository;
        private readonly ICrudRepository<VagonType> _vagonTypeRepository;

        public TestController(ILogger<TestController> logger,
            ICrudRepository<Vagon> vagonRepository,
            ICrudRepository<GruzType> gruzRepository,
            ICrudRepository<Station> stationRepository,
            ICrudRepository<Way> wayRepository,
            ICrudRepository<VagonType> vagonTypeRepository)
        {
            _logger = logger;
            _vagonRepository = vagonRepository;
            _gruzRepository = gruzRepository;
            _stationRepository = stationRepository;
            _wayRepository = wayRepository;
            _vagonTypeRepository = vagonTypeRepository;
        }

        [HttpGet("testData1")]
        public void MakeTestData()
        {
            foreach (var g in GruzType.GetBaseList())
                _gruzRepository.Insert(g);

            foreach (var v in VagonType.GetBaseList())
                _vagonTypeRepository.Insert(v);

            MakeTestStation();
        }


        [HttpGet("testDataStation")]
        public void MakeTestStation()
        {
            int id = 0;
            var st = new Station() { Id = 1, Name = "Город Пассажирская", Description = "Ну просто станция" };
            st.Ways = new List<Way>()
            {
                new Way() {Id = ++id, Num = 1},
                new Way() {Id = ++id, Num = 2},
                new Way() {Id = ++id, Num = 3},
            };
            _stationRepository.Insert(st);
            foreach (var w in st.Ways)
                _wayRepository.Insert(w);

            st = new Station() { Id = 2, Name = "Город Грузовая", Description = "Не просто станция, а другая танция" };
            st.Ways = new List<Way>()
            {
                new Way() {Id = ++id, Num = 1},
                new Way() {Id = ++id, Num = 2},
                new Way() {Id = ++id, Num = 3},
                new Way() {Id = ++id, Num = 4},
                new Way() {Id = ++id, Num = 5},
            };
            _stationRepository.Insert(st);
            foreach (var w in st.Ways)
                _wayRepository.Insert(w);
           
        }
    }
}
