using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Records.Queries;

public record GetRecordsQuery : IRequest<IBaseResponse<List<RecordModel>>>
{
    public long MedicalCardId { get; set; }
}

public class GetRecordsQueryHandler : IRequestHandler<GetRecordsQuery, IBaseResponse<List<RecordModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _tokenHelper;

    public GetRecordsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ITokenHelper tokenHelper)
    {
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<IBaseResponse<List<RecordModel>>> Handle(GetRecordsQuery request, CancellationToken ct)
    {
        var user = await _tokenHelper.GetUserByRefreshToken();
        
        if (user == null) return ResponseHelper<List<RecordModel>>.GetRefreshTokenErrorResponse();

        var records =
            await _unitOfWork.RecordRepository.FindBy(record =>
                record.MedicalCard.Id == request.MedicalCardId && record.MedicalCard.UserId == user.Id);

        var recordModels = _mapper.Map<List<RecordModel>>(records);
        
        return new BaseResponse<List<RecordModel>>
        {
            StatusCode = recordModels.Any() ? 200 : 202,
            Data = recordModels
        };
    }
}