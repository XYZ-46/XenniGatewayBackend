using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase { }
}
