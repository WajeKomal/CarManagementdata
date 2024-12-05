using CarManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("commission-report")]
        public async Task<IActionResult> GetCommissionReport()
        {
            var reports = await _salesService.GenerateCommissionReportAsync();
            return Ok(reports);
        }
    }
}
