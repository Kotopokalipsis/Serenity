using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace Application.MedicalCards.Commands;

public record NewMedicalCardCommand : IRequest<IBaseResponse<MedicalCardModel>>
{
    public string Name { get; set; }
};

public class NewMedicalCardHandler : IRequestHandler<NewMedicalCardCommand, IBaseResponse<MedicalCardModel>>
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NewMedicalCardHandler(ITokenHelper tokenHelper, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
    }
    
    public async Task<IBaseResponse<MedicalCardModel>> Handle(NewMedicalCardCommand request, CancellationToken cancellationToken)
    {
        var user = await _tokenHelper.GetUserByRefreshToken();
        
        if (user == null) return ResponseHelper<MedicalCardModel>.GetRefreshTokenErrorResponse();
        
        var medicalCard = _unitOfWork.MedicalCardRepository.Add(new MedicalCard()
        {
            Name = request.Name,
            UserId = user.Id
        });

        var model = _mapper.Map<MedicalCardModel>(medicalCard);

        return new BaseResponse<MedicalCardModel>()
        {
            StatusCode = 201,
            Data = model
        };
    }
}