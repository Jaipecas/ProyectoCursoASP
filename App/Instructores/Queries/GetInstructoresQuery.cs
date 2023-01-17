
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace App.Instructores.Queries
{
    public class GetInstructoresQuery : IRequest<List<InstructorModel>> { }

    public class GetInstructorsQueryHandler : IRequestHandler<GetInstructoresQuery, List<InstructorModel>>
    {
        private readonly IInstructores _instructores;

        public GetInstructorsQueryHandler(IInstructores instructores)
        {
            _instructores = instructores;
        }

        public async Task<List<InstructorModel>> Handle(GetInstructoresQuery request, CancellationToken cancellationToken)
        {
            return (List<InstructorModel>)await _instructores.GetAll();
        }
    }
}
