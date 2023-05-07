using Microsoft.AspNetCore.Mvc;
using TrainzLib.Models;
using TrainzLib.Repository;
using TrainzApi.Utils;
using Microsoft.AspNetCore.Authorization;

namespace TrainzApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Tags("Crud операции со станциями")]
    public class StationController : BaseCrudController<Station>
    {
        public StationController(ILogger<StationController> logger, ICrudRepository<Station> repository) :
           base(logger, repository)
        {
        }
    }
}
