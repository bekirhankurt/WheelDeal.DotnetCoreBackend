using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Concrete;

namespace Persistence.Repositories;

public class RentalRepository(BaseDbContext context)
    : EntityFrameworkRepositoryBase<Rental, Guid, BaseDbContext>(context), IRentalRepository;