using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.MedicalCards.Queries;

public record GetMedicalCardsQuery : IRequest<IBaseResponse<List<MedicalCardModel>>>
{}

public class GetMedicalCardsQueryHandler : IRequestHandler<GetMedicalCardsQuery, IBaseResponse<List<MedicalCardModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _tokenHelper;

    public GetMedicalCardsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ITokenHelper tokenHelper)
    {
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<IBaseResponse<List<MedicalCardModel>>> Handle(GetMedicalCardsQuery request, CancellationToken ct)
    {
        var user = await _tokenHelper.GetUserByRefreshToken();
        
        if (user == null) return ResponseHelper<List<MedicalCardModel>>.GetRefreshTokenErrorResponse();

        var medicalCards = await _unitOfWork.MedicalCardRepository.FindBy(md => md.UserId == user.Id);

        var medicalCardModels = _mapper.Map<List<MedicalCardModel>>(medicalCards);
        
        return new BaseResponse<List<MedicalCardModel>>
        {
            StatusCode = medicalCardModels.Any() ? 200 : 202,
            Data = medicalCardModels
        };
    }
}