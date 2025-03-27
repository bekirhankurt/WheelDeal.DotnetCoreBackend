using Domain.Entities;
using Persistence.Repositories.Abstract;

namespace Application.Repositories;

public interface IRentalsAdditionalServiceRepository : IAsyncRepository<RentalsAdditionalService, Guid>, IRepository<RentalsAdditionalService, Guid>
{
    
}