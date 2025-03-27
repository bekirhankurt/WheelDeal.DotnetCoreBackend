using Domain.Entities;
using Persistence.Repositories.Abstract;

namespace Application.Repositories;

public interface IRentalBranchRepository : IAsyncRepository<RentalBranch, Guid>, IRepository<RentalBranch, Guid>
{
    
}