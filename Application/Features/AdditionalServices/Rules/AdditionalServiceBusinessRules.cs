using Application.Features.AdditionalServices.Constants;
using Application.Repositories;
using Application.Rules;
using Core.Localization.Abstraction;
using CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.AdditionalServices.Rules;

public abstract class AdditionalServiceBusinessRules(
    IAdditionalServiceRepository additionalServiceRepository,
    ILocalizationService localizationService)
    : BaseBusinessRules
{
    private async Task throwBusinessException(string messageKey)
    {
        var message =
            await localizationService.GetLocalizedAsync(messageKey, AdditionalServicesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AdditionalServiceShouldExistWhenSelected(AdditionalService additionalService)
    {
        if (additionalService == null)
            await throwBusinessException(AdditionalServicesBusinessMessages.AdditionalServiceNotExists);
        
    }

    public async Task AdditionalServiceIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken) =>
        await AdditionalServiceShouldExistWhenSelected(additionalServiceRepository.GetAsync(predicate: a => a.Id == id,
            enableTracking: false, cancellationToken: cancellationToken).Result);
}