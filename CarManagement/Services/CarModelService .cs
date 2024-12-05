using CarManagement.Models;
using CarManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarManagement.Services
{
    public interface ICarModelService
    {
        Task<List<CarModel>> GetAllCarModelsAsync();
        Task<int> AddCarModelAsync(CarModel carModel);
        Task<int> UpdateCarModelAsync(CarModel carModel);
        Task<int> DeleteCarModelAsync(int id);
    }

    public class CarModelService : ICarModelService
    {
        private readonly ICarModelRepository _carModelRepository;

        public CarModelService(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        public async Task<List<CarModel>> GetAllCarModelsAsync()
        {
            return await _carModelRepository.GetAllCarModelsAsync();
        }

        public async Task<int> AddCarModelAsync(CarModel carModel)
        {
            return await _carModelRepository.AddCarModelAsync(carModel);
        }

        public async Task<int> UpdateCarModelAsync(CarModel carModel)
        {
            return await _carModelRepository.UpdateCarModelAsync(carModel);
        }

        public async Task<int> DeleteCarModelAsync(int id)
        {
            return await _carModelRepository.DeleteCarModelAsync(id);
        }
    }
}
