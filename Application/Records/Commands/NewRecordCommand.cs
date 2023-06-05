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

namespace Application.Records.Commands;

public record NewRecordCommand : IRequest<IBaseResponse<RecordModel>>
{
    public string Name { get; set; }
    public DateTime VisitedAt { get; set; }
    public string Content { get; set; }
    public long ServiceTypeId { get; set; }
    public long ServiceCategoryId { get; set; }
    public long MedicalCardId { get; set; }
};

public class NewRecordHandler : IRequestHandler<NewRecordCommand, IBaseResponse<RecordModel>>
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NewRecordHandler(ITokenHelper tokenHelper, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
    }
    
    public async Task<IBaseResponse<RecordModel>> Handle(NewRecordCommand request, CancellationToken cancellationToken)
    {
        var user = await _tokenHelper.GetUserByRefreshToken();
        
        if (user == null) return ResponseHelper<RecordModel>.GetRefreshTokenErrorResponse();

        var isMedicalCardExist = await _unitOfWork
            .MedicalCardRepository
                .Any(medicalCard => medicalCard.Id == request.MedicalCardId && medicalCard.UserId == user.Id);
        
        if (!isMedicalCardExist) return ResponseHelper<RecordModel>.GetBadRequestErrorResponse();

        var isServiceTypeExist = await _unitOfWork.ServiceTypeRepository.Any(type => type.Id == request.ServiceTypeId);
        var isServiceCategoryExist = await _unitOfWork.ServiceCategoryRepository.Any(category => category.Id == request.ServiceCategoryId);
        
        if (!isServiceCategoryExist || !isServiceTypeExist) return ResponseHelper<RecordModel>.GetBadRequestErrorResponse();

        var newRecord = _unitOfWork.RecordRepository.Add(new Record()
        {
            Name = request.Name,
            Content = request.Content,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            MedicalCardId = request.MedicalCardId,
            VisitedAt = request.VisitedAt.ToUniversalTime(),
            ServiceTypeId = request.ServiceTypeId,
            ServiceCategoryId = request.ServiceCategoryId,
        });

        await _unitOfWork.Commit(cancellationToken);
        
        var model = _mapper.Map<RecordModel>(newRecord);

        return new BaseResponse<RecordModel>()
        {
            StatusCode = 201,
            Data = model
        };
    }
}