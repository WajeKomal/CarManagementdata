using CarManagement.Models;
using CarManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarManagement.Services
{
    public interface ISalesService
    {
        Task<List<CommissionReportModel>> GenerateCommissionReportAsync();
    }
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<List<CommissionReportModel>> GenerateCommissionReportAsync()
        {
            var salesmen = await _salesRepository.GetSalesmenAsync();
            var sales = await _salesRepository.GetSalesAsync();

            var reports = new List<CommissionReportModel>();

            foreach (var salesman in salesmen)
            {
                decimal totalCommission = 0;
                string details = "";

                foreach (var sale in sales)
                {
                    if (sale.SalesmanName == salesman.SalesmanName)
                    {
                        decimal fixedCommission = GetFixedCommission(sale.Brand);
                        decimal classCommission = GetClassCommission(sale.Class, sale.Brand);

                        decimal commissionForThisSale = fixedCommission + (classCommission / 100) * sale.CarCount;
                        totalCommission += commissionForThisSale;

                        details += $"{sale.CarCount} cars sold for {sale.Brand} in {sale.Class} class. Fixed: ${fixedCommission}, Class: {classCommission}%.\n";
                    }
                }

                if (salesman.LastYearSales > 500000)
                {
                    decimal bonus = totalCommission * 0.02m;
                    totalCommission += bonus;
                    details += $"Additional 2% bonus applied. Bonus: ${bonus:F2}.\n";
                }

                reports.Add(new CommissionReportModel
                {
                    SalesmanName = salesman.SalesmanName,
                    TotalCommission = totalCommission,
                    Details = details
                });
            }

            return reports;
        }

        private decimal GetFixedCommission(string brand)
        {
            return brand switch
            {
                "Audi" => 800,
                "Jaguar" => 750,
                "Land Rover" => 850,
                "Renault" => 400,
                _ => 0
            };
        }

        private decimal GetClassCommission(string carClass, string brand)
        {
            return (carClass, brand) switch
            {
                ("A", "Audi") => 8,
                ("A", "Jaguar") => 6,
                ("A", "Land Rover") => 7,
                ("A", "Renault") => 5,

                ("B", "Audi") => 6,
                ("B", "Jaguar") => 5,
                ("B", "Land Rover") => 5,
                ("B", "Renault") => 3,

                ("C", "Audi") => 4,
                ("C", "Jaguar") => 3,
                ("C", "Land Rover") => 4,
                ("C", "Renault") => 2,

                _ => 0
            };
        }
    }
}
