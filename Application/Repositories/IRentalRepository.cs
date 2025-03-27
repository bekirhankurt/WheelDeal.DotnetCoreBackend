using Domain.Entities;
using Persistence.Repositories.Abstract;

namespace Application.Repositories;

public interface IRentalRepository : IAsyncRepository<Rental, Guid> , IRepository<Rental, Guid>
{
    
}