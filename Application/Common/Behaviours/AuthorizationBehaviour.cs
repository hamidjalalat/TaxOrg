using FluentValidation;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ViewModels.Menus;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Domain.Constants;
using Application.Common.Interfaces.Repository.Anemic.Base;

namespace Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IAuthenticatedUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(
            IAuthenticatedUserService currentUserService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            
            var controllerName = _httpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString();
            var actionName = _httpContextAccessor.HttpContext?.Request.RouteValues["action"]?.ToString();
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    var authorized = false;

                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            var isInRole = await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }

                    // Must be a member of at least one role in roles
                    if (!authorized)
                    {
                        throw new ForbiddenAccessException("شما اجازه دسترسی به این فرم را ندارید");
                    }
                }

                // Policy-based authorization
                var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                    {
                        var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

                        if (!authorized)
                        {
                            throw new ForbiddenAccessException("شما اجازه دسترسی به این فرم را ندارید");
                        }
                    }
                }

                if (authorizeAttributesWithRoles.Any() == false && authorizeAttributesWithPolicies.Any() == false)
                {
                    var user = await _userManager.FindByIdAsync(_currentUserService.UserId);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Any(s => s.ToLower().Contains(PublicConstants.Admin)))
                    {
                        return await next();
                    }
                    foreach (var item in roles)
                    {
                        var role = await _roleManager.FindByNameAsync(item);
                        var claims = await _roleManager.GetClaimsAsync(role);
                        var inputViewModel = request.GetType().GetProperty("InputViewModel")?.GetValue(request);
                        var menuId = inputViewModel?.GetType().GetProperty("MenuId")?.GetValue(inputViewModel);
                        var c = claims.FirstOrDefault(s => s.Type == menuId?.ToString());
                        if (c != null)
                        {
                            var menuControllerModel = await _unitOfWork.MenuControllers.GetAll
                                .Include(q => q.Controller)
                                .Where(s => s.MenuId == Convert.ToInt32(c.Type) && s.Controller.TitleEn.ToLower() == $"{controllerName}Controller".ToLower())
                                .AsNoTracking()
                                .FirstOrDefaultAsync(cancellationToken);
                          
                            if (menuControllerModel != null)
                            {
                                var isActionModel = await _unitOfWork.MenuControllerActions.GetAll
                                    .Include(s => s.ActionMethod)
                                    .AsNoTracking()
                                    .AnyAsync(s => s.MenuControllerId == menuControllerModel.MenuControllerId && s.ActionMethod.TitleEn.ToLower() == actionName.ToLower(), cancellationToken);
                                if (isActionModel)
                                {
                                    return await next();
                                }
                            }
                        }

                    }

                    throw new ForbiddenAccessException("شما اجازه دسترسی به این فرم را ندارید");
                }
            }

            // User is authorized / authorization not required
            return await next();
        }


    }
}