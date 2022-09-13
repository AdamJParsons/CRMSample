using CRMSample.Application.Common.Data.User;

namespace CRMSample.Application.Common.Services
{
    public interface ICurrentUserContext
    {
        Task<IUser> GetCurrentUserContext();

        string GetCurrentUserToken();
    }
}
