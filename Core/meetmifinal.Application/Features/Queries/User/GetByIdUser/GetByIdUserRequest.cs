using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Features.Queries.User.GetByIdUser
{
    public class GetByIdUserRequest : IRequest<GetByIdUserResponse>
    {
        public Guid Id { get; set; }
    }
}
