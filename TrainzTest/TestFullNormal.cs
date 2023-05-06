using TrainzLib.Models;
using TrainzLib.Operations;
using TrainzLib.Repository;
using TrainzMock;
using TrainzLib;

namespace TrainzTest
{
    [TestClass]
    public class TestFullNormal
    {
        private readonly ICrudRepository<Station> _stationRepository;
        private readonly ICrudRepository<Way> _wayRepository;
        private readonly TrainzOperator _trainzOperator;
        private readonly IVagonInfoRepository _vagonInfoRepository;

        public TestFullNormal()
        {
            _stationRepository = new StationCrudMock();
            _wayRepository = new WayCrudMock();
            _vagonInfoRepository = new VagonInfoMock();
            _trainzOperator = new TrainzOperator(_vagonInfoRepository, _wayRepository);

            #region Make Stations and Ways
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
            #endregion
        }

        [TestMethod("Операция приема вагонов на предприятие")]
        public void Test1ReceiptVagons()
        {
            List<VagonInfo> list = new List<VagonInfo>();
            for (int i = 0; i < 9; i++)
            {
                VagonInfo vi = new VagonInfo()
                {
                    VagonId = i + 1,
                    WayId = (i / 3) + 1
                };
                list.Add(vi);
            }
            _trainzOperator.ReceiptVagons(list);

            Assert.AreEqual(_vagonInfoRepository.VagonsOnWay(1).Count(), 3);
            Assert.AreEqual(_vagonInfoRepository.VagonsOnWay(2).Count(), 3);
            Assert.AreEqual(_vagonInfoRepository.VagonsOnWay(3).Count(), 3);
        }

        [TestMethod("Операция перестановки вагонов внутри станции")]
        public void Test2TranspositionVagons()
        {
            _trainzOperator.TranspositionVagons(1, new List<int> { 4 });
            _trainzOperator.TranspositionVagons(2, new List<int> { 7 });
            _trainzOperator.TranspositionVagons(1, new List<int> { 8 });
            _trainzOperator.TranspositionVagons(3, new List<int> { 6, 5 });
            _trainzOperator.TranspositionVagons(2, new List<int> { 3 });

            Assert.AreEqual(_vagonInfoRepository.VagonsOnWay(1).Count(), 4);
            Assert.AreEqual(_vagonInfoRepository.VagonsOnWay(2).Count(), 2);
            Assert.AreEqual(_vagonInfoRepository.VagonsOnWay(3).Count(), 3);

            var text = _vagonInfoRepository.GetAll().ToPrintString();
            Console.WriteLine(text);
        }

        [TestMethod("Операция убытия вагонов на сеть РЖД")]
        public void Test3DepartureVagons()
        {          
            _trainzOperator.DepartureVagons(new List<int> { 8, 4, 1, 2 });
            _trainzOperator.DepartureVagons(new List<int> { 7, 3 });
            _trainzOperator.DepartureVagons(new List<int> { 9, 6, 5 });

            Assert.AreEqual(_vagonInfoRepository.GetAll().Count(), 0);
        }
    }
}
