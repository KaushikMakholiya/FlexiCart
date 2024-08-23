using FlexiCart.Common.Interfaces;
using System.Data;
using Dapper;

namespace FlexiCart.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;
        private readonly string _getProcedure;
        private readonly string _getAllProcedure;
        private readonly string _addProcedure;
        private readonly string _updateProcedure;
        private readonly string _deleteProcedure;

        public Repository(IDbConnection dbConnection, string getProcedure, string getAllProcedure,
                          string addProcedure, string updateProcedure, string deleteProcedure)
        {
            _dbConnection = dbConnection;
            _getProcedure = getProcedure;
            _getAllProcedure = getAllProcedure;
            _addProcedure = addProcedure;
            _updateProcedure = updateProcedure;
            _deleteProcedure = deleteProcedure;
        }

        public async Task<T> GetAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await _dbConnection.QueryFirstOrDefaultAsync<T>(
                _getProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<T>(
                _getAllProcedure,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> AddAsync(T entity)
        {
            var parameters = MapEntityToParameters(entity);
            return await _dbConnection.ExecuteAsync(
                _addProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var parameters = MapEntityToParameters(entity);
            return await _dbConnection.ExecuteAsync(
                _updateProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> DeleteAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await _dbConnection.ExecuteAsync(
                _deleteProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        private DynamicParameters MapEntityToParameters(T entity)
        {
            var parameters = new DynamicParameters();
            // Implement mapping of entity properties to parameters here
            return parameters;
        }
    }
}
