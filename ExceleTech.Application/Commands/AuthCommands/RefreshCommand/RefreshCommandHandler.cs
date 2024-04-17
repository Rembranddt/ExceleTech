using System.Security.Claims;
using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.TokenResponse;
using ExceleTech.Domain.Interfaces.Repositories;
using ExceleTech.Domain.Interfaces.Services;
using ExceleTech.Domain.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace ExceleTech.Application.Commands.AuthCommands.RefreshCommand
{
    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, BaseResponse<TokenResponse>>
    {
        private readonly IUserRepository _userRepository;

        private readonly ITokenService _tokenService;

        private readonly IOptions<TokenOptions> _options;

        private readonly IUnitOfWork _unitOfWork;

        public RefreshCommandHandler(IUserRepository userRepository, 
        ITokenService tokenService, 
        IOptions<TokenOptions> options,
        IUnitOfWork unitOfWork)

        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _options = options;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<TokenResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<TokenResponse>();

            if (_tokenService.IsValidToken(request.tokenDTO.AccessToken))
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.InternalServerError);
                return response;
            }

            var user = await _userRepository.GetUserByTokenAsync(request.tokenDTO.RefreshToken);

            if(user == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.UserNotFound);
                return response;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);

            var refreshToken = _tokenService.GenerateRefreshToken();

            user.AddNewToken(refreshToken, DateTime.UtcNow.AddDays(_options.Value.RefreshLifeTime));

            await _unitOfWork.SaveChangesAsync();
            
            response.AddData(new TokenResponse
            {
                RefreshToken = refreshToken,
                AccessToken = accessToken
            });

            return response;
        }
    }
}