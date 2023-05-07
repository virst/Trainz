using Microsoft.AspNetCore.Mvc;
using TrainzLib;
using TrainzLib.Models;
using TrainzLib.Operations;
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
        private readonly TrainzOperator _trainzOperator;
        private readonly IVagonInfoRepository _vagonInfoRepository;

        public TestController(ILogger<TestController> logger,
            ICrudRepository<Vagon> vagonRepository,
            ICrudRepository<GruzType> gruzRepository,
            ICrudRepository<Station> stationRepository,
            ICrudRepository<Way> wayRepository,
            ICrudRepository<VagonType> vagonTypeRepository,
            TrainzOperator trainzOperator,
            IVagonInfoRepository vagonInfoRepository)
        {
            _logger = logger;
            _vagonRepository = vagonRepository;
            _gruzRepository = gruzRepository;
            _stationRepository = stationRepository;
            _wayRepository = wayRepository;
            _vagonTypeRepository = vagonTypeRepository;
            _trainzOperator = trainzOperator;
            _vagonInfoRepository = vagonInfoRepository;
        }

        [HttpGet("helloworld")]
        public string HelloWorld()
        {
            return "hello world";
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

        [HttpGet("testReceiptVagons")]
        public string ReceiptVagons()
        {
            List<VagonInfo> list = new List<VagonInfo>();
            for (int i = 0; i < 9; i++)
            {
                VagonInfo vi = new VagonInfo()
                {
                    Vagon = new Vagon { NomVag = i + 1, VagTypeId = 1 },
                    GruzTypeId = 1,
                    WayId = (i / 3) + 1
                };
                list.Add(vi);
            }
            _trainzOperator.ReceiptVagons(list);

            return _vagonInfoRepository.GetAll().ToPrintString();
        }

        [HttpGet("testranspositionVagons")]
        public List<string> ranspositionVagons()
        {
            List<string> strings = new();
            strings.Add(_vagonInfoRepository.GetAll().ToPrintString());

            _trainzOperator.TranspositionVagons(1, new List<int> { 4 });
            strings.Add(_vagonInfoRepository.GetAll().ToPrintString());
            _trainzOperator.TranspositionVagons(2, new List<int> { 7 });
            strings.Add(_vagonInfoRepository.GetAll().ToPrintString());
            _trainzOperator.TranspositionVagons(1, new List<int> { 8 });
            strings.Add(_vagonInfoRepository.GetAll().ToPrintString());
            _trainzOperator.TranspositionVagons(3, new List<int> { 6, 5 });
            strings.Add(_vagonInfoRepository.GetAll().ToPrintString());
            _trainzOperator.TranspositionVagons(2, new List<int> { 3 });
            strings.Add(_vagonInfoRepository.GetAll().ToPrintString());

            return strings;
        }

        [HttpGet("DepartureVagons")]
        public int DepartureVagons()
        {
            _trainzOperator.DepartureVagons(new List<int> { 8, 4, 1, 2 });
            _trainzOperator.DepartureVagons(new List<int> { 7, 3 });
            _trainzOperator.DepartureVagons(new List<int> { 9, 6, 5 });

            return _vagonInfoRepository.GetAll().Count();
        }
    }
}
