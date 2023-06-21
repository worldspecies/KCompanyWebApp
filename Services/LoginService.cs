using Alachisoft.NCache.Config.Dom;
using KCompanyWebApp.Interface;
using KCompanyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace KCompanyWebApp.Services
{
    public class LoginService : LoginAPI
    {
        static IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        public static string JWTscrt = configuration.GetValue<string>("AppSettings:JWTscrt").ToString(); //random 64bit
        public static MyDBAuth dbAuth = new MyDBAuth();
        public AuthenticateLogin _loginUser = new AuthenticateLogin(dbAuth);

        public LoginServiceModel Authenticate(string uid, string passwd)
        {
            LoginServiceModel sm_login = new LoginServiceModel();
            sm_login.Message = "Login Failed, Please Check User ID / Password";
            sm_login.UserID = uid;
            sm_login.AccessToken = "";

            //initialize
            var User = _loginUser.AuthenticateUser(uid, passwd);
            if (User != null)
            {
                //generate JWT Token
                var currentDT = DateTime.Now;

                string token = generateJWTtoken(uid, currentDT);
                //compile sm_login
                sm_login.AccessToken = token;
                sm_login.Message = "Login Successful";
                sm_login.Expires = currentDT.AddHours(8);

                User.AccessToken = token;
                dbAuth.MsUsers.Update(User);
                dbAuth.SaveChanges();
            }

            return sm_login;
        }

        public static string generateJWTtoken(string userID, DateTime currSessionDate)
        {
            //JWT Security Key
            var securityKey = JWTscrt;

            //Encoded to UTF8
            var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityKey));

            //Creating Signature for our JWT Token
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //Assembling our JWT Token
            var token = new JwtSecurityToken(
                            issuer: "kcompany_webapp",
                            audience: userID,
                            expires: currSessionDate.AddHours(8),
                            signingCredentials: signingCredentials
                        );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt.ToString();
        }
    }
}