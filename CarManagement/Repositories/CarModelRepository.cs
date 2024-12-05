using CarManagement.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CarManagement.Repositories
{
    public interface ICarModelRepository
    {
        Task<List<CarModel>> GetAllCarModelsAsync();
        Task<int> AddCarModelAsync(CarModel carModel);
        Task<int> UpdateCarModelAsync(CarModel carModel);
        Task<int> DeleteCarModelAsync(int id);
    }

    public class CarModelRepository : ICarModelRepository
    {
        private readonly string _connectionString;

        public CarModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<CarModel>> GetAllCarModelsAsync()
        {
            var carModels = new List<CarModel>();
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM CarModel ORDER BY DateOfManufacturing DESC, SortOrder";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            carModels.Add(new CarModel
                            {
                                Id = reader.GetInt32(0),
                                Brand = reader.GetString(1),
                                Class = reader.GetString(2),
                                ModelName = reader.GetString(3),
                                ModelCode = reader.GetString(4),
                                Description = reader.GetString(5),
                                Features = reader.GetString(6),
                                Price = reader.GetDecimal(7),
                                DateOfManufacturing = reader.GetDateTime(8),
                                Active = reader.GetBoolean(9),
                                SortOrder = reader.GetInt32(10),
                                ModelImage = reader.IsDBNull(11) ? null : reader.GetString(11)
                            });
                        }
                    }
                }
            }
            return carModels;
        }

        public async Task<int> AddCarModelAsync(CarModel carModel)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "INSERT INTO CarModel (Brand, Class, ModelName, ModelCode, Description, Features, Price, DateOfManufacturing, Active, SortOrder) " +
                            "VALUES (@Brand, @Class, @ModelName, @ModelCode, @Description, @Features, @Price, @DateOfManufacturing, @Active, @SortOrder)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Brand", carModel.Brand);
                    cmd.Parameters.AddWithValue("@Class", carModel.Class);
                    cmd.Parameters.AddWithValue("@ModelName", carModel.ModelName);
                    cmd.Parameters.AddWithValue("@ModelCode", carModel.ModelCode);
                    cmd.Parameters.AddWithValue("@Description", carModel.Description);
                    cmd.Parameters.AddWithValue("@Features", carModel.Features);
                    cmd.Parameters.AddWithValue("@Price", carModel.Price);
                    cmd.Parameters.AddWithValue("@DateOfManufacturing", carModel.DateOfManufacturing);
                    cmd.Parameters.AddWithValue("@Active", carModel.Active);
                    cmd.Parameters.AddWithValue("@SortOrder", carModel.SortOrder);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateCarModelAsync(CarModel carModel)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "UPDATE CarModel SET Brand = @Brand, Class = @Class, ModelName = @ModelName, ModelCode = @ModelCode, " +
                            "Description = @Description, Features = @Features, Price = @Price, DateOfManufacturing = @DateOfManufacturing, " +
                            "Active = @Active, SortOrder = @SortOrder WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", carModel.Id);
                    cmd.Parameters.AddWithValue("@Brand", carModel.Brand);
                    cmd.Parameters.AddWithValue("@Class", carModel.Class);
                    cmd.Parameters.AddWithValue("@ModelName", carModel.ModelName);
                    cmd.Parameters.AddWithValue("@ModelCode", carModel.ModelCode);
                    cmd.Parameters.AddWithValue("@Description", carModel.Description);
                    cmd.Parameters.AddWithValue("@Features", carModel.Features);
                    cmd.Parameters.AddWithValue("@Price", carModel.Price);
                    cmd.Parameters.AddWithValue("@DateOfManufacturing", carModel.DateOfManufacturing);
                    cmd.Parameters.AddWithValue("@Active", carModel.Active);
                    cmd.Parameters.AddWithValue("@SortOrder", carModel.SortOrder);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteCarModelAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM CarModel WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
