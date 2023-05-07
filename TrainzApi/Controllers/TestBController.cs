using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainzApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Tags("Утилиты для тестирования")]
    public class TestBController: ControllerBase
    {
        [HttpGet("helloworld")]
        public string HelloWorld()
        {
            return "Hello, World !";
        }
    }
}
