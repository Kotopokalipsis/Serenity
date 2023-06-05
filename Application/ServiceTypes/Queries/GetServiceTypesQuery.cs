using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.ServiceTypes.Queries;

public record GetServiceTypesQuery : IRequest<IBaseResponse<IEnumerable<ServiceType>>> {};

public class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, IBaseResponse<IEnumerable<ServiceType>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetServiceTypesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));;
    }

    public async Task<IBaseResponse<IEnumerable<ServiceType>>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
    {
        var types = (await _unitOfWork.ServiceTypeRepository.FindAll()).ToList();
        return new BaseResponse<IEnumerable<ServiceType>>
        {
            StatusCode = types.Any() ? 200 : 202,
            Data = types,
        };
    }
};