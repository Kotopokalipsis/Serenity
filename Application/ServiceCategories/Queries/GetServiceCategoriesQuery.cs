using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.ServiceTypes.Queries;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.ServiceCategories.Queries;

public record GetServiceCategoriesQuery : IRequest<IBaseResponse<IEnumerable<ServiceCategory>>> {};

public class GetServiceTypesQueryHandler : IRequestHandler<GetServiceCategoriesQuery, IBaseResponse<IEnumerable<ServiceCategory>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetServiceTypesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));;
    }

    public async Task<IBaseResponse<IEnumerable<ServiceCategory>>> Handle(GetServiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = (await _unitOfWork.ServiceCategoryRepository.FindAll()).ToList();
        return new BaseResponse<IEnumerable<ServiceCategory>>
        {
            StatusCode = categories.Any() ? 200 : 202,
            Data = categories,
        };
    }
};