using CarManagement.Models;
using CarManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarManagement.Services
{
    public interface ISalesmanInfoService
    {
        Task<List<SalesmanInfo>> GetAllSalesmenAsync();
        Task<SalesmanInfo> GetSalesmanByIdAsync(int id);
        Task<int> AddSalesmanAsync(SalesmanInfo salesmanInfo);
        Task<int> UpdateSalesmanAsync(SalesmanInfo salesmanInfo);
        Task<int> DeleteSalesmanAsync(int id);
    }
    public class SalesmanInfoService : ISalesmanInfoService
    {
        private readonly ISalesmanInfoRepository _salesmanInfoRepository;

        public SalesmanInfoService(ISalesmanInfoRepository salesmanInfoRepository)
        {
            _salesmanInfoRepository = salesmanInfoRepository;
        }

        public async Task<List<SalesmanInfo>> GetAllSalesmenAsync()
        {
            return await _salesmanInfoRepository.GetAllSalesmenAsync();
        }

        public async Task<SalesmanInfo> GetSalesmanByIdAsync(int id)
        {
            return await _salesmanInfoRepository.GetSalesmanByIdAsync(id);
        }

        public async Task<int> AddSalesmanAsync(SalesmanInfo salesmanInfo)
        {
            return await _salesmanInfoRepository.AddSalesmanAsync(salesmanInfo);
        }

        public async Task<int> UpdateSalesmanAsync(SalesmanInfo salesmanInfo)
        {
            return await _salesmanInfoRepository.UpdateSalesmanAsync(salesmanInfo);
        }

        public async Task<int> DeleteSalesmanAsync(int id)
        {
            return await _salesmanInfoRepository.DeleteSalesmanAsync(id);
        }
    }
}
