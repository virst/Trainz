using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainzLib;
using TrainzLib.Models;
using TrainzLib.Operations;
using TrainzLib.Repository;
using TrainzMock;

namespace TrainzTest
{
    [TestClass]
    public class TestException
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private ICrudRepository<Station> _stationRepository;
        private ICrudRepository<Way> _wayRepository;
        private TrainzOperator _trainzOperator;
        private IVagonInfoRepository _vagonInfoRepository;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        [TestInitialize]
        public void InitializeTest()
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

            #region ReceiptVagons Base
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

            #endregion
        }

        [TestCleanup]
        public void CleanupTest()
        {
            _stationRepository.ClearAll();
            _wayRepository.ClearAll();           
            _vagonInfoRepository.ClearAll();
        }

        [TestMethod("Операция приема вагонов на предприятие. Неверный путь.")]
        public void ReceiptWrongWay()
        {
            int errorCode = 0;
            try
            {
                VagonInfo vi = new VagonInfo()
                {
                    VagonId = 1000,
                    WayId = 50
                };

                _trainzOperator.ReceiptVagons(new List<VagonInfo> { vi });
            }
            catch (TrainzException ex)
            {
                errorCode = ex.ErrorCode;
            }

            Assert.AreEqual(errorCode, 100);
        }


        [TestMethod("Операция приема вагонов на предприятие. Неверное положение.")]
        public void ReceiptWrongPlace()
        {
            int errorCode = 0;
            try
            {
                VagonInfo vi = new VagonInfo()
                {
                    VagonId = 1000,
                    WayId = 1,
                    OrderNum = 2,
                };

                _trainzOperator.ReceiptVagons(new List<VagonInfo> { vi });
            }
            catch (TrainzException ex)
            {
                errorCode = ex.ErrorCode;
            }

            Assert.AreEqual(errorCode, 101);
        }

        [TestMethod("Операция убытия вагонов на сеть РЖД. Неверное начальное положение.")]
        public void DepartureVagonsWrongPlace()
        {
            int errorCode = 0;
            try
            {
                _trainzOperator.DepartureVagons(new List<int> { 3 });
            }
            catch (TrainzException ex)
            {
                errorCode = ex.ErrorCode;
            }

            Assert.AreEqual(errorCode, 302);
        }

        [TestMethod("Операция перестановки вагонов внутри станции. Неверное начальное положение.")]
        public void TranspositionVagonsWrongPlace()
        {
            int errorCode = 0;
            try
            {
                _trainzOperator.TranspositionVagons(1, new List<int> { 5 });
            }
            catch (TrainzException ex)
            {
                errorCode = ex.ErrorCode;
            }

            Assert.AreEqual(errorCode, 203);
        }
    }
}
