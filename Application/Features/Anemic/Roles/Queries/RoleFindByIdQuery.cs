using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Anemic.Roles.Queries
{
    
    public class RoleFindByIdQuery : BaseRequest, IRequest<Result<IdentityRole>>
    {
        public string Id { get; set; }

    }

    public class RoleFindByIdQueryHandler : BaseRequestHandler<RoleFindByIdQuery, Result<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleFindByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Result<IdentityRole>> HandleRequestAsync(RoleFindByIdQuery input, CancellationToken cancellationToken)
        {

            var result = new FluentResults.Result<IdentityRole>();

            var role = await _roleManager.Roles.Where(s => s.Id == input.Id).FirstOrDefaultAsync(cancellationToken);

            return result.WithValue(role).ConvertToDtatResult();

        }
    }
}
