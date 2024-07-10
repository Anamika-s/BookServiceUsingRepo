using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookServiceUsingRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public int Divide()
        {
            _logger.LogWarning("warning");
            _logger.LogCritical("critical");
            _logger.LogInformation("info");
            int res = 0;
            try
            {
                int x = 10;
                int y = 0;
                res = x / y;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new DivideByZeroException(ex.Message);
            }
            return res;
        }

    }
}
