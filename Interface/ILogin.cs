using KCompanyWebApp.Models;

namespace KCompanyWebApp.Interface
{
    public interface ILogin
    {
        List<MsUser> getUsers();
        MsUser AuthenticateUser(string UserId, string Password);
    }
}
