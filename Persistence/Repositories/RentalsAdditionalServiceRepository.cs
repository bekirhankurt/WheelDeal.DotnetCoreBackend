using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Concrete;

namespace Persistence.Repositories;

public class RentalsAdditionalServiceRepository(BaseDbContext context)
    : EntityFrameworkRepositoryBase<RentalsAdditionalService, Guid, BaseDbContext>(context),
        IRentalsAdditionalServiceRepository;