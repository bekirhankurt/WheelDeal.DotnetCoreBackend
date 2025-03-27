using Application.Features.AdditionalServices.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.AdditionalServices.Commands.Delete;

public class DeleteAdditionalServiceCommand : IRequest<DeletedAdditionalServiceResponse>
{
    public Guid Id { get; set; }

    public class
        DeleteAdditionalServiceCommandHandler : IRequestHandler<DeleteAdditionalServiceCommand,
        DeletedAdditionalServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public DeleteAdditionalServiceCommandHandler(AdditionalServiceBusinessRules additionalServiceBusinessRules,
            IAdditionalServiceRepository additionalServiceRepository, IMapper mapper)
        {
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
        }

        public async Task<DeletedAdditionalServiceResponse> Handle(DeleteAdditionalServiceCommand request,
            CancellationToken cancellationToken)
        {
            var additionalService = await _additionalServiceRepository.GetAsync(predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken);

            await _additionalServiceBusinessRules.AdditionalServiceShouldExistWhenSelected(additionalService);
            await _additionalServiceRepository.DeleteAsync(additionalService!);

            var response = _mapper.Map<DeletedAdditionalServiceResponse>(additionalService);
            return response;
        }
    }
}