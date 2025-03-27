using Domain.Entities;
using Persistence.Repositories.Abstract;

namespace Application.Repositories;

public interface IAdditionalServiceRepository : IAsyncRepository<AdditionalService, Guid>,
    IRepository<AdditionalService, Guid>
{
}