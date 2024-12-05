using CarManagement.Models;
using CarManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesmanInfoController : ControllerBase
    {
        private readonly ISalesmanInfoService _salesmanInfoService;

        public SalesmanInfoController(ISalesmanInfoService salesmanInfoService)
        {
            _salesmanInfoService = salesmanInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesmen()
        {
            var salesmen = await _salesmanInfoService.GetAllSalesmenAsync();
            return Ok(salesmen);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesmanById(int id)
        {
            var salesman = await _salesmanInfoService.GetSalesmanByIdAsync(id);
            if (salesman == null)
                return NotFound();

            return Ok(salesman);
        }

        [HttpPost]
        public async Task<IActionResult> AddSalesman(SalesmanInfo salesmanInfo)
        {
            var result = await _salesmanInfoService.AddSalesmanAsync(salesmanInfo);
            if (result > 0)
                return Ok(new { Message = "Salesman added successfully!" });

            return StatusCode(500, "Error adding salesman.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesman(int id, SalesmanInfo salesmanInfo)
        {
            if (id != salesmanInfo.Id)
                return BadRequest("ID mismatch.");

            var result = await _salesmanInfoService.UpdateSalesmanAsync(salesmanInfo);
            if (result > 0)
                return Ok(new { Message = "Salesman updated successfully!" });

            return StatusCode(500, "Error updating salesman.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesman(int id)
        {
            var result = await _salesmanInfoService.DeleteSalesmanAsync(id);
            if (result > 0)
                return Ok(new { Message = "Salesman deleted successfully!" });

            return StatusCode(500, "Error deleting salesman.");
        }
    }
}
