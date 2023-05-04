using Microsoft.AspNetCore.Mvc;
using TrainzLib.Models;
using TrainzLib.Repository;
using TrainzApi.Utils;

namespace TrainzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Crud операции с типами груза")]
    public class GruzController : BaseCrudController<GruzType>
    {
        public GruzController(ILogger<GruzController> logger, ICrudRepository<GruzType> repository) :
           base(logger, repository)
        {
        }
    }
}
