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
    public class AdvertVisitRepository : IAdvertVisitRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AdvertContext _advertContext;
        public AdvertVisitRepository(IConfiguration configuration, AdvertContext advertContext)
        {
            this._configuration = configuration;
            _advertContext = advertContext;
        }

        public async Task AddAsync(AdvertVisitHistory entity)
        {
            var sql = @"INSERT INTO AdvertVisitHistory (AdvertId, IPAddress, VisitDate) VALUES (@AdvertId, @IPAddress, @VisitDate)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM AdvertVisitHistory WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result > 0;
            }
        }

        public async Task<IEnumerable<AdvertVisitHistory>> GetAllAsync()
        {
            var sql = "SELECT * FROM AdvertVisitHistory";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AdvertVisitHistory>(sql);
                return result.ToList();
            }
        }

        public async Task<AdvertVisitHistory> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM AdvertVisitHistory WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AdvertVisitHistory>(sql, new { Id = id });
                return result;
            }
        }

        public async Task UpdateAsync(AdvertVisitHistory entity)
        {
            var sql = @"UPDATE Advert SET AdvertId = @AdvertId, IPAddress = @IPAddress, VisitDate = @VisitDate WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServer")))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<int> AllCount()
        {
            return _advertContext.AdvertVisitHistory.Count();
        }
    }
}
