using CRMSample.Application.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace CRMSample.Infrastructure.Common.Services
{
    //public class CurrentUserContext : ICurrentUserContext
    //{
    //    private readonly UserManager<ConduitUser> _userManager;
    //    private readonly IHttpContextAccessor _contextAccessor;

    //    public CurrentUserContext(IUserService, IHttpContextAccessor contextAccessor)
    //    {
    //        _userManager = userManager;
    //        _contextAccessor = contextAccessor;
    //    }

    //    public async Task<ConduitUser> GetCurrentUserContext()
    //    {
    //        var currentHttpContext = _contextAccessor.HttpContext;
    //        var currentUser = await _userManager.GetUserAsync(currentHttpContext?.User);

    //        if (currentUser == null)
    //        {
    //            throw new ConduitApiException("User was not found", HttpStatusCode.BadRequest);
    //        }

    //        return await _userManager.GetUserAsync(currentHttpContext?.User);
    //    }

    //    public string GetCurrentUserToken()
    //    {
    //        string token;
    //        var authorizationHeader = _contextAccessor.HttpContext.Request.Headers?["Authorization"];
    //        if (authorizationHeader.HasValue && authorizationHeader.ToString().StartsWith("Token ", StringComparison.OrdinalIgnoreCase))
    //        {
    //            token = authorizationHeader.ToString().Split(' ')[1];
    //        }
    //        else
    //        {
    //            throw new ConduitApiException($"Invalid token for user [{_contextAccessor.HttpContext.User?.Identity?.Name}]", HttpStatusCode.BadRequest);
    //        }

    //        return token;
    //    }
    //}
}
