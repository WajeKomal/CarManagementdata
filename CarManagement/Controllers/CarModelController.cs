using Microsoft.AspNetCore.Mvc;
using CarManagement.Services;
using CarManagement.Models;
using System.Threading.Tasks;

namespace CarManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelService _carModelService;

        public CarModelController(ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCarModels()
        {
            var models = await _carModelService.GetAllCarModelsAsync();
            return Ok(models);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCarModel([FromBody] CarModel model)
        {
            if (model == null)
                return BadRequest("Model is required.");

            var result = await _carModelService.AddCarModelAsync(model);
            return Ok(new { message = "Car model added successfully." });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCarModel(int id, [FromBody] CarModel model)
        {
            if (model == null || id != model.Id)
                return BadRequest("Invalid model data.");

            var result = await _carModelService.UpdateCarModelAsync(model);
            return Ok(new { message = "Car model updated successfully." });
        }

        // Delete a car model
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            await _carModelService.DeleteCarModelAsync(id);
            return Ok(new { message = "Car model deleted successfully." });
        }
    }
}
