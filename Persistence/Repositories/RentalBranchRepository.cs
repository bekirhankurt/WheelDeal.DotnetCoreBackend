using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Concrete;

namespace Persistence.Repositories;

public class RentalBranchRepository(BaseDbContext context)
    : EntityFrameworkRepositoryBase<RentalBranch, Guid, BaseDbContext>(context), IRentalBranchRepository;