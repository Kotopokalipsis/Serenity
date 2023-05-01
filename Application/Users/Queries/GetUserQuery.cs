using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries;

public record GetUserQuery : IRequest<IBaseResponse<UserModel>>
{
    public Guid UserId { get; init; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IBaseResponse<UserModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<IBaseResponse<UserModel>> Handle(GetUserQuery request, CancellationToken ct)
    {
        var user = await _unitOfWork.UserRepository.GetById(request.UserId);

        if (user == null)
            return new BaseResponse<UserModel>()
            {
                StatusCode = 400,
            };

        var userModel = _mapper.Map<UserModel>(user);
        
        return new BaseResponse<UserModel>
        {
            StatusCode = 201,
            Data = userModel
        };
    }
}