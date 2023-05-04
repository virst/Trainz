using System;
using System.Data.SqlTypes;
using System.IO;
using TrainzLib.Models;
using TrainzLib.Repository;

namespace TrainzLib.Operations
{
    public class TrainzOperator
    {
        class Nums
        {
            public int BaseNum;
            public int AddNum;
            public int Num => BaseNum + AddNum;
        }

        private readonly IVagonInfoRepository _vagonRep;
        private readonly ICrudRepository<Way> _wayRep;

        public TrainzOperator(IVagonInfoRepository vagonRep, ICrudRepository<Way> wayRep)
        {
            _vagonRep = vagonRep;
            _wayRep = wayRep;
        }

        /// <summary>
        /// Операция приема вагонов на предприятие.
        /// </summary>
        /// <param name="vagons">Натурный лист для приема вагонов</param>
        /// <exception cref="TrainzException">Ошибки во входных данные, не возможность выполнить такую операцию приема</exception>
        public void ReceiptVagons(List<VagonInfo> vagons)
        {
            // сортируем, для удобной расстановки по местам
            vagons = vagons.OrderBy(v => v.WayId).ThenBy(v => v.OrderNum).ToList();

            Dictionary<int, Nums> wayNums = new Dictionary<int, Nums>();

            // проверяем возможнсть вставки входных данных
            foreach (var v in vagons)
            {
                if (v.WayId == null)
                    throw new TrainzException(100, "Нельзя ставить на неопределенный путь");

                if (!wayNums.ContainsKey(v.WayId.Value))
                    wayNums[v.WayId.Value] = new Nums() { BaseNum = _vagonRep.WayLasnNum(v.WayId.Value) };

                wayNums[v.WayId.Value].AddNum++;

                if ((v.OrderNum ?? 0) == 0)
                    v.OrderNum = wayNums[v.WayId.Value].Num;
                else if (v.OrderNum != wayNums[v.WayId.Value].Num)
                    throw new TrainzException(101, $"Не возможно выполнить вставку на путь {v.WayId} на позицию {v.OrderNum}");
            }

            // вставляем гарантированно коррекнтые данные
            foreach (var v in vagons)
                _vagonRep.Insert(v);

        }

        /// <summary>
        /// Операция перестановки вагонов внутри станции.
        /// </summary>
        /// <param name="wayId">Целевой путь</param>
        /// <param name="vagonNums">Список нумеров вагонов</param>
        /// <exception cref="TrainzException">Ошибка о невозможности выполнить соответствующую перестановку</exception>
        public void TranspositionVagons(int wayId, List<int> vagonNums)
        {
            var w = _wayRep.GetById(wayId);

            if (w == null)
                throw new TrainzException(200, "Нельзя ставить на не созданный путь");

            int stanId = w.StationId;

            _vagonRep.StartBuffer();
            var vagonsOnWay = _vagonRep.VagonsOnWay(wayId).ToList(); // Все вагоны на целевом пути
            try
            {
                int minNum = _vagonRep.WayFirsNum(wayId);
                int maxNum = _vagonRep.WayLastNum(wayId);

                foreach (var vNum in vagonNums)
                {
                    var vi = _vagonRep.InfoByWagon(vNum);
                    if (vi == null)
                        throw new TrainzException(201, "Нельзя переместить не зарегистированный вагон");
                    if (stanId != _wayRep.GetById(vi.WayId ?? 0)?.StationId)
                        throw new TrainzException(202, "Перестановки возможны только в рамках одной станции");

                    if (_vagonRep.IsFirstOnWay(vNum)) // если в начале - можем переставить в начало
                    {
                        vi.OrderNum = --minNum;
                        vi.WayId = wayId;
                        vagonsOnWay.Insert(0, vi);
                        continue;
                    }

                    if (_vagonRep.IsLastOnWay(vNum)) // если в конце можем переставить в конец
                    {
                        vi.OrderNum = ++maxNum;
                        vi.WayId = wayId;
                        vagonsOnWay.Add(vi);
                        continue;
                    }

                    throw new TrainzException(203, "Перестановки возможны только с начала или конца пути");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _vagonRep.ClearBuffer();
            }

            int num = 0;
            foreach (var v in vagonsOnWay) // перенумеровываем
            {
                v.OrderNum = ++num;
            }

            foreach (var v in vagonsOnWay) // сохраняем
            {
                _vagonRep.Update(v);
            }
        }

        public void DepartureVagons(List<int> vagonNums)
        {
            if (vagonNums.Count == 0) return;
            List<VagonInfo> list = new List<VagonInfo>();

            // Проверяем возможность убития всех требуемых вагонов
            // Собираем все инфы
            foreach (var v in vagonNums)
            {
                var vi = _vagonRep.InfoByWagon(v);
                list.Add(vi);
            }

            if (list.Select(t => t.WayId).Distinct().Count() > 1)
                throw new TrainzException(301, "Отбытие возможно только с 1 пути за раз");

            var wayId = list[0].WayId ?? throw new TrainzException(303, "Попытка отбытия с неопределенного пути");

            list = list.OrderBy(t => t.OrderNum).ToList(); // Сортируем для проверки
            for (int i = 0; i < list.Count; i++)
                if (list[i].OrderNum != i + 1)
                    throw new TrainzException(302, "Вагоны могут убывать только с начала состава");

            foreach (var v in list)
            {
                _vagonRep.DeleteById(v.Id);
            }

            var infoToReOrd = _vagonRep.VagonsOnWay(wayId).OrderBy(t => t.OrderNum);
            int num = 0;
            foreach (var v in infoToReOrd)
            {
                v.OrderNum = ++num;
                _vagonRep.Update(v);
            }

        }
    }
}
