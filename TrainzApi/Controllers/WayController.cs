using Microsoft.AspNetCore.Mvc;
using TrainzLib.Models;
using TrainzLib.Repository;
using TrainzApi.Utils;

namespace TrainzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Crud операции с путями")]
    public class WayController : BaseCrudController<Way>
    {
        public WayController(ILogger<WayController> logger, ICrudRepository<Way> repository) :
           base(logger, repository)
        {
        }
    }
}
