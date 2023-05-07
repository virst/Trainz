using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainzLib.Models;
using TrainzLib.Operations;
using TrainzLib.Repository;

namespace TrainzApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Tags("Операции над вагонами")]
    public class OperationsController : ControllerBase
    {

        private readonly TrainzOperator _trainzOperator;

        public OperationsController(TrainzOperator trainzOperator)
        {
            _trainzOperator = trainzOperator;
        }

        [HttpPost("ReceiptVagons")]
        public bool ReceiptVagons(List<VagonInfo> list)
        {
            _trainzOperator.ReceiptVagons(list);
            return true;
        }

        [HttpPost("TranspositionVagons")]
        public bool TranspositionVagons(int wayId, List<int> vagonNums)
        {
            _trainzOperator.TranspositionVagons(wayId, vagonNums);
            return true;
        }

        [HttpPost("DepartureVagons")]
        public bool DepartureVagons(List<int> vagonNums)
        {
            _trainzOperator.DepartureVagons(vagonNums);
            return true;
        }
    }
}
