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
    [Tags("Crud операции с вагоном")]
    public class VagonController : BaseCrudController<Vagon>
    {
        public VagonController(ILogger<VagonController> logger, ICrudRepository<Vagon> repository) :
            base(logger, repository)
        {
        }
    }
}
