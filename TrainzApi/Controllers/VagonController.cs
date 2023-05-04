using Microsoft.AspNetCore.Mvc;
using TrainzLib.Models;
using TrainzLib.Repository;
using TrainzApi.Utils;

namespace TrainzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Crud операции с вагоном")]
    public class VagonController : BaseCrudController<Vagon>
    {
        public VagonController(ILogger<VagonController> logger, ICrudRepository<Vagon> repository) :
            base(logger, repository)
        {
        }
    }
}
