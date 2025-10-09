using DataTransferObject.Tenant;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiService.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : BaseApiController
    {
        [HttpGet("v1")]
        public async Task<IActionResult> test1()
        {


            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var data= JsonSerializer.Deserialize<AddTenantReq>(json);

            return Ok(data);
        }
    }
}
