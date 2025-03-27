using Application.Features.AdditionalServices.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.AdditionalServices.Commands.Update;

public class UpdateAdditionalServiceCommand : IRequest<UpdatedAdditionalServiceResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }


    public class
        UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand,
        UpdatedAdditionalServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public UpdateAdditionalServiceCommandHandler(AdditionalServiceBusinessRules additionalServiceBusinessRules, IAdditionalServiceRepository additionalServiceRepository, IMapper mapper)
        {
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedAdditionalServiceResponse> Handle(UpdateAdditionalServiceCommand request,
            CancellationToken cancellationToken)
        {
            var additionalService = await _additionalServiceRepository.GetAsync(predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken);

            additionalService = _mapper.Map(request, additionalService);

            var response = _mapper.Map<UpdatedAdditionalServiceResponse>(additionalService);
            return response;
        }
    }
    
}