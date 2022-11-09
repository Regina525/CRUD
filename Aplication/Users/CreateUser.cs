using Aplication.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplication.Users;

public class CreateUser
{
    public class CreateUserCommand:IRequest<UserDto>
    {
      //Por default Identity Obrigada a colocar certos atributos:
      public string Email { get; set; }
      public string UserName { get; set; }
      public string PassWord { get; set; }
      public string PhoneNumber { get; set; }
      //Atributo que esta na class User
      public string FullName { get; set; }
      
    }

    public class CreateUserHandler:IRequestHandler<CreateUserCommand,UserDto>
    {
        private UserManager<User> _userManager;
        private IMapper _mapper;
        public CreateUserHandler(UserManager<User> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;

        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.PassWord);
            if (result.Succeeded)
            {
                return _mapper.Map<UserDto>(user);

            }

            throw new Exception(result.Errors.ToString());
        }
    }

  
}