using ExceleTech.Application.Responses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Enums;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace ExceleTech.Application.Commands.AccountCommands.SetRoleCommand
{
    public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public SetRoleCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(SetRoleCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdAsync(request.SetRoleDTO.UserId);

            BaseResponse response = new BaseResponse();

            if (user is null) 
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError("User not found");
                return response;
            }

            user.SetRole(Enum.Parse<UserRole>(request.SetRoleDTO.RoleName));
            _unitOfWork.SaveChangesAsync();

            return response;
        }
    }
}
