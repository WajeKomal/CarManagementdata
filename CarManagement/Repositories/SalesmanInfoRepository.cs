using CarManagement.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CarManagement.Repositories
{
    public interface ISalesmanInfoRepository
    {
        Task<List<SalesmanInfo>> GetAllSalesmenAsync();
        Task<SalesmanInfo> GetSalesmanByIdAsync(int id);
        Task<int> AddSalesmanAsync(SalesmanInfo salesmanInfo);
        Task<int> UpdateSalesmanAsync(SalesmanInfo salesmanInfo);
        Task<int> DeleteSalesmanAsync(int id);
    }
    public class SalesmanInfoRepository : ISalesmanInfoRepository
    {
        private readonly string _connectionString;

        public SalesmanInfoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<SalesmanInfo>> GetAllSalesmenAsync()
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

        public async Task<SalesmanInfo> GetSalesmanByIdAsync(int id)
        {
            SalesmanInfo salesman = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM SalesmanInfo WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            salesman = new SalesmanInfo
                            {
                                Id = reader.GetInt32(0),
                                SalesmanName = reader.GetString(1),
                                LastYearSales = reader.GetDecimal(2)
                            };
                        }
                    }
                }
            }
            return salesman;
        }

        public async Task<int> AddSalesmanAsync(SalesmanInfo salesmanInfo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "INSERT INTO SalesmanInfo (SalesmanName, LastYearSales) VALUES (@SalesmanName, @LastYearSales)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SalesmanName", salesmanInfo.SalesmanName);
                    cmd.Parameters.AddWithValue("@LastYearSales", salesmanInfo.LastYearSales);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateSalesmanAsync(SalesmanInfo salesmanInfo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "UPDATE SalesmanInfo SET SalesmanName = @SalesmanName, LastYearSales = @LastYearSales WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", salesmanInfo.Id);
                    cmd.Parameters.AddWithValue("@SalesmanName", salesmanInfo.SalesmanName);
                    cmd.Parameters.AddWithValue("@LastYearSales", salesmanInfo.LastYearSales);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteSalesmanAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM SalesmanInfo WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
