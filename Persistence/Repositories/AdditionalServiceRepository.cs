using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Concrete;

namespace Persistence.Repositories;

public class AdditionalServiceRepository(BaseDbContext context)
    : EntityFrameworkRepositoryBase<AdditionalService, Guid, BaseDbContext>(context), IAdditionalServiceRepository;