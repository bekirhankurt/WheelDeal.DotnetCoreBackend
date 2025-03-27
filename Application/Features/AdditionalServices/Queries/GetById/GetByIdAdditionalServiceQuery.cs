using System.Reflection.Metadata.Ecma335;
using Application.Features.AdditionalServices.Rules;
using Application.Repositories;
using Application.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.AdditionalServices.Queries.GetById;

public class GetByIdAdditionalServiceQuery :IRequest<GetByIdAdditionalServiceResponse>
{
    public Guid Id { get; set; }

    public class
        GetByIdAdditionalServiceQueryHandler : IRequestHandler<GetByIdAdditionalServiceQuery,
        GetByIdAdditionalServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public GetByIdAdditionalServiceQueryHandler(AdditionalServiceBusinessRules additionalServiceBusinessRules, IAdditionalServiceRepository additionalServiceRepository, IMapper mapper)
        {
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdAdditionalServiceResponse> Handle(GetByIdAdditionalServiceQuery request,
            CancellationToken cancellationToken)
        {
            var additionalService = await _additionalServiceRepository.GetAsync(predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken);

            var response = _mapper.Map<GetByIdAdditionalServiceResponse>(additionalService);
            return response;
        }
    }
}