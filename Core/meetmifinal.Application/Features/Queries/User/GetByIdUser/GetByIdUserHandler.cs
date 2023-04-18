using MediatR;
using meetmifinal.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Features.Queries.User.GetByIdUser
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserRequest, GetByIdUserResponse>
    {
        readonly IUserService _userService;
        public GetByIdUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<GetByIdUserResponse> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.Id);
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenEndDate = user.RefreshTokenEndDate,
                PhotoUrl = user.PhotoUrl,
                Meetings = user.Meetings
            };
        }
    }
}
