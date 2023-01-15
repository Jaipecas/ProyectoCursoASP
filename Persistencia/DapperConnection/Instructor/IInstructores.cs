
namespace Persistencia.DapperConnection.Instructor
{
    public interface IInstructores
    {
        Task<IList<InstructorModel>> GetAll();
        Task<InstructorModel> GetById(Guid id);
        Task<int> Update(InstructorModel model);
        Task<int> Delete(Guid id);
        Task<int> Create(InstructorModel model);
    }
}
