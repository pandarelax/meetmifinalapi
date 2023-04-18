using MediatR;
using meetmifinal.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Features.Queries.User.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        readonly IUserService _userService;

            public GetAllUserQueryHandler(IUserService userService)
            {
                _userService = userService;
            }
    
            public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
            {
                var users = await _userService.GetAllUsersAsync();
                return new() { Users = users };
            }
    }
}
