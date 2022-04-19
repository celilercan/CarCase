using CarCaseTest.Domain.Entities;
using CarCaseTest.Infrastructure.Persistence;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Infrastructure.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AdvertContext _advertContext;
        public AdvertRepository(IConfiguration configuration, AdvertContext advertContext)
        {
            this._configuration = configuration;
            _advertContext = advertContext;
        }

        public async Task AddAsync(Advert entity)
        {
            var sql = @"INSERT INTO Advert (MemberId, CityId, CityName, TownId, TownName, ModelId, ModelName, Year, Price, Title, Date, 
                                            CategoryId, Category, Km, Color, Gear, Fuel, FirstPhoto, SecondPhoto, UserInfo, UserPhone, [Text]) 
                        VALUES (@MemberId, @CityId, @CityName, @TownId, @TownName, @ModelId, @ModelName, @Year, @Price, @Title, @Date, 
                                            @CategoryId, @Category, @Km, @Color, @Gear, @Fuel, @FirstPhoto, @SecondPhoto, @UserInfo, @UserPhone, @Text)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Advert WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result > 0;
            }
        }

        public async Task<Advert> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Advert WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<Advert>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IEnumerable<Advert>> GetAllAsync()
        {
            var sql = "SELECT * FROM Advert";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Advert>(sql);
                return result.ToList();
            }
        }

        public async Task UpdateAsync(Advert entity)
        {
            var sql = @"UPDATE Advert SET MemberId = @MemberId, CityId = @CityId, CityName = @CityName, TownId = @TownId, TownName = @TownName, ModelId = @ModelId, ModelName = @ModelName, 
                                            Year = @Year, Price = @Price, Title = @Title, Date = @Date, CategoryId = @CategoryId, Category = @Category, Km = @Km, Color = @Color, Gear = @Gear,
                                            Fuel = @Fuel, FirstPhoto = @FirstPhoto, SecondPhoto = @SecondPhoto, UserInfo = @UserInfo, UserPhone = @UserPhone, [Text] = @Text WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<int> AllCount()
        {
            return _advertContext.Advert.Count();
        }
    }
}
