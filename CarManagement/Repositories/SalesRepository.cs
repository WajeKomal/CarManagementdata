using CarManagement.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CarManagement.Repositories
{
    public interface ISalesRepository
    {
        Task<List<SalesmanInfo>> GetSalesmenAsync();
        Task<List<Sales>> GetSalesAsync();
    }
    public class SalesRepository : ISalesRepository
    {
        private readonly string _connectionString;

        public SalesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<SalesmanInfo>> GetSalesmenAsync()
        {
            var salesmen = new List<SalesmanInfo>();

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM SalesmanInfo";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            salesmen.Add(new SalesmanInfo
                            {
                                Id = reader.GetInt32(0),
                                SalesmanName = reader.GetString(1),
                                LastYearSales = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }

            return salesmen;
        }

        public async Task<List<Sales>> GetSalesAsync()
        {
            var sales = new List<Sales>();

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM Sales";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sales.Add(new Sales
                            {
                                Id = reader.GetInt32(0),
                                SalesmanName = reader.GetString(1),
                                Class = reader.GetString(2),
                                Brand = reader.GetString(3),
                                CarCount = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return sales;
        }
    }
}
