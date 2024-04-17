using AutoMapper;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.AccountResponses;
using ExceleTech.Domain.Interfaces.Services;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;

using MediatR;
using ExceleTech.Application.Services;
using ExceleTech.Application.Resources;
using System.Net;

namespace ExceleTech.Application.Commands.AccountCommands.CreateAccountCommand
{
   
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, BaseResponse<CreateAccountResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IHashService _hashService;

        public CreateAccountCommandHandler(
            IUserRepository userRepository,
            IBasketRepository basketRepository,
            IMapper mapper, 
            IEmailSenderService emailSenderService,
            IUnitOfWork unitOfWork,
            IHashService hashService)
        {
            _userRepository = userRepository;
            _basketRepository = basketRepository;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
            _unitOfWork = unitOfWork;
            _hashService = hashService; 
        }
        public async Task<BaseResponse<CreateAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                
                User User = await _userRepository.GetUserByLoginAsync(request.CreateAccountDTO.Name);
                var response = new BaseResponse<CreateAccountResponse>();
                if (User != null)
                {
                    response.AddError(ErrorMessage.UserIsAlreadyExists);
                    response.StatusCode = HttpStatusCode.Conflict;
                }
                string passwordHash = _hashService.HashPassword(request.CreateAccountDTO.password);
                var user = User.Create(request.CreateAccountDTO.Name,
                                       request.CreateAccountDTO.email,
                                       passwordHash);

                
                var result = _userRepository.Add(user);
                var basket = Basket.Create(result.Id);
                _basketRepository.AddBasket(basket);
                await _unitOfWork.SaveChangesAsync();
                await _emailSenderService.SendAsync(request.CreateAccountDTO.email, new Random().Next(0, 1000000).ToString("D6"), result.Id);
                _unitOfWork.CommitTransaction();
                


                var data = new CreateAccountResponse
                {
                    EmailAddress = result.Email,
                    UserId = result.Id
                };
                response.AddData(data);

                return response;
            }
            
            catch(Exception) 
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }
    }
}