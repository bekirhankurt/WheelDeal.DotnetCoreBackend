using Application.Repositories;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.AdditionalServices.Queries.GetList;

public class GetListAdditionalServiceQuery : IRequest<GetListResponse<GetListAdditionalServiceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAdditionalServiceQueryHandler : IRequestHandler<GetListAdditionalServiceQuery,
        GetListResponse<GetListAdditionalServiceListItemDto>>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;

        public GetListAdditionalServiceQueryHandler(IMapper mapper, IAdditionalServiceRepository additionalServiceRepository)
        {
            _mapper = mapper;
            _additionalServiceRepository = additionalServiceRepository;
        }

        public async Task<GetListResponse<GetListAdditionalServiceListItemDto>> Handle(
            GetListAdditionalServiceQuery request, CancellationToken cancellationToken)
        {
            var additionalServices = await _additionalServiceRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
            );

            var response = _mapper.Map<GetListResponse<GetListAdditionalServiceListItemDto>>(additionalServices);
            return response;
        }
    }
} 