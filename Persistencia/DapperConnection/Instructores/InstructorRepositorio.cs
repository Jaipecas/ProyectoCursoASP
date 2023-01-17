
using Dapper;

namespace Persistencia.DapperConnection.Instructor
{
    public class InstructorRepositorio : IInstructores
    {
        private readonly IFactoryConnection _factoryConnection;

        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public Task<int> Create(InstructorModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> GetAll()
        {
            IEnumerable<InstructorModel> instrucotList;

            var storeProcedure = "usp_Obtener_Instructores";
            try
            {
                var conn = _factoryConnection.GetDbConnection();
                instrucotList = await conn.QueryAsync<InstructorModel>(storeProcedure, null, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos", e);                
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return instrucotList;
        }

        public Task<InstructorModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(InstructorModel model)
        {
            throw new NotImplementedException();
        }
    }
}
