using System.Net;
using System.Security.Claims;
using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.AccountResponses;
using ExceleTech.Domain.Interfaces.Repositories;
using ExceleTech.Domain.Interfaces.Services;
using ExceleTech.Domain.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace ExceleTech.Application.Commands.AccountCommands.LoginAccountCommand
{
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, BaseResponse<LoginResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IOptions<TokenOptions> _options;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashService _hashService;

        public LoginAccountCommandHandler(
            IUserRepository userRepository,
            ITokenService tokenService,
            IOptions<TokenOptions> options,
            IUnitOfWork unitOfWork,
            IHashService hashService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _options = options;
            _unitOfWork = unitOfWork;
            _hashService = hashService;
        }

        public async Task<BaseResponse<LoginResponse>> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByLoginAsync(request.loginAccountDto.Login);

            BaseResponse<LoginResponse> response = new BaseResponse<LoginResponse>();
            

            if (user == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.UserNotFound);
                return response;
            }
            if (_hashService.IsPasswordCorrect(request.loginAccountDto.Password,user.Password) == false) 
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.AddError(ErrorMessage.IncorrectPassword);
                return response;
            }
            var refreshToken = _tokenService.GenerateRefreshToken();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);

            user.AddNewToken(refreshToken, DateTime.UtcNow.AddDays(_options.Value.RefreshLifeTime));

            await _unitOfWork.SaveChangesAsync();

            response.AddData(new LoginResponse
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                AccessToken = accessToken
            });

            return response;
        }
    }
}