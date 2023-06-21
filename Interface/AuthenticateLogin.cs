using KCompanyWebApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace KCompanyWebApp.Interface
{
    public class AuthenticateLogin : ILogin
    {
        private readonly MyDBAuth _context;

        public AuthenticateLogin(MyDBAuth context)
        {
            _context = context;
        }
        //async Task<MsUser>
        public MsUser AuthenticateUser(string UserId, string Password)
        {
            var hashPwd = CreateMD5(Password);
            var succeeded = _context.MsUsers.Where(authUser => authUser.UserId == UserId && authUser.Password == hashPwd).FirstOrDefault();
            return succeeded;
        }
        //async Task<IEnumerable<MsUser>>
        public List<MsUser> getUsers()
        {
            return _context.MsUsers.ToList<MsUser>();
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
